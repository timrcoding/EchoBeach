using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button StartNewGameButton;
    [SerializeField] private Button ContinueButton;
    [SerializeField] private CanvasGroup CanvasGroup;

    void Start()
    {
        if (SaveManager.instance.ActiveSave.GameCompleted || !SaveManager.instance.ActiveSave.GameStarted)
        {
            ContinueButton.interactable = false;
        }
    }

    public void FadeAndLoad(bool newGame)
    {
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
    }

    public void StartNewGame()
    {
        SaveManager.instance.deleteData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMainGameScene()
    {
        SceneManager.LoadScene("MainGameScene");
    }


}
