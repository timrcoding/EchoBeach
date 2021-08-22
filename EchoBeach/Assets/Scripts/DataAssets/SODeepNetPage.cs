using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeepNetPage", menuName = "ScriptableObjects/DeepNet/DeepNetPage")]
public class SODeepNetPage : ScriptableObject
{
    public string LinkTitleForButton;
    public string HeaderText;
    public Sprite BackgroundPattern;
    public Font Font;
    public string BodyText;
    public List<DeepNetLink> DeepNetLinks;
}

public enum Font
{
    INVALID,
    Londrina,
    Future,
}

public enum DeepNetLink
{
    INVALID,
    EllaHome,
    AdrianHome,
}
