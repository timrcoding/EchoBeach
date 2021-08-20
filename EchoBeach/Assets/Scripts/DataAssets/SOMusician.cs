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
