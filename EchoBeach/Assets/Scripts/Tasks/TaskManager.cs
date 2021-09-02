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

    public override void OutOrAway()
    {
        base.OutOrAway();
    }

    public void DisappearTaskWindow()
    {
        TutorialManager.instance.SetTargetFloat(20f);
        LeanTween.moveLocal(TaskIntroCard, new Vector3(0,-1050,0), .5f).setEaseInOutBack();
    }

    public void SumbitButton()
    {
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
            Debug.Log("NOT ALL CORRECT");
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
            if (!ans.GetComponent<AnswerArea>().IsCorrect)
            {
                return false;
            }
        }
        return true;
    }


    public void SetupTask()
    {
        LeanTween.moveLocal(TaskIntroCard, Vector3.zero, .5f).setEaseInOutBack();
        TutorialManager.instance.SetTargetFloat(0.1f);
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
