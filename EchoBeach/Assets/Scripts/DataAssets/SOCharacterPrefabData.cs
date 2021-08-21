using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character To Prefab Data Asset", menuName = "ScriptableObjects/DataAssets/CharacterDataAssets")]
public class SOCharacterPrefabData : ScriptableObject
{
    public List<CharacterToScriptableObjectLookup> CharacterToScriptableObjectLookups;
    public Dictionary<CharacterName, ScriptableObject> CharacterToScriptableObjectDictionary;

    public List<TemplateTypeToTemplatePrefabLookup> TemplateTypeToTemplatePrefabLookups;
    public Dictionary<CharacterPageTemplateType, GameObject> TemplateTypeToTemplatePrefabDictionary;

    public List<ColorLookup> ColorLookups;
    public Dictionary<ColorName, Color> ColorLookupsDictionary;

    public List<RealNameToString> RealNameToStrings;
    public Dictionary<RealName, string> RealNameDictionary;

    public List<AddressToString> AddressToStrings;
    public Dictionary<Address, string> AddressStringDictionary;

    public List<DOBToString> DOBToStrings;
    public Dictionary<DOB, string> DOBToStringDictionary;



    private void OnEnable()
    {
        ConstructDictionaries();
    }

    void ConstructDictionaries()
    {
        CharacterToScriptableObjectDictionary = new Dictionary<CharacterName, ScriptableObject>();
        TemplateTypeToTemplatePrefabDictionary = new Dictionary<CharacterPageTemplateType, GameObject>();
        ColorLookupsDictionary = new Dictionary<ColorName, Color>();
        RealNameDictionary = new Dictionary<RealName, string>();
        AddressStringDictionary = new Dictionary<Address, string>();
        DOBToStringDictionary = new Dictionary<DOB, string>();

        foreach(CharacterToScriptableObjectLookup ChSo in CharacterToScriptableObjectLookups)
        {
            CharacterToScriptableObjectDictionary.Add(ChSo.CharacterName, ChSo.ScriptableObject);
        }

        foreach (TemplateTypeToTemplatePrefabLookup TempTyPrLo in TemplateTypeToTemplatePrefabLookups)
        {
            TemplateTypeToTemplatePrefabDictionary.Add(TempTyPrLo.CharacterPageTemplateType, TempTyPrLo.CharacterPagePrefab);
        }

        foreach (ColorLookup CharCo in ColorLookups)
        {
            ColorLookupsDictionary.Add(CharCo.CharacterColor, CharCo.ColorRef);
        }

        foreach (RealNameToString CharNa in RealNameToStrings)
        {
            RealNameDictionary.Add(CharNa.RealName,CharNa.RealNameString);
        }

        foreach (AddressToString AdStr in AddressToStrings)
        {
            AddressStringDictionary.Add(AdStr.Address, AdStr.AddressString);
        }

        foreach (DOBToString DobStr in DOBToStrings)
        {
            DOBToStringDictionary.Add(DobStr.DOB, DobStr.DOBString);
        }
    }
   
}
public enum CharacterPageTemplateType
{
    INVALID,
    MusicianMyspace,
    MusicianBandcamp,
    MusicianSoundcloud,
}

[System.Serializable]
public struct CharacterToScriptableObjectLookup
{
    public CharacterName CharacterName;
    public ScriptableObject ScriptableObject;
}

[System.Serializable]
public struct TemplateTypeToTemplatePrefabLookup
{
    public CharacterPageTemplateType CharacterPageTemplateType;
    public GameObject CharacterPagePrefab;
}

[System.Serializable]
public struct RealNameToString
{
    public RealName RealName;
    public string RealNameString;
}

[System.Serializable]
public struct DOBToString
{
    public DOB DOB;
    public string DOBString;
}

[System.Serializable]
public struct AddressToString
{
    public Address Address;
    public string AddressString;
}

public enum ColorName
{
    INVALID,
    Red,
    LightRed,
    Blue,
    LightBlue,
    Green,
    LightGreen,
    Yellow,
    Orange,
    Pink,
    Purple,
    Brown,
    White,
    Black
}

[System.Serializable]
public struct ColorLookup
{
    public ColorName CharacterColor;
    public Color ColorRef;
}
