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
    }
    void Start()
    {
        SetupTask();
        HardCodeValues();
    }

    private void Update()
    {
        transform.localPosition = Vector2.Lerp(transform.localPosition, TargetPosition, Time.deltaTime * 5);
    }

    public override void OutOrAway()
    {
        base.OutOrAway();
        PutAwayMutuallyExclusiveObjects("MutualPullout");
    }

    public void CountdownToPutAway()
    {
        StartCoroutine(PutAway(15));
    }

    public void SetupTask()
    {
        var CurrentTask = SOTasks.Tasks[TaskNumber];
        foreach(CharacterName CharName in CurrentTask.CharacterName)
        {
            GameObject NewAnswerArea = Instantiate(TaskAnswerAreaPrefab);
            NewAnswerArea.transform.SetParent(AnswerAreaParent);
            NewAnswerArea.transform.localScale = Vector3.one;
            NewAnswerArea.GetComponent<AnswerArea>().SetCharacter(CharName);
        }
    }
}
