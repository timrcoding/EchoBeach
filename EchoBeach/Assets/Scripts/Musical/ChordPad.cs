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
            ChangeToPlayedColor(MChordType);
        }
    }

    void ChangeToPlayedColor(ChordType chordType)
    {
        Color col = ChordManager.instance.ChordToColorDictionary[MChordType];
        Color ChangeCol = new Color();
        if (chordType != ChordType.INVALID)
        {
            ChangeCol = Color.white;
        }
        else
        {
            ChangeCol = Color.black;
        }

        LeanTween.value(gameObject, 0, 1, 1).setOnUpdate((value) =>
        {
            MImage.color = Color.Lerp(col, ChangeCol, value);
        }).setOnComplete(ChangeColorBack);
    }

    void ChangeColorBack()
    {
        Color col = ChordManager.instance.ChordToColorDictionary[MChordType];
        LeanTween.value(gameObject, 1, 0, 2).setOnUpdate((value) =>
        {
            MImage.color = Color.Lerp(col, Color.white, value);
        });
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

    public void ClearChord()
    {
        MStoredChordType = ChordType.INVALID;
        MChordType = ChordType.INVALID;
        SetColors();
    }

    
}
