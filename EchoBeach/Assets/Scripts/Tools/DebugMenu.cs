using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    [SerializeField] private GameObject DebugPanel;
    // Update is called once per frame

    private void Start()
    {
        DebugPanel.SetActive(false);
    }
    void Update()
    {
        if (SaveManager.instance.ActiveSave.MTaskNumber != TaskNumber.Tutorial)
        {
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                if (Input.GetKeyDown(KeyCode.F12))
                {
                    DebugPanel.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.F11))
                {
                    DebugPanel.SetActive(true);
                }
            }
        }
    }
}
