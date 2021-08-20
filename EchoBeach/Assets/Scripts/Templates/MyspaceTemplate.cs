using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MyspaceTemplate : BaseTemplate
{
    [SerializeField] private TextMeshProUGUI AboutMeText;
    [SerializeField] private Image AvatarImage;
    [SerializeField] GameObject LinksBox;
    [SerializeField] GameObject FriendsBox;


    void Start()
    {
        SetParameters();
    }

    public override void SetParameters()
    {
        base.SetParameters();

        SOMusician MSOMusician = (SOMusician)MScriptableObject;
        if (MSOMusician.CharacterNameText != "")
        {
            TMPName.text = MScriptableObject.CharacterNameText;
        }
        else
        {
            Debug.Log("CharacterNameNotSet");
        }

        if (MSOMusician.AboutMeText != "")
        {
            AboutMeText.text = MSOMusician.AboutMeText;
        }
        else
        {
            Debug.Log("AboutMeTextNotSet");
        }

        if (MSOMusician.CharacterAvatar != null)
        {
            AvatarImage.sprite = MSOMusician.CharacterAvatar;
        }
        else
        {
            Debug.Log("AvatarImageNotSet");
        }
    }
}
