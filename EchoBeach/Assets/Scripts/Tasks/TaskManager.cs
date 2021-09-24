using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TaskManager : PulloutManager
{
    public static TaskManager instance;
    [SerializeField] public SOTaskManager SOTasks;
    [SerializeField] private Transform AnswerAreaParent;
    [SerializeField] private GameObject TaskAnswerAreaPrefab;
    [SerializeField] private GameObject TaskIntroCard;
    [SerializeField] private TextMeshProUGUI TaskHeader;
    [SerializeField] private TextMeshProUGUI TaskDescription;
    [SerializeField] private CanvasGroup BlackCoverImage;
    [SerializeField] private Slider CheckSlider;
    [SerializeField] private Image SliderFill;
    [SerializeField] private Button SubButton;
    private bool TaskSetupSuccessfully;
    public bool TaskIntroductionAway;

    [FMODUnity.EventRef]
    [SerializeField] private string CheckSound;
    [FMODUnity.EventRef]
    [SerializeField] private string CorrectSound;
    [FMODUnity.EventRef]
    [SerializeField] private string IncorrectSound;

    private void Awake()
    {
        instance = this;
        HardCodeValues();
    }
    void Start()
    {
        SaveManager.instance.ActiveSave.CurrentTargets.Clear();
        LeanTween.value(gameObject, 1, 0, 4).setOnUpdate((value) =>
           {
               BlackCoverImage.alpha = value;
           });
        if (SaveManager.instance.ActiveSave.MTaskNumber != TaskNumber.Tutorial)
        {
            StartCoroutine(RunSetupSequence());
        }
    }

    IEnumerator RunSetupSequence()
    {
        yield return new WaitForEndOfFrame();
        if (!TaskSetupSuccessfully)
        {
            SetupTask();
            yield return new WaitForSeconds(Time.deltaTime);
            StartCoroutine(RunSetupSequence());
        }
    }

    void IntroduceTaskWindow()
    {
        Debug.Log("INTRODUCED");
        TaskIntroCard.transform.localPosition = Vector3.zero;
    }

    public void DisappearTaskWindow()
    {
        TaskIntroCard.SetActive(false);
        TaskIntroductionAway = true;
        DeepnetManager.instance.ChangeMat();
        DeepnetManager.instance.LoadPageText(DeepNetLinkName.CuckooSong);
        SetupTask();
    }

    public void StartCheck()
    {
        SubButton.interactable = false;
        FMODUnity.RuntimeManager.PlayOneShot(CheckSound);
        GameSceneManager.instance.PlayClick();
        LeanTween.value(0, 1, 3).setOnUpdate((value) =>
          {
              CheckSlider.value = value;
              SliderFill.color = Color.Lerp(Color.white, Color.red, value);
          }).setEaseInOutQuad().setOnComplete(SubmitButton);

    }

    public void SubmitButton()
    {
        CheckSlider.value = 0;
        if (CheckAllCorrect())
        {
            FMODUnity.RuntimeManager.PlayOneShot(CorrectSound);
            float vol;
            SongManager.instance.StopSong();
            SongManager.instance.AmbienceInstance.getVolume(out vol);
            LeanTween.value(vol, 0, 4).setOnUpdate((value) =>
            {
                SongManager.instance.AmbienceInstance.setVolume(value);
            });

            LeanTween.value(0, 1, 5).setOnUpdate((value) =>
            {
                if (BlackCoverImage != null)
                {
                    BlackCoverImage.alpha = value;
                }
            }).setOnComplete(LoadConfirmScene);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot(IncorrectSound);
            SubButton.interactable = true;
        }
    }

    public void LoadConfirmScene()
    {
        SongManager.instance.AmbienceInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        SongManager.instance.AmbienceInstance.release();
        SaveManager.instance.ActiveSave.TransferTargets();
        SceneManager.LoadScene("ConfirmScene");
    }

    public bool CheckAllCorrect()
    {
        foreach(Transform ans in AnswerAreaParent)
        {
            if (ans.gameObject.activeInHierarchy)
            {
                if (!ans.GetComponent<AnswerArea>().IsCorrect)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void SetupTask()
    {
        IntroduceTaskWindow();
        var TaskNumber = SaveManager.instance.ActiveSave.MTaskNumber;
        var CurrentTask = SOTasks.TaskDictionary[TaskNumber];
        TaskHeader.text = $"Day: {TaskNumber}";
        TaskDescription.text = CurrentTask.Description;
        if (CurrentTask.CharacterNames.Count > 0)
        
            foreach (CharName Character in CurrentTask.CharacterNames)
            {
                int i = AnswerAreaParent.childCount;
                if (i < CurrentTask.CharacterNames.Count)
                {
                    GameObject NewAnswerArea = Instantiate(TaskAnswerAreaPrefab);
                    NewAnswerArea.transform.SetParent(AnswerAreaParent);
                    NewAnswerArea.transform.localScale = Vector3.one;
                    NewAnswerArea.GetComponent<AnswerArea>().SetCharacter(Character);
                    SaveManager.instance.ActiveSave.CurrentTargets.Add(Character);
                    Debug.Log("ANSWER CREATED");
                }
            }
        TaskSetupSuccessfully = true;
        StartCoroutine(DeepnetManager.instance.LoadInitialPages());
    }
}
