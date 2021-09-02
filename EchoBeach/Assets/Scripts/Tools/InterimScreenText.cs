using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InterimScreenText", menuName = "ScriptableObjects/Text/InterimScreenText")]
public class InterimScreenText : ScriptableObject
{ 

    public List<StringToType> StringToTypes;
}

[System.Serializable]
public class StringToType
{
    [Multiline(5)]
    public string Text;
    public Typer Typer;
}

public enum Typer
{
    INVALID,
    Person,
    Computer,
    Musician,
}
