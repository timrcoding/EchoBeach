using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class DataResources : MonoBehaviour
{
    public static DataResources instance;

    [SerializeField] private SOCharacterPrefabData CharacterPrefabData;
    [SerializeField] private SpriteLibraryAsset LASpriteLibraryAsset;
    public SOCharacterPrefabData GetCharacterPrefabData { get { return CharacterPrefabData; } }
    public SpriteLibraryAsset GetSpriteLibrary { get { return LASpriteLibraryAsset; } }

    private void Awake()
    {
        instance = this;
    }

    public static SOCharacter ReturnChToSo(CharacterName CharName, SOCharacterPrefabData CharData)
    {
        return (SOCharacter) CharData.CharacterToScriptableObjectDictionary[CharName];
    }

}
