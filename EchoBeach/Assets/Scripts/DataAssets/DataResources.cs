using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataResources : MonoBehaviour
{
    public static DataResources instance;

    [SerializeField] private SOCharacterPrefabData CharacterPrefabData;
    public SOCharacterPrefabData GetCharacterPrefabData { get { return CharacterPrefabData; } }

    private void Awake()
    {
        instance = this;
    }

    public static SOCharacter ReturnChToSo(CharacterName CharName, SOCharacterPrefabData CharData)
    {
        return (SOCharacter) CharData.CharacterToScriptableObjectDictionary[CharName];
    }

    public static GameObject ReturnTempToPrefab(CharacterPageTemplateType TempType, SOCharacterPrefabData CharData)
    {
        return CharData.TemplateTypeToTemplatePrefabDictionary[TempType];
    }

    public static Color ReturnRefToCol(ColorName Col, SOCharacterPrefabData CharData)
    {
        return CharData.ColorLookupsDictionary[Col];
    }
}
