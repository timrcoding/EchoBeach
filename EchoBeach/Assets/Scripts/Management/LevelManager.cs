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
        
        
    }

    public void SetDialogue()
    {
        var dialogue = TaskToDialogueDictionary[SaveManager.instance.ActiveSave.MTaskNumber];
        if (dialogue != DialogueScene.INVALID)
        {
            SaveManager.instance.ActiveSave.CurrentDialogueScene = dialogue;

            if (SaveManager.instance.ActiveSave.MTaskNumber == TaskNumber.Nine)
            {
                if (CalculateEnding())
                {
                    //TOO MUCH MUSIC
                    SaveManager.instance.ActiveSave.CurrentDialogueScene = DialogueScene.EndingTwo;
                }
                else
                {
                    SaveManager.instance.ActiveSave.CurrentDialogueScene = DialogueScene.EndingOne;
                }
            }
            SaveManager.instance.ActiveSave.PlayDialogueSceneNext = true;
        }
        else
        {
            SaveManager.instance.ActiveSave.PlayDialogueSceneNext = false;
        }
    }
    
    bool CalculateEnding()
    {
        if (SaveManager.instance.ActiveSave.SongPlays > 30 || SaveManager.instance.ActiveSave.InstrumentPlays > 20)
        {
            //TOO MUCH MUSIC
            return true;
        }
        else
        {
            return false;
        }
    }

}

[System.Serializable]
public struct TaskToDialogueCutscene
{
    public TaskNumber TaskNumber;
    public DialogueScene DialogueScene;
}
