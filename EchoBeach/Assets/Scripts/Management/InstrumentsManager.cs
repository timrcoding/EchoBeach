using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentsManager : MonoBehaviour
{
    public InstrumentToLevel[] InstrumentToLevels;
    void Start()
    {
        SetInstruments();
    }

    void SetInstruments()
    {
        foreach(var inst in InstrumentToLevels)
        {
            if((int) inst.TaskNumber <= (int) SaveManager.instance.ActiveSave.MTaskNumber)
            {
                inst.ToggleButton.SetActive(true);
                inst.Instrument.SetActive(true);
            }
            else
            {
                inst.ToggleButton.SetActive(false);
                inst.Instrument.SetActive(false);
            }
        }
    }


    [System.Serializable]
    public struct InstrumentToLevel
    {
        public TaskNumber TaskNumber;
        public GameObject ToggleButton;
        public GameObject Instrument;
    }
}
