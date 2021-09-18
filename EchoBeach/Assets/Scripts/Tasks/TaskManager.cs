using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TaskManager : PulloutManager
{
    public static TaskManager instance;
    public TaskNumber TaskNumber;
    [SerializeField] private SOTaskManager SOTasks;
    [SerializeField] private Transform AnswerAreaParent;
    [SerializeField] private GameObject TaskAnswerAreaPrefab;
    [SerializeField] private GameObject TaskIntroCard;
    [SerializeField] private TextMeshProUGUI TaskHeader;
    [SerializeField] private TextMeshProUGUI TaskDescription;
    [SerializeField] private CanvasGroup BlackCoverImage;
    [SerializeField] private Slider CheckSlider;
    [SerializeField] private Image SliderFill;

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
            SetTaskFromSave();
        }
    }

    public void SetTaskFromSave()
    {
        TaskNumber = SaveManager.instance.ActiveSave.MTaskNumber;
        if (TaskNumber != TaskNumber.Tutorial)
        {
            SetupTask();
        }
        
    }

    public void IncrementTask()
    {
        int TaskAsInt = (int)TaskNumber;
        TaskNumber = (TaskNumber)TaskAsInt + 1;
        SetupTask();
    }

    void IntroduceTaskWindow()
    {
        Debug.Log("INTRODUCED");
        TaskIntroCard.transform.localPosition = Vector3.zero;
    }

    public void DisappearTaskWindow()
    {
        TaskIntroCard.SetActive(false);
        DeepnetManager.instance.ChangeMat();
        DeepnetManager.instance.LoadPageText(DeepNetLinkName.CuckooSong);
    }

    public void StartCheck()
    {
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
        }
    }

    void LoadConfirmScene()
    {
        SongManager.instance.AmbienceInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        SongManager.instance.AmbienceInstance.release();
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
        var CurrentTask = SOTasks.TaskDictionary[TaskNumber];
        TaskHeader.text = $"Day: {TaskNumber}";
        TaskDescription.text = CurrentTask.Description;
        if (CurrentTask.CharacterNames.Count > 0)
        {
            foreach (CharName Character in CurrentTask.CharacterNames)
            {
                GameObject NewAnswerArea = Instantiate(TaskAnswerAreaPrefab);
                NewAnswerArea.transform.SetParent(AnswerAreaParent);
                NewAnswerArea.transform.localScale = Vector3.one;
                NewAnswerArea.GetComponent<AnswerArea>().SetCharacter(Character);
                SaveManager.instance.ActiveSave.CurrentTargets.Add(Character);
            }
        }
    }

}
