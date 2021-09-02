using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        LeanTween.value(gameObject, 1, 0, 4).setOnUpdate((value) =>
           {
               BlackCoverImage.alpha = value;
           });
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
            }
        }
    }
}
