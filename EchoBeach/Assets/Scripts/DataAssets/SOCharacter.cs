using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterBase", menuName = "ScriptableObjects/Character/CharacterBase")]
public class SOCharacter : ScriptableObject
{
    public CharacterCategory CharacterCategory;
    public CharacterName CharacterName;
    public string CharacterNameText;
    public ColorName BackgroundColor;
    public ColorName ButtonColor;
    public ColorName TextColor;
    public List<Link> LinkList;
    public CharacterPageTemplateType CharacterPageTemplateType;
    public RealName RealName;
    public Address Address;
    public DOB DateOfBirth;
}

[System.Serializable]
public struct Link
{
    public string LinkName;
    public CharacterName Character;
}

public enum IdentType
{
    INVALID,
    Name,
    DOB,
    Address,
}

public enum CharacterName
{
    INVALID,
    EllaNella,
    Mingbastard,
    Adrian,
    Robespierre,
    CallumWinters,
    ShelleyBaby,
}

public enum RealName
{
    INVALID,
    RosieJones,
    DavidPritchard,
    RonCox,
}

public enum DOB
{
    INVALID,
    TenOctoberEightySix,

}

public enum Address
{
    INVALID,
    SevenRuebakerStreet,
    TwelveHourglassRoad,
    StudebakerMansions,
}

public enum CharacterCategory
{
    INVALID,
    Musician,
    Journalist,
    Theorist,
}