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
}

[System.Serializable]
public struct Link
{
    public string LinkName;
    public CharacterName Character;
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

public enum CharacterCategory
{
    INVALID,
    Musician,
    Journalist,
    Theorist,
}