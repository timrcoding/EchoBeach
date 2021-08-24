using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChordPad : MonoBehaviour
{
    [SerializeField] private ChordType MChordType;
    [SerializeField] private ChordType MStoredChordType;
    [SerializeField] private Image MImage;
    [SerializeField] private int IndexRef;
    void Start()
    {
        FindIndexRef();
        ChordManager.instance.TriggerChordOnBar += PlayOnBar;
        MChordType = (ChordType)Random.Range(1, 4);
        SetColors();
    }

    void FindIndexRef()
    {
        Transform Parent = transform.parent;
        for(int i = 0; i < Parent.childCount; i++)
        {
            if(Parent.GetChild(i).gameObject == gameObject)
            {
                IndexRef = i;
            }
        }
    }

    public void PlayOnBar()
    {
        if (IndexRef == ChordManager.instance.BarCount)
        {
            if (MChordType != ChordType.INVALID)
            {
                FMODUnity.RuntimeManager.PlayOneShot(ChordManager.instance.ChordToFMODRefDictionary[MChordType]);
            }
        }
    }

    public void SetChord(ChordType Chord)
    {
        MChordType = Chord;
        SetColors();
    }

    public void StoreChord()
    {
        MStoredChordType = MChordType;
    }

    void SetColors()
    {
        MImage.color = ChordManager.instance.ChordToColorDictionary[MChordType];
    }

    public void RevertColor()
    {
        MChordType = MStoredChordType;
        SetColors();
    }

    
}
