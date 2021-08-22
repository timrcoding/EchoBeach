using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeepNetLookup", menuName = "ScriptableObjects/DeepNet/DeepNetLookup")]
public class SODeepNetLookup : ScriptableObject
{
    public List<LinkToScriptableObject> LinkToScriptableObjects;
    public Dictionary<DeepNetLink, SODeepNetPage> LinkToScriptableDictionary;

    public List<FontToString> FontToStrings;
    public Dictionary<Font, string> FontToStringsDictionary;

    private void OnEnable()
    {
        LinkToScriptableDictionary = new Dictionary<DeepNetLink, SODeepNetPage>();
        FontToStringsDictionary = new Dictionary<Font, string>();

        foreach(LinkToScriptableObject LObj in LinkToScriptableObjects)
        {
            LinkToScriptableDictionary.Add(LObj.DeepNetLink, LObj.SODeepNetPage);
        }

        foreach(FontToString FToStr in FontToStrings)
        {
            FontToStringsDictionary.Add(FToStr.Font,FToStr.String);
        }
    }
}

[System.Serializable]
public struct LinkToScriptableObject
{
    public DeepNetLink DeepNetLink;
    public SODeepNetPage SODeepNetPage;
}

[System.Serializable]
public struct FontToString
{
    public Font Font;
    public string String;
}
