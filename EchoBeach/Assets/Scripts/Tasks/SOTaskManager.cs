using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskManager", menuName = "ScriptableObjects/TaskManager")]
public class SOTaskManager : ScriptableObject
{
    public List<Task> Tasks;
}

[System.Serializable]
public struct Task
{
    public List<CharacterName> CharacterName;
}