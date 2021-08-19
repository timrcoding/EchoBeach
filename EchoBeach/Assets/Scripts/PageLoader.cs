using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PageLoader : MonoBehaviour
{
    [SerializeField] private SOCharacterPrefabData CharacterDataTable;
    [SerializeField] private Transform WebPageParent;
    [SerializeField] private CharacterName Character;
    void Start()
    {
        
    }

    public void LoadWebPage()
    {
        if (CharacterDataTable.CharacterToScriptableObjectDictionary != null)
        {
            SOCharacter MScriptableObject = (SOCharacter)CharacterDataTable.CharacterToScriptableObjectDictionary[Character];
            CharacterPageTemplateType TemplateType = MScriptableObject.CharacterPageTemplateType;
            GameObject Prefab = CharacterDataTable.TemplateTypeToTemplatePrefabDictionary[TemplateType];

            GameObject NewPage = Instantiate(Prefab);
            NewPage.transform.SetParent(WebPageParent);
            NewPage.transform.localScale = Vector3.one;
        }
    }
}
