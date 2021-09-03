using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



[System.Serializable]
public struct InterimToSOText
{
    public DialogueScene DialogueScene;
    public CutSceneText DialogueScriptable;
}

public class DialogueSceneManager : InterimTextManager
{
    [SerializeField] private List<InterimToSOText> DialogueSceneLookup;
    [SerializeField] private Dictionary<DialogueScene,CutSceneText> DialogueSceneLookupDictionary;
    public bool GameCompleted;
    [FMODUnity.EventRef]
    public string Music;
    protected FMOD.Studio.EventInstance MusicInstance;
 
    void Start()
    {
        DialogueSceneLookupDictionary = new Dictionary<DialogueScene, CutSceneText>();

        foreach(var text in DialogueSceneLookup)
        {
            DialogueSceneLookupDictionary.Add(text.DialogueScene, text.DialogueScriptable);
        }

        AmbientInst = FMODUnity.RuntimeManager.CreateInstance(Ambience);
        AmbientInst.start();
        MusicInstance = FMODUnity.RuntimeManager.CreateInstance(Music);
        StartCoroutine(InitDelay());
        if (SaveManager.instance != null)
        {
            CutSceneTextScriptableObject = DialogueSceneLookupDictionary[SaveManager.instance.ActiveSave.CurrentDialogueScene];
        }
        else
        {

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
                if (IntroCount < CutSceneTextScriptableObject.StringToTypes.Count-1)
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
            if (SaveManager.instance.ActiveSave.GameCompleted)
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
                SaveManager.instance.ActiveSave.CurrentDialogueScene = DialogueScene.Two;
            }
            else
            {
                MusicInstance.release();
                MusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                SceneManager.LoadScene("MenuScene");
            }
        }
        else
        {
            SceneManager.LoadScene("MainGameScene");
        }
    }
}
