using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameSceneManager))]

public class GameSceneManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameSceneManager GameManager = (GameSceneManager)target;

        if (GUILayout.Button("Complete Task"))
        {
          //  GameManager.CompleteTask();
        }
    }
}
