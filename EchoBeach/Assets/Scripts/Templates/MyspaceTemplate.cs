using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MyspaceTemplate : BaseTemplate
{
    [SerializeField] private Image AvatarImage;
    [SerializeField] GameObject LinksBox;
    [SerializeField] GameObject FriendsBox;



    //TMP ELEMENTS
    public TextMeshPro TMPAboutMe;

    void Start()
    {
        
    }

    void SetPageElements()
    {
        SOMusician MSOMusician = (SOMusician)MScriptableObject;
        if (MSOMusician.CharacterNameText != "")
        {
            TMPName.text = MSOMusician.CharacterNameText;
        }
        else
        {
            Debug.Log("CharacterNameNotSet");
        }

        if (MSOMusician.AboutMeText != "")
        {
            TMPAboutMe.text = MSOMusician.AboutMeText;
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
