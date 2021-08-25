using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicLookup", menuName = "ScriptableObjects/Music/MusicLookup")]
public class SOMusicLookup : ScriptableObject
{
    public List<SongToFMODString> SongToFMODStrings;
    public Dictionary<Song, string> SongToFMODictionary;

    private void OnEnable()
    {
        SongToFMODictionary = new Dictionary<Song, string>();

        foreach(var SoFMOD in SongToFMODStrings)
        {
            SongToFMODictionary.Add(SoFMOD.Song, SoFMOD.FMODRef);
        }
    }
}

[System.Serializable]
public struct SongToFMODString
{
    public Song Song;
    [FMODUnity.EventRef]
    public string FMODRef;
}
