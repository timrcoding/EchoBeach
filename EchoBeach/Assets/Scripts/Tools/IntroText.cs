using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntroText", menuName = "ScriptableObjects/Intro/IntroText")]
public class IntroText : ScriptableObject
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
    Computer
}
