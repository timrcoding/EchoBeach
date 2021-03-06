using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DrumMachine : PulloutManager
{
    public static DrumMachine instance; 

    [SerializeField] private AudioClip SixteenthNoteReferenceClip;
    public event Action TriggerDrums;
    public event Action SetDrumStrip;
    public event Action ClearAll;
    [SerializeField] private int SixteenthBeatCount;
    [SerializeField] private Toggle StartStopToggle;
    [SerializeField] private Slider TempoSlider;
    [SerializeField] private TextMeshProUGUI BPMReading;
    public bool DrumInSync;

    public bool StartedOrStopped;
    public DrumType DrumTypePlayed;
    [FMODUnity.EventRef]
    public List<string> FMODAcousticDrums;
    [FMODUnity.EventRef]
    public List<string> FMODElectricDrums;
    public Toggle DrumTypeToggle;
    public int GetSixteenth { get { return SixteenthBeatCount; } }

    public enum DrumType
    {
        INVALID,
        Acoustic,
        Electric
    }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        TriggerDrums += PlayDrumMachine;
        DrumTypeToggle.isOn = false;
        DrumTypePlayed = DrumType.Electric;
    }

    public void ChangeDrumType()
    {
        if (DrumTypeToggle.isOn)
        {
            DrumTypePlayed = DrumType.Acoustic;
        }
        else
        {
            DrumTypePlayed = DrumType.Electric;
        }
    }

    public void CanStartStop()
    {
        if (StartStopToggle.isOn)
        {
            StartedOrStopped = true;
            StartCoroutine(PlayDrumLoop());
        }
        else
        {
            StartedOrStopped = false;
        }
    }

    public void SetBPMReading()
    {
        BPMReading.text = TempoSlider.value.ToString();
    }

    public IEnumerator PlayDrumLoop()
    {
        float BPM = 60 / TempoSlider.value;

        if (StartedOrStopped)
        {
            TriggerDrums();
            yield return new WaitForSeconds(BPM/4);
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
        SixteenthBeatCount++;
        if (SixteenthBeatCount >= 16)
        {
            SixteenthBeatCount = 0;
        }
    }
}
