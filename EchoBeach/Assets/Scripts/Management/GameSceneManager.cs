using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;
    public List<DeepnetLinkToCharName> DeepnetLinkToCharNames;
    public Dictionary<DeepNetLinkName, CharName> DeepnetLinkToCharnameDict;
    [FMODUnity.EventRef]
    public string ClickSound;
    public int overrideTask;
    public TMP_Dropdown TMPDropdown;

    private void Awake()
    {
        instance = this;
        for(int i = 0; i < 28; i++)
        {
            DeepnetLinkToCharNames.Add(new DeepnetLinkToCharName((DeepNetLinkName)i, (CharName)i));
        }

        DeepnetLinkToCharnameDict = new Dictionary<DeepNetLinkName, CharName>();
        foreach (var c in DeepnetLinkToCharNames)
        {
            DeepnetLinkToCharnameDict.Add(c.DeepNetLinkName, c.CharName);
        }
    }
    public void PlayClick()
    {
        FMODUnity.RuntimeManager.PlayOneShot(ClickSound);
    }

    public void CompleteTaskAndGoToDropdown()
    {
        TaskManager.instance.SwitchTaskInSave(true, TMPDropdown.value);
    }

    public void SkipTask()
    {
        TaskManager.instance.SwitchTaskInSave(false,0);
        TaskManager.instance.LoadConfirmScene();
    }

    public void SetOverrideTaskNumber()
    {
        overrideTask = TMPDropdown.value;
    }
}

[System.Serializable]
public class DeepnetLinkToCharName
{
    public CharName CharName;
    public DeepNetLinkName DeepNetLinkName;

    public DeepnetLinkToCharName(DeepNetLinkName DN, CharName C)
    {
        CharName = C;
        DeepNetLinkName = DN;
    }
}
