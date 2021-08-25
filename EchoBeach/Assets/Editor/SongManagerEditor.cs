using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SongManager))]

public class SongManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SongManager songManager = (SongManager)target;

        if (GUILayout.Button("AddSong"))
        {
           // songManager.AddSong(CharacterName.EllaNella,Song.TurnTheHands,1);
        }
    }
}
