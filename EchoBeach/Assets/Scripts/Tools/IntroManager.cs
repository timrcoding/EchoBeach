using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public IntroText IntroText;
    public int IntroCount = 1;
    [SerializeField] private TextMeshProUGUI IntroTMP;
    [SerializeField] private TextMeshProUGUI TitleTMP;
    [SerializeField] private TextMeshProUGUI SubTMP;
    public bool CanAdvance;
    [SerializeField] Button button;
    [FMODUnity.EventRef]
    public string Ambience;
    [FMODUnity.EventRef]
    public string Music;
    FMOD.Studio.EventInstance AmbInstance;
    FMOD.Studio.EventInstance MusicInstance;
    [FMODUnity.EventRef]
    public string KeyboardClick;
    [FMODUnity.EventRef]
    public string ComputerClick;
    private bool SetForSceneAdvance;
    [SerializeField] private CanvasGroup BlackCoverImage;
    void Start()
    {
        StartCoroutine(InitDelay());
        AmbInstance = FMODUnity.RuntimeManager.CreateInstance(Ambience);
        MusicInstance = FMODUnity.RuntimeManager.CreateInstance(Music);
        AmbInstance.start();
        LeanTween.value(gameObject, 1, 0, 4).setOnUpdate((value) =>
        {
            BlackCoverImage.alpha = value;
        });
    }

    IEnumerator InitDelay()
    {
        button.interactable = false;
        SubTMP.text = "";
        TitleTMP.text = "";
        IntroTMP.text = "";
        button.interactable = false;
        yield return new WaitForSeconds(5);
        SetIntro();
    }

    public void SetIntro()
    {
        StartCoroutine(PlayIntro());
        TitleTMP.text = "";
    }

    IEnumerator PlayIntro()
    {
        CanAdvance = false;
        button.interactable = false;
        string s = IntroText.StringToTypes[IntroCount].Text;
        IntroTMP.text = s;
        IntroTMP.maxVisibleCharacters = 0;
        for (int i = 0; i <= s.Length; i++)
        {
            IntroTMP.maxVisibleCharacters = i;
            if(IntroText.StringToTypes[IntroCount].Typer == Typer.Person) 
            {
                FMODUnity.RuntimeManager.PlayOneShot(KeyboardClick);
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot(ComputerClick);
            }
            
            yield return new WaitForSeconds(Random.Range(0.03f,0.05f));
            if (i > 0)
            {
                if (s[i-1] == ',')
                {
                    yield return new WaitForSeconds(1);
                }
                if (s[i - 1] == '.' && i-1 != s.Length-1)
                {
                    yield return new WaitForSeconds(1.5f);
                }
            }
        }
        yield return new WaitForSeconds(1.5f);
        CanAdvance = true;
        button.interactable = true;
        IntroCount++;
    }

    public void AdvanceText()
    {
        if (!SetForSceneAdvance)
        {
            if (IntroCount < IntroText.StringToTypes.Count && CanAdvance)
            {

                SetIntro();
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
            AmbInstance.release();
            LeanTween.value(gameObject, 0, 1, 7).setOnUpdate((value) =>
            {
                BlackCoverImage.alpha = value;
            });

            LeanTween.value(gameObject, 1, 0, 6).setOnUpdate((value) =>
            {
                MusicInstance.setVolume(value);
                AmbInstance.setVolume(value);
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
