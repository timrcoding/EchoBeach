using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrumStrip : MonoBehaviour
{
    
   [SerializeField] private int IndexNum;
    public List<Toggle> DrumStripToggles;
    public Toggle IsActiveToggle;
    void Start()
    {
        FindIndex();
        CompileToggleList();
        DrumMachine.instance.SetDrumStrip += PlayDrums;
        DrumMachine.instance.ClearAll += ClearActiveToggle;
    }

    void FindIndex()
    {
        Transform Parent = transform.parent;
        for(int i = 0; i < Parent.childCount; i++)
        {
            if(Parent.GetChild(i).gameObject == gameObject)
            {
                IndexNum = i;
            }
        }
    }

    void CompileToggleList()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform Child = transform.GetChild(i);
            if (Child.tag == "Toggle")
            {
               DrumStripToggles.Add(Child.GetComponent<Toggle>());
            }
        }
    }
    
    void PlayDrums() 
    {
        if (IndexNum == DrumMachine.instance.GetSixteenth)
        {
            IsActiveToggle.isOn = true;
            for(int i = 0; i < DrumStripToggles.Count; i++)
            {
                if (DrumStripToggles[i].isOn)
                {
                    if (DrumMachine.instance.FMODDrums[i] != null)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(DrumMachine.instance.FMODDrums[i]);
                    }
                }
            }
        }
        else
        {
            IsActiveToggle.isOn = false;
        }
    }

    public void ClearActiveToggle()
    {
        IsActiveToggle.isOn = false;
    }

    public void PlayAuditionSound(int i)
    {
        if (DrumStripToggles[i].isOn && !DrumMachine.instance.StartedOrStopped)
        {
            FMODUnity.RuntimeManager.PlayOneShot(DrumMachine.instance.FMODDrums[i]);
        }
    }
}
