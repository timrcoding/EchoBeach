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
    [SerializeField] private Transform Pivot;
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
    public string CorrectSound;
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
              float ran = Random.Range(-1, 1);
              Pivot.rotation = Quaternion.Euler(0, 0, ran);
              CheckSlider.value = value;
              SliderFill.color = Color.Lerp(Color.white, Color.red, value);
          }).setEaseInOutQuad().setOnComplete(SubmitButton);

    }

    public void SubmitButton()
    {
        Pivot.rotation = Quaternion.Euler(0, 0, 0);
        CheckSlider.value = 0;
        if (CheckAllCorrect())
        {
            SwitchTaskInSave(false,0);
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

    public void SwitchTaskInSave(bool oride, int task)
    {
        int currentTask = (int)SaveManager.instance.ActiveSave.MTaskNumber;
        var Task = SaveManager.instance.ActiveSave.MTaskNumber;
        if(!oride) 
        { 
            Task = (TaskNumber)currentTask + 1;
            SaveManager.instance.ActiveSave.MTaskNumber = Task;
            SetComplete(Task);
            LevelManager.instance.SetDialogue();
        }
        else
        {
            Task = (TaskNumber)task;
            SaveManager.instance.ActiveSave.MTaskNumber = Task;
            SetComplete(Task);
            LevelManager.instance.SetDialogue();
            LoadConfirmScene();
        }
        
    }

    void SetComplete(TaskNumber task)
    {
        if ((int)task > (int)TaskNumber.Eight)
        {
            SaveManager.instance.ActiveSave.GameCompleted = true;
        }
        else
        {
            SaveManager.instance.ActiveSave.GameCompleted = false;
        }
    }

    public void LoadConfirmScene()
    {
        SongManager.instance.musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        SongManager.instance.musicInstance.release();
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
