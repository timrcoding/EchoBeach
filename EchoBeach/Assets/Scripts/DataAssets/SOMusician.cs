using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Musician", menuName = "ScriptableObjects/Character/Musician")]
public class SOMusician : SOCharacter
{
    //BASIC

    public Sprite CharacterAvatar;
    public string AboutMeText;
    public List<CharacterName> FriendList;
    

    //COSMETIC
    public Color CharacterBackgroundColor;
    public Color CharacterButtonColor;
    public Sprite BackgroundTexture;
    public Color BackgroundColor;

    //FUNCTIONS
    public Sprite ReturnImage()
    {
        return CharacterAvatar;
    }
}

public struct Link
{
    public string LinkName;
    public CharacterName LinkToCharacter;
}
