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

    void Start()
    {
        StartCoroutine(InitDelay());
        AmbientInst = FMODUnity.RuntimeManager.CreateInstance(Ambience);
        AmbientInst.start();
    }

    public override IEnumerator InitDelay()
    {
        SubTMP.text = "";
        TitleTMP.text = "";
        MusicInstance = FMODUnity.RuntimeManager.CreateInstance(Music);
        StartCoroutine(base.InitDelay());
        yield return new WaitForSeconds(5);
        SetText();
    }
    public void SetText()
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
            if (IntroCount < InterimText.StringToTypes.Count && CanAdvance)
            {

                SetText();
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

    void LoadGameScene()
    {
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

        s = "A game by Tim Sheinman";

        for (int j = 0; j < s.Length; j++)
        {
            SubTMP.text += s[j];
            FMODUnity.RuntimeManager.PlayOneShot(KeyboardClick);
            yield return new WaitForSeconds(Random.Range(.1f, .4f));

        }
        yield return new WaitForSeconds(2);
        button.interactable = true;
        SetForSceneAdvance = true;
    }
}
