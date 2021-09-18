using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private TaskToDialogueCutscene[] TaskToDialogueCutscenes;
    private Dictionary<TaskNumber, DialogueScene> TaskToDialogueDictionary;
    void Start()
    {
        instance = this;
        TaskToDialogueDictionary = new Dictionary<TaskNumber, DialogueScene>();

        foreach(var tdc in TaskToDialogueCutscenes)
        {
            TaskToDialogueDictionary.Add(tdc.TaskNumber, tdc.DialogueScene);
        }

        var dialogue = TaskToDialogueDictionary[SaveManager.instance.ActiveSave.MTaskNumber];
        if (dialogue != DialogueScene.INVALID)
        {
            SaveManager.instance.ActiveSave.CurrentDialogueScene = dialogue;
            SaveManager.instance.ActiveSave.PlayDialogueSceneNext = true;
        }
        else
        {
            SaveManager.instance.ActiveSave.PlayDialogueSceneNext = false;
        }
    }

}

[System.Serializable]
public struct TaskToDialogueCutscene
{
    public TaskNumber TaskNumber;
    public DialogueScene DialogueScene;
}
