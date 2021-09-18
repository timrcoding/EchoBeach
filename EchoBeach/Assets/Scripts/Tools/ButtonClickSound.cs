using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    private Button Button;
    void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(PlayClick);
    }

    void PlayClick()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/SingleClick");
    }
}
