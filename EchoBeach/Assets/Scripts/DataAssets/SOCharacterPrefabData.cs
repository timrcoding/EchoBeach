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

    private void OnEnable()
    {
        ConstructDictionaries();
        Debug.Log("Dictionaries Constructed");
    }

    void ConstructDictionaries()
    {
        CharacterToScriptableObjectDictionary = new Dictionary<CharacterName, ScriptableObject>();
        TemplateTypeToTemplatePrefabDictionary = new Dictionary<CharacterPageTemplateType, GameObject>();

        foreach(CharacterToScriptableObjectLookup ChSo in CharacterToScriptableObjectLookups)
        {
            CharacterToScriptableObjectDictionary.Add(ChSo.CharacterName, ChSo.ScriptableObject);
        }

        foreach (TemplateTypeToTemplatePrefabLookup TempTyPrLo in TemplateTypeToTemplatePrefabLookups)
        {
            TemplateTypeToTemplatePrefabDictionary.Add(TempTyPrLo.CharacterPageTemplateType, TempTyPrLo.CharacterPagePrefab);
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
