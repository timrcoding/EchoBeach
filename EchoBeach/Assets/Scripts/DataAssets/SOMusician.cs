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
    public Song Song;

    //FUNCTIONS
    public Sprite ReturnImage()
    {
        return CharacterAvatar;
    }

}
public enum Song
{
    INVALID,
    TurnTheHands,
    Poodles,
    FoiGrasPourTois,
}