using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum DialogueScene
{
    INVALID,
    Intro,
    One,
    Two,
    Three
}

public class InterimTextManager : MonoBehaviour
{

    public CutSceneText CutSceneTextScriptableObject;
    public int IntroCount = 1;
    [SerializeField] protected TextMeshProUGUI IntroTMP;
    public bool CanAdvance;
    [SerializeField] protected Button button;
    [FMODUnity.EventRef]
    public string Ambience;
    protected FMOD.Studio.EventInstance AmbientInst;
    [FMODUnity.EventRef]
    public string KeyboardClick;
    [FMODUnity.EventRef]
    public string ComputerClick;
    [FMODUnity.EventRef]
    public string MusicianClick;
    [SerializeField] protected bool SetForSceneAdvance;
    [SerializeField] protected CanvasGroup BlackCoverImage;

    public virtual IEnumerator PrintText()
    {
        CanAdvance = false;
        button.interactable = false;
        string s = CutSceneTextScriptableObject.StringToTypes[IntroCount].Text;
        IntroTMP.text = s;
        IntroTMP.maxVisibleCharacters = 0;
        for (int i = 0; i <= s.Length; i++)
        {
            IntroTMP.maxVisibleCharacters = i;
            if (CutSceneTextScriptableObject.StringToTypes[IntroCount].Typer == Typer.Person)
            {
                FMODUnity.RuntimeManager.PlayOneShot(KeyboardClick);
            }
            else if(CutSceneTextScriptableObject.StringToTypes[IntroCount].Typer == Typer.Musician)
            {
                FMODUnity.RuntimeManager.PlayOneShot(MusicianClick);
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot(ComputerClick);
            }

            yield return new WaitForSeconds(Random.Range(0.03f, 0.05f));
            if (i > 0)
            {
                if (s[i - 1] == ',' || s[i - 1] == '?')
                {
                    yield return new WaitForSeconds(1);
                }
                if (s[i - 1] == '.' && i - 1 != s.Length - 1)
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

    public virtual IEnumerator InitDelay()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        LeanTween.value(gameObject, 1, 0, 4).setOnUpdate((value) =>
        {
            BlackCoverImage.alpha = value;
        });
        
        button.interactable = false;
        IntroTMP.text = "";
        button.interactable = false;
    }
    public void SetText()
    {
        StartCoroutine(PrintText());
    }


}
