using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character To Prefab Data Asset", menuName = "ScriptableObjects/DataAssets/CharacterDataAssets")]
public class SOCharacterPrefabData : ScriptableObject
{
    public List<CharacterToScriptableObjectLookup> CharacterToScriptableObjectLookups;
    public Dictionary<CharName, ScriptableObject> CharacterToScriptableObjectDictionary;
    private void OnEnable()
    {
        ConstructDictionaries();
    }

    void ConstructDictionaries()
    {
        CharacterToScriptableObjectDictionary = new Dictionary<CharName, ScriptableObject>();
        foreach(var Char in CharacterToScriptableObjectLookups)
        {
            CharacterToScriptableObjectDictionary.Add(Char.CharacterName, Char.ScriptableObject);
        }
    }
}

[System.Serializable]
public struct CharacterToScriptableObjectLookup
{
    public CharName CharacterName;
    public ScriptableObject ScriptableObject;
}