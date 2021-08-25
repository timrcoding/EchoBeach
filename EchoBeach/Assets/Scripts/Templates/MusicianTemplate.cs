using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicianTemplate : BaseTemplate
{
    [SerializeField] private TextMeshProUGUI AboutMeText;
    private Image AvatarImage;
    [SerializeField] GameObject FriendsBox;
    [SerializeField] Song MSong;


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

        Sprite sprite = DataResources.ReturnCharAvatar(MCharacterName, MCharacterCategory, DataResources.instance.GetSpriteLibrary);
        
        if(sprite != null)
        {
           // AvatarImage.sprite = sprite;
        }
        MSong = MSOMusician.Song;


        AddSongToPlaylist();
    }

    public void AddSongToPlaylist()
    {
      // SongManager.instance.AddSong(MCharacterName, MSong);
    }
}
