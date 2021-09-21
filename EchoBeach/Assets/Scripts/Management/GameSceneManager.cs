using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;

    [FMODUnity.EventRef]
    public string ClickSound;

    private void Awake()
    {
        instance = this;
    }

    public void PlayClick()
    {
        FMODUnity.RuntimeManager.PlayOneShot(ClickSound);
    }

    public void CompleteTask()
    {
        TaskManager.instance.LoadConfirmScene();
    }

}
