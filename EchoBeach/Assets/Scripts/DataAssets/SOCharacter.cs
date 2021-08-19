using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterBase", menuName = "ScriptableObjects/Character/CharacterBase")]
public class SOCharacter : ScriptableObject
{
    public CharacterName CharacterName;
    public string CharacterNameText;
    public List<Link> LinkList;
    public CharacterPageTemplateType CharacterPageTemplateType;
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
