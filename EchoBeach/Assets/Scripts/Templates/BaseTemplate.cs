using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseTemplate : MonoBehaviour
{
    protected CharacterName MCharacterName;
    protected CharacterCategory MCharacterCategory;
    protected SOCharacter MScriptableObject;
    [SerializeField] protected TextMeshProUGUI TMPName;
    [SerializeField] protected TextMeshProUGUI[] TextElements;
    [SerializeField] private Image Background;
    [SerializeField] private Image[] Buttons;
    [SerializeField] private GameObject LinkButtonPrefab;
    [SerializeField] private Transform LinksBox;

    public void SetCharacter(CharacterName CharName)
    {
        MCharacterName = CharName;
        MScriptableObject = DataResources.ReturnChToSo(CharName,DataResources.instance.GetCharacterPrefabData);
        MCharacterCategory = MScriptableObject.CharacterCategory;
    }

    public virtual void SetParameters()
    {
        Background.color = DataResources.ReturnRefToCol(MScriptableObject.BackgroundColor, DataResources.instance.GetCharacterPrefabData);

        foreach(Image button in Buttons)
        {
            button.color = DataResources.ReturnRefToCol(MScriptableObject.ButtonColor, DataResources.instance.GetCharacterPrefabData);
        }

        foreach(var text in TextElements)
        {
            text.color = DataResources.ReturnRefToCol(MScriptableObject.TextColor, DataResources.instance.GetCharacterPrefabData);
        }

        createLinks();
    }

    void createLinks()
    {
        foreach(Link link in MScriptableObject.LinkList)
        {
            GameObject button = Instantiate(LinkButtonPrefab);
            button.transform.SetParent(LinksBox);
            button.transform.localScale = Vector3.one;
            button.GetComponent<LinkButton>().SetLink(link);
        }
    }
}
