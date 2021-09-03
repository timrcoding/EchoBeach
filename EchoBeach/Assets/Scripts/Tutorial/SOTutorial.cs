using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialSlide", menuName = "ScriptableObjects/Tutorial/TutorialSlide")]
public class SOTutorial : ScriptableObject
{
    public string Header;
    [Multiline(10)]
    public string SlideDescription;
    public TutorialSlide TutorialSlide;
    public Sprite Image;
}
