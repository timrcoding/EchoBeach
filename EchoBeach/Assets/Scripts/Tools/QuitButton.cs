using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    private Button Button;
    void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(CloseGame);
    }

    void CloseGame()
    {
        if(SaveManager.instance != null)
        {
            SaveManager.instance.Save();
        }
        Application.Quit();
    }
}
