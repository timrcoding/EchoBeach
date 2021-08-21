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

    public static GameObject ReturnTempToPrefab(CharacterPageTemplateType TempType, SOCharacterPrefabData CharData)
    {
        return CharData.TemplateTypeToTemplatePrefabDictionary[TempType];
    }

    public static Color ReturnRefToCol(ColorName Col, SOCharacterPrefabData CharData)
    {
        return CharData.ColorLookupsDictionary[Col];
    }

    public static string ReturnRealNameToStr(RealName CharName, SOCharacterPrefabData CharData)
    {
        return CharData.RealNameDictionary[CharName];
    }

    public static string ReturnDOBToStr(DOB DobName, SOCharacterPrefabData CharData)
    {
        return CharData.DOBToStringDictionary[DobName];
    }

    public static string ReturnAddrToStr(Address AddrName, SOCharacterPrefabData CharData)
    {
        return CharData.AddressStringDictionary[AddrName];
    }

    public static Sprite ReturnCharAvatar(CharacterName CharName,CharacterCategory category, SpriteLibraryAsset spriteLibraryAsset)
    {
        Sprite sprite = spriteLibraryAsset.GetSprite(category.ToString(), CharName.ToString());
        if (sprite != null)
        {
            return sprite;
        }
        else
        {
            Debug.Log("No Sprite Set");
            return null;
        }
    }

}
