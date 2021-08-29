using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : PulloutManager
{
    public static TaskManager instance;

    [SerializeField] private SOTaskManager SOTasks;
    [SerializeField] private Transform AnswerAreaParent;
    [SerializeField] private GameObject TaskAnswerAreaPrefab;
    [SerializeField] int TaskNumber;

    private void Awake()
    {
        instance = this;
        HardCodeValues();
    }
    void Start()
    {
        SetupTask();
    }

    public override void OutOrAway()
    {
        base.OutOrAway();
    }

    public void CountdownToPutAway()
    {
     
    }

    public void SetupTask()
    {
        var CurrentTask = SOTasks.Tasks[TaskNumber];
        foreach(CharacterName Character in CurrentTask.CharacterName)
        {
            GameObject NewAnswerArea = Instantiate(TaskAnswerAreaPrefab);
            NewAnswerArea.transform.SetParent(AnswerAreaParent);
            NewAnswerArea.transform.localScale = Vector3.one;
            NewAnswerArea.GetComponent<AnswerArea>().SetCharacter(Character);
        }
    }
}
