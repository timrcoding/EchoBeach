using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button StartNewGameButton;
    [SerializeField] private Button ContinueButton;
    [SerializeField] private CanvasGroup CanvasGroup;
    [SerializeField] private Vector3 PostcardAwayPosition;
    [SerializeField] private GameObject Postcard;
    [SerializeField] private CanvasGroup PostCardCanvGroup;
    [SerializeField] private TextMeshProUGUI VersionText;
    [FMODUnity.EventRef]
    public string Ambience;
    private FMOD.Studio.EventInstance AmbientInst;

    private void Awake()
    {
        Postcard.SetActive(true);
    }

    void Start()
    {
        FMODUnity.RuntimeManager.LoadBank("Master");
        if (SaveManager.instance.ActiveSave.GameCompleted || !SaveManager.instance.ActiveSave.GameStarted || (int)SaveManager.instance.ActiveSave.MTaskNumber < 1)
        {
            ContinueButton.interactable = false;
        }
        StartCoroutine(SetText());
    }

    IEnumerator SetText()
    {
        if (VersionText != null)
        {
            VersionText.text = SaveManager.instance.ActiveSave.saveName;
        }
        yield return null;
    }

    public void MovePostCard()
    {
        Postcard.GetComponent<Button>().interactable = false;
        LeanTween.value(gameObject, 1, 0, 4).setOnUpdate((value) =>
           {
               PostCardCanvGroup.alpha = value;
           }).setOnComplete(PlaySong);
    }

    void PlaySong()
    {
        Postcard.SetActive(false);
        AmbientInst = FMODUnity.RuntimeManager.CreateInstance(Ambience);
        AmbientInst.start();
    }

    public void FadeAndLoad(bool newGame)
    {
        float vol;
        AmbientInst.getVolume(out vol);
        if (newGame)
        {

            LeanTween.value(gameObject, 0, 1, 2).setOnUpdate((value) =>
            {
                CanvasGroup.alpha = value;
            }).setOnComplete(StartNewGame);
        }
        else
        {
            LeanTween.value(gameObject, 0, 1, 2).setOnUpdate((value) =>
            {
                CanvasGroup.alpha = value;
            }).setOnComplete(LoadMainGameScene);
        }

        LeanTween.value(gameObject, vol, 0, 2).setOnUpdate((value) =>
        {
            AmbientInst.setVolume(value);
        });
    }

    public void StartNewGame()
    {
        SaveManager.instance.deleteData();
        SaveManager.instance.ActiveSave.GameStarted = true;
        AmbientInst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        AmbientInst.release();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMainGameScene()
    {
        AmbientInst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        AmbientInst.release();
        SceneManager.LoadScene("MainGameScene");
    }


}
