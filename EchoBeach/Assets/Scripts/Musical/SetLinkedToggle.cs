using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLinkedToggle : MonoBehaviour
{
    private Toggle Toggle;
    public Toggle LinkedToggle;
    void Start()
    {
        Toggle = GetComponent<Toggle>();
        Toggle.onValueChanged.AddListener(delegate { SetToggle(); });
    }

    void SetToggle()
    {
      //  LinkedToggle.isOn = Toggle.isOn;
        if (Toggle.isOn)
        {
            SaveManager.instance.ActiveSave.InstrumentPlays++;
        }
    }

}
