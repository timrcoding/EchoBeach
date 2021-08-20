using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseTemplate : MonoBehaviour
{
    private CharacterName CharacterName;
    protected SOCharacter MScriptableObject;
    [SerializeField] protected TextMeshProUGUI TMPName;
    [SerializeField] private Image Background;
    [SerializeField] private Image[] Buttons;

    public void SetCharacter(CharacterName CharName)
    {
        CharacterName = CharName;
        MScriptableObject = DataResources.ReturnChToSo(CharName,DataResources.instance.GetCharacterPrefabData);
    }

    public virtual void SetParameters()
    {
        Background.color = DataResources.ReturnRefToCol(MScriptableObject.BackgroundColor, DataResources.instance.GetCharacterPrefabData);

        foreach(Image button in Buttons)
        {
            button.color = DataResources.ReturnRefToCol(MScriptableObject.ButtonColor, DataResources.instance.GetCharacterPrefabData);
        }
    }
}
