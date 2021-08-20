using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PageLoader : MonoBehaviour
{
    public static PageLoader instance;

    [SerializeField] private Transform WebPageParent;
    [SerializeField] private CharacterName Character;
    private void Awake()
    {
        instance = this;
    }

    public void DestroyActiveWebPage()
    {
        foreach(Transform child in WebPageParent)
        {
            Destroy(child.gameObject);
            Debug.Log("WebPage Destroyed");
        }
    }

    public void SetCharacter(CharacterName MCharacterName)
    {
        Character = MCharacterName;
        LoadWebPage();
    }

    public void LoadWebPage()
    {
        DestroyActiveWebPage();
        SOCharacter MScriptableObject = DataResources.ReturnChToSo(Character,DataResources.instance.GetCharacterPrefabData);
        CharacterPageTemplateType TemplateType = MScriptableObject.CharacterPageTemplateType;
        GameObject Prefab = DataResources.ReturnTempToPrefab(TemplateType, DataResources.instance.GetCharacterPrefabData);
        GameObject NewPage = Instantiate(Prefab);
        NewPage.transform.SetParent(WebPageParent);
        NewPage.transform.localScale = Vector3.one;
        NewPage.GetComponent<BaseTemplate>().SetCharacter(Character);
        Debug.Log("WebPage Created");
    }
}
