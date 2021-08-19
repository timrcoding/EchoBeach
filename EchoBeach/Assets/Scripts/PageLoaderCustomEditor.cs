using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PageLoader))]
public class PageLoaderCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PageLoader PageLoader = (PageLoader)target;

        if(GUILayout.Button("Create Web Page"))
        {
            PageLoader.LoadWebPage();
        }
    }
}
