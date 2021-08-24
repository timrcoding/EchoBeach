using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ChordManager : PulloutManager
{
    public static ChordManager instance;
    [SerializeField] private List<ChordToColor> ChordToColors;
    public Dictionary<ChordType, Color> ChordToColorDictionary;
    [SerializeField] private List<ChordToFMODREF> ChordToFMODREFs;
    public Dictionary<ChordType, string> ChordToFMODRefDictionary;
    public event Action TriggerChordOnBar;

    public int BeatCount;
    public int BarCount;


    private void Awake()
    {
        instance = this;
        ChordToColorDictionary = new Dictionary<ChordType, Color>();
        ChordToFMODRefDictionary = new Dictionary<ChordType, string>();

        foreach (ChordToColor ChCol in ChordToColors)
        {
            ChordToColorDictionary.Add(ChCol.ChordType, ChCol.Color);
        }

        foreach (ChordToFMODREF ChRef in ChordToFMODREFs)
        {
            ChordToFMODRefDictionary.Add(ChRef.ChordType, ChRef.FMODRef);
        }
        
    }

    private void Update()
    {
        transform.localPosition = Vector2.Lerp(transform.localPosition, TargetPosition, Time.deltaTime * 5);
    }

    private void Start()
    {
        DrumMachine.instance.SetDrumStrip += AddBeat;
        DrumMachine.instance.ClearAll += ClearBeats;
    }

    void AddBeat()
    {
        if(BeatCount == 0 && DrumMachine.instance.GetSixteenth % 4 == 0)
        {
            TriggerChordOnBar();
        }

        int num = DrumMachine.instance.GetSixteenth;
        if (num == 0 || num == 4 || num == 8 || num == 12)
        {
            BeatCount++;
        }
        if(BeatCount >= 4)
        {
            BeatCount = 0;
            BarCount++;
            Debug.Log("NEW BAR");
            if(BarCount >= 8)
            {
                BarCount = 0;
            }
        }
    }

    void ClearBeats()
    {
        BeatCount = 0;
        BarCount = 0;
    }

    [System.Serializable]
    public struct ChordToColor
    {
        public ChordType ChordType;
        public Color Color;
    }

    [System.Serializable]
    public struct ChordToFMODREF
    {
        public ChordType ChordType;
        [FMODUnity.EventRef]
        public string FMODRef;
    }
}





