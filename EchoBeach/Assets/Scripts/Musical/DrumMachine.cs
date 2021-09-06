using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrumMachine : PulloutManager
{
    public static DrumMachine instance; 

    [SerializeField] private AudioClip SixteenthNoteReferenceClip;
    public event Action TriggerDrums;
    public event Action SetDrumStrip;
    public event Action ClearAll;
    [SerializeField] private int SixteenthBeatCount;
    [SerializeField] private Toggle StartStopToggle;
    public bool StartedOrStopped;

    [FMODUnity.EventRef]
    public List<string> FMODDrums;
    public int GetSixteenth { get { return SixteenthBeatCount; } }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        TriggerDrums += PlayDrumMachine;
    }

    public void CanStartStop()
    {
        if (StartStopToggle.isOn)
        {
            StartedOrStopped = true;
            StartCoroutine(PlayDrumLoop());
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Mute", 1);
        }
        else
        {
            StartedOrStopped = false;
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Mute", 0);
        }
    }

    public IEnumerator PlayDrumLoop()
    {
        if (StartedOrStopped)
        {
            yield return new WaitForSeconds(SixteenthNoteReferenceClip.length);
            TriggerDrums();
            StartCoroutine(PlayDrumLoop());
        }
        else
        {
            SixteenthBeatCount = 0;
            ClearAll();
        }
    }

    public void PlayDrumMachine()
    {
        SetDrumStrip();
        SixteenthBeatCount += 1;
        if (SixteenthBeatCount >= 16)
        {
            SixteenthBeatCount = 0;
        }
    }
}
