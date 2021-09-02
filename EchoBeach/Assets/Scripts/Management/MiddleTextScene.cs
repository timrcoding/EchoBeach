using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiddleTextScene : InterimTextManager
{
    public InterimScreenText FirstInterim;
    public InterimScreenText SecondInterim;
    public bool GameCompleted;
    [FMODUnity.EventRef]
    public string Music;
    protected FMOD.Studio.EventInstance MusicInstance;

    void Start()
    {
        AmbientInst = FMODUnity.RuntimeManager.CreateInstance(Ambience);
        AmbientInst.start();
        MusicInstance = FMODUnity.RuntimeManager.CreateInstance(Music);
        StartCoroutine(InitDelay());
        if (SaveManager.instance != null)
        {
            if (SaveManager.instance.ActiveSave.GameCompleted)
            {
                InterimText = SecondInterim;
            }
            else
            {
                InterimText = FirstInterim;
            }
        }
    }

    public override IEnumerator InitDelay()
    {
        StartCoroutine(base.InitDelay());
        yield return new WaitForSeconds(5);
        SetText();
    }

    public void AdvanceText()
    {
        if (!SetForSceneAdvance)
        {
            if (CanAdvance)
            {
                if (IntroCount < InterimText.StringToTypes.Count-1)
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
            float FadeOutLength = 5;
            if (GameCompleted)
            {
                MusicInstance.start();
                FadeOutLength = 15;
            }
            LeanTween.value(gameObject, 0,1, 4).setOnUpdate((value) =>
            {
                BlackCoverImage.alpha = value;
            });

            LeanTween.value(gameObject, 1, 0,FadeOutLength ).setOnUpdate((value) =>
            {
                AmbientInst.setVolume(value);
            }).setOnComplete(FadeMusic);
        }
    }

    void FadeMusic()
    {
        float vol;
        MusicInstance.getVolume(out vol);
        LeanTween.value(gameObject, vol , 0, 6).setOnUpdate((value) =>
        {
            MusicInstance.setVolume(value);
        }).setOnComplete(LoadGameScene);
    }


    void LoadGameScene()
    {
        if (SaveManager.instance != null)
        {
            if (!SaveManager.instance.ActiveSave.GameCompleted || !GameCompleted)
            {
                SceneManager.LoadScene("MainGameScene");
            }
            else
            {
                MusicInstance.release();
                MusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                SceneManager.LoadScene("MainGameScene");
            }
        }
        else
        {
            SceneManager.LoadScene("MainGameScene");
        }
    }
}
