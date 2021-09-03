using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmManager : InterimTextManager
{
    public List<CharName> TempTargets;
    [FMODUnity.EventRef]
    public string Music;
    protected FMOD.Studio.EventInstance MusicInst;
    void Awake()
    {
        DeleteAddedData();
        if (SaveManager.instance != null)
        {
            foreach(CharName CName in SaveManager.instance.ActiveSave.CurrentTargets)
            {
                CutSceneTextScriptableObject.StringToTypes[0].Text += $"{'\n'}{'\n'}{StringEnum.GetStringValue(CName).ToUpper()} ";
            }
        }
        else
        {
            foreach (CharName CName in TempTargets)
            {
                CutSceneTextScriptableObject.StringToTypes[0].Text += $"{'\n'}{'\n'}{StringEnum.GetStringValue(CName).ToUpper()}.";
            }
        }
        StartCoroutine(InitDelay());
    }

    void createInstance()
    {
        MusicInst = FMODUnity.RuntimeManager.CreateInstance(Music);
        MusicInst.start();
    }
    
    void DeleteAddedData()
    {
        CutSceneTextScriptableObject.StringToTypes[0].Text = "WELL DONE EMPLOYEE.\n\nYOU HAVE APPREHENDED THE FOLLOWING CRIMINALS;";
    }

    public override IEnumerator InitDelay()
    {
        StartCoroutine(base.InitDelay());
        createInstance();
        yield return new WaitForSeconds(3);
        SetText();
    }

    public void AdvanceText()
    {
        if (!SetForSceneAdvance)
        {
            if (CanAdvance)
            {
                if (IntroCount < CutSceneTextScriptableObject.StringToTypes.Count - 1)
                {
                    SetText();
                }
                else
                {
                    SetForSceneAdvance = true;
                    SetText();
                }
            }
        }
        else
        {
            LeanTween.value(gameObject, 0, 1, 4).setOnUpdate((value) =>
            {
                BlackCoverImage.alpha = value;
            });

            LeanTween.value(gameObject, 1, 0, 3).setOnUpdate((value) =>
            {
                MusicInst.setVolume(value);
            }).setOnComplete(LoadGameScene);
        }
    }

    void LoadGameScene()
    {

        MusicInst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        MusicInst.release();
        int currentTask = (int)SaveManager.instance.ActiveSave.MTaskNumber;

        if (SaveManager.instance != null)
        {
            var Task = SaveManager.instance.ActiveSave.MTaskNumber;
            Task = (TaskNumber)currentTask + 1;
            SaveManager.instance.ActiveSave.MTaskNumber = Task;
            if (Task > (TaskNumber)8)
            {
                SaveManager.instance.ActiveSave.GameCompleted = true;
            }
            if (SaveManager.instance.ActiveSave.MTaskNumber == TaskNumber.Three || SaveManager.instance.ActiveSave.GameCompleted)
            {
                SceneManager.LoadScene("InterimScene");
            }
            else
            {
                SceneManager.LoadScene("MainGameScene");
            }
        }
        else
        {
            SceneManager.LoadScene("MainGameScene");
        }
        
        
    }


}
