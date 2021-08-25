using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeepNetLookup", menuName = "ScriptableObjects/DeepNet/DeepNetLookup")]
public class SODeepNetLookup : ScriptableObject
{
    public List<LinkToScriptableObject> LinkToScriptableObjects;
    public Dictionary<DeepNetLinkName, SODeepNetPage> LinkToScriptableDictionary;

    public List<FontToString> FontToStrings;
    public Dictionary<Font, string> FontToStringsDictionary;

    public List<FontToFont> FontRefToTMPFont;
    public Dictionary<Font, TMPro.TMP_FontAsset> FontRefToTMPFontDictionary;

    private void OnEnable()
    {
        LinkToScriptableDictionary = new Dictionary<DeepNetLinkName, SODeepNetPage>();
        FontToStringsDictionary = new Dictionary<Font, string>();
        FontRefToTMPFontDictionary = new Dictionary<Font, TMPro.TMP_FontAsset>();

        foreach(LinkToScriptableObject LObj in LinkToScriptableObjects)
        {
            LinkToScriptableDictionary.Add(LObj.DeepNetLink, LObj.SODeepNetPage);
        }

        foreach(FontToString FToStr in FontToStrings)
        {
            FontToStringsDictionary.Add(FToStr.Font,FToStr.String);
        }

        foreach(FontToFont FToF in FontRefToTMPFont)
        {
            FontRefToTMPFontDictionary.Add(FToF.Font, FToF.FontAsset);
        }
    }
}

[System.Serializable]
public struct LinkToScriptableObject
{
    public DeepNetLinkName DeepNetLink;
    public SODeepNetPage SODeepNetPage;
}

[System.Serializable]
public struct FontToString
{
    public Font Font;
    public string String;
}

[System.Serializable]
public struct FontToFont
{
    public Font Font;
    public TMPro.TMP_FontAsset FontAsset;
}