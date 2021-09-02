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
        Debug.Log("START");
        DeleteAddedData();
        if (SaveManager.instance != null)
        {
            foreach(CharName CName in SaveManager.instance.ActiveSave.CurrentTargets)
            {
                InterimText.StringToTypes[0].Text += $"{'\n'}{'\n'}{StringEnum.GetStringValue(CName).ToUpper()} ";
            }
        }
        else
        {
            foreach (CharName CName in TempTargets)
            {
                InterimText.StringToTypes[0].Text += $"{'\n'}{'\n'}{StringEnum.GetStringValue(CName).ToUpper()}.";
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
        InterimText.StringToTypes[0].Text = "WELL DONE EMPLOYEE.\n\nYOU HAVE APPREHENDED THE FOLLOWING CRIMINALS;";
    }

    public override IEnumerator InitDelay()
    {
        StartCoroutine(base.InitDelay());
        Debug.Log("CALLED");
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
                if (IntroCount < InterimText.StringToTypes.Count - 1)
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
        float vol;
        MusicInst.getVolume(out vol);
        MusicInst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        MusicInst.release();
        SceneManager.LoadScene("MainGameScene");
    }


}
