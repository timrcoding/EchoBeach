using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseTemplate : MonoBehaviour
{
    protected CharName MCharacterName;
    protected CharacterCategory MCharacterCategory;
    protected SOCharacter MScriptableObject;
    [SerializeField] protected TextMeshProUGUI TMPName;
    [SerializeField] protected List<TextMeshProUGUI> TextElements;
    [SerializeField] private Image Background;
    [SerializeField] private List<Image> Buttons;
    [SerializeField] private GameObject LinkButtonPrefab;
    [SerializeField] private Transform LinksBox;
    protected Transform[] AllObjects;
    
    public void SetCharacter(CharName CharName)
    {
        MCharacterName = CharName;
        MScriptableObject = DataResources.ReturnChToSo(CharName,DataResources.instance.GetCharacterPrefabData);
        MCharacterCategory = MScriptableObject.CharacterCategory;
    }

    public virtual void SetParameters()
    {
     
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

    }
}
