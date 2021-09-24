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
        BlackCoverImage.alpha = 1;
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
        if (SaveManager.instance != null) {
            int num = 9 - (int)SaveManager.instance.ActiveSave.MTaskNumber;
            if (num > 0 && num != 1)
            {
                CutSceneTextScriptableObject.StringToTypes[1].Text = $"{num} DAYS REMAIN.";
            }
            if(num == 1)
            {
                CutSceneTextScriptableObject.StringToTypes[1].Text = "1 DAY REMAINS";
            }
            else if(num <= 0)
            {
                CutSceneTextScriptableObject.StringToTypes[1].Text = "NO MORE DAYS REMAIN.\n\nTHE COMPETITION IS OVER.";
            }
        }
        else
        {
            CutSceneTextScriptableObject.StringToTypes[1].Text = "8 DAYS REMAIN.";
        }
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

        if (SaveManager.instance != null)
        {
            if (SaveManager.instance.ActiveSave.PlayDialogueSceneNext)
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
