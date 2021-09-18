using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionCover : MonoBehaviour
{
    public static TransitionCover instance;
    private CanvasGroup CanvasGroup;
    void Start()
    {
        instance = this;
        CanvasGroup = GetComponent<CanvasGroup>();
        if(instance != this)
        {
            Destroy(instance);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void IncreaseOpacityAndLoadNext(float time, string scene)
    {
        LeanTween.value(gameObject, 0, 1, time).setOnUpdate((value) =>
           {
               CanvasGroup.alpha = value;
           }).setOnComplete(LoadNextScene);
    }

    public void DecreaseOpacity(float time)
    {
        LeanTween.value(gameObject, 1, 0, time).setOnUpdate((value) =>
        {
            CanvasGroup.alpha = value;
        });
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
