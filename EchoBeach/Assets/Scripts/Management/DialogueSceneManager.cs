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
    [FMODUnity.EventRef]
    public string Music;
    protected FMOD.Studio.EventInstance MusicInstance;
    public GameObject Dust;
    public GameObject Flakes;

    void Start()
    {
        Dust.SetActive(false);
        Flakes.SetActive(false);
        BlackCoverImage.alpha = 1;
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
                Dust.SetActive(true);
                Flakes.SetActive(true);
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
        LeanTween.value(gameObject, vol , 0, 3).setOnUpdate((value) =>
        {
            MusicInstance.setVolume(value);
        }).setOnComplete(LoadGameScene);
    }


    public void LoadGameScene()
    {
        if (SaveManager.instance != null)
        {
            if (!SaveManager.instance.ActiveSave.GameCompleted)
            {
                MusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                MusicInstance.release();
                AmbientInst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                AmbientInst.release();
                int curr = (int)SaveManager.instance.ActiveSave.CurrentDialogueScene;
                SaveManager.instance.ActiveSave.CurrentDialogueScene = (DialogueScene)curr + 1;
                SceneManager.LoadScene("MainGameScene");
            }
            else
            {
                MusicInstance.release();
                MusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                MusicInstance.release();
                AmbientInst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                SceneManager.LoadScene("MenuScene");
            }
        }
        else
        {
           SceneManager.LoadScene("MainGameScene");
        }
    }
}
