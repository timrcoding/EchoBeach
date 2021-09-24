using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroManager : InterimTextManager
{
    [SerializeField] private TextMeshProUGUI TitleTMP;
    [SerializeField] private TextMeshProUGUI SubTMP;
    [FMODUnity.EventRef]
    public string Music;
    protected FMOD.Studio.EventInstance MusicInstance;
    public bool ScreenStarted;
    private string SubString;

    void Start()
    {
        BlackCoverImage.alpha = 1;
        AmbientInst = FMODUnity.RuntimeManager.CreateInstance(Ambience);
        AmbientInst.start();
        StartCoroutine(StartScreenCheck());
    }

    IEnumerator StartScreenCheck()
    {
        if (!ScreenStarted)
        {
            StartCoroutine(InitDelay());
        }
        yield return new WaitForSeconds(Time.deltaTime);
        if (!ScreenStarted)
        {
            AmbientInst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            SceneManager.LoadScene("Intro");
        }
    }

    public override IEnumerator InitDelay()
    {
        ScreenStarted = true;
        SubString = SubTMP.text;
        SubTMP.text = "";
        TitleTMP.text = "";
        MusicInstance = FMODUnity.RuntimeManager.CreateInstance(Music);
        StartCoroutine(base.InitDelay());
        yield return new WaitForSeconds(5);
        SetScreenText();
    }
    public void SetScreenText()
    {
        TitleTMP.text = "";
        StartCoroutine(PrintText());   
    }

    public override IEnumerator PrintText()
    {
        yield return null;
        StartCoroutine(base.PrintText());
    }

    public void AdvanceText()
    {
        if (!SetForSceneAdvance)
        {
            if (IntroCount < CutSceneTextScriptableObject.StringToTypes.Count && CanAdvance)
            {

                SetScreenText();
            }
            else if (CanAdvance)
            {
                IntroTMP.text = "";
                StartCoroutine(SetTitle());
            }
        }

        else
        {
            MusicInstance.release();
            AmbientInst.release();
            LeanTween.value(gameObject, 0, 1, 7).setOnUpdate((value) =>
            {
                BlackCoverImage.alpha = value;
            });

            LeanTween.value(gameObject, 1, 0, 6).setOnUpdate((value) =>
            {
                MusicInstance.setVolume(value);
                AmbientInst.setVolume(value);
            }).setOnComplete(LoadGameScene);
    
        }
        
    }

    public void LoadGameScene()
    {
        MusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        AmbientInst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        SceneManager.LoadScene("MainGameScene");  
    }

    IEnumerator SetTitle()
    {

        MusicInstance.start();
        button.interactable = false;
        string s = $"ECHO BEACH";

        for(int i = 0; i < s.Length; i++)
        {
            TitleTMP.text += s[i];
            FMODUnity.RuntimeManager.PlayOneShot(KeyboardClick);
            
            yield return new WaitForSeconds(Random.Range(.5f, 1));

        }

        s = SubString;

        for (int j = 0; j < s.Length; j++)
        {
            SubTMP.text += s[j];
            FMODUnity.RuntimeManager.PlayOneShot(KeyboardClick);
            yield return new WaitForSeconds(Random.Range(.1f, .2f));

        }
        yield return new WaitForSeconds(2);
        button.interactable = true;
        SetForSceneAdvance = true;
    }
}
