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
    [SerializeField] protected List<TextMeshProUGUI> TextElements;
    [SerializeField] private Image Background;
    [SerializeField] private List<Image> Buttons;
    [SerializeField] private GameObject LinkButtonPrefab;
    [SerializeField] private Transform LinksBox;
    protected Transform[] AllObjects;
    
    public void SetCharacter(CharacterName CharName)
    {
        MCharacterName = CharName;
        MScriptableObject = DataResources.ReturnChToSo(CharName,DataResources.instance.GetCharacterPrefabData);
        MCharacterCategory = MScriptableObject.CharacterCategory;
    }

    public virtual void SetParameters()
    {
        AllObjects = gameObject.GetComponentsInChildren<Transform>();
        SortObjectsByTag();

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

    void SortObjectsByTag()
    {
        foreach(Transform obj in AllObjects)
        {
            if (obj.tag == "TMP")
            {
                TextElements.Add(obj.GetComponent<TextMeshProUGUI>());
            }
            if (obj.tag == "Background")
            {
                Background = obj.GetComponent<Image>();
            }
            if (obj.tag == "Button")
            {
                Buttons.Add(obj.GetComponent<Image>());
            }
        }
    }

    void createLinks()
    {
        foreach(Link link in MScriptableObject.LinkList)
        {
            GameObject button = Instantiate(LinkButtonPrefab);
            button.transform.SetParent(LinksBox);
            button.transform.localScale = Vector3.one;
           // button.GetComponent<LinkButton>().SetLink(link);
        }
    }
}
