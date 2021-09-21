using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTutorial : MonoBehaviour
{
    [SerializeField] private GameObject TutorialWindow;
    private Toggle Toggle;
    void Start()
    {
        Toggle = GetComponent<Toggle>();
    }

    public void TurnOnOffTutorial()
    {

    }

    
}
