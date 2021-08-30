using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskManager", menuName = "ScriptableObjects/TaskManager")]
public class SOTaskManager : ScriptableObject
{
    public List<Task> Tasks;
    public Dictionary<TaskNumber, Task> TaskDictionary;

    private void OnEnable()
    {
        TaskDictionary = new Dictionary<TaskNumber, Task>();
        Debug.Log("DICTIONARY COMPILED");
        foreach(var T in Tasks)
        {
            TaskDictionary.Add(T.TaskNumber,T);
        }
    }
}

[System.Serializable]
public struct Task
{
    public TaskNumber TaskNumber;
    public string Header;
    [Multiline(10)]
    public string Description;
    public List<DeepNetLinkName> CharacterNames;
}