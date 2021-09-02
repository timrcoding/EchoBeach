using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

[CreateAssetMenu(fileName = "DeepNetPage", menuName = "ScriptableObjects/DeepNet/DeepNetPage")]
public class SODeepNetPage : ScriptableObject
{
    public Color BackgroundColor;
    public Sprite BackgroundPattern;
    public Font Font;
    public List<DeepNetLinkToLevel> DeepNetLinksAndLevelsOfAccess;
    public Song Song;
}

[System.Serializable]
public struct DeepNetLinkToLevel
{
    public DeepNetLinkName DeepNetLink;
    public TaskNumber TaskNumber;
}
[System.Serializable]
public struct StringToLevelOfAccess
{
    public string BodyText;
    public TaskNumber TaskNumber;
}
public enum Font
{
    INVALID,
    Londrina,
    Klee,
    FutureLight,
}

public enum DeepNetLinkName
{
    INVALID,
    [StringValue("Ella Nella")]
    EllaNella,
    [StringValue("Wronguns")]
    WrongUns,
    [StringValue("Future Perfect")]
    FuturePerfect,
    [StringValue("Davey Moon")]
    DaveyMoon,
    [StringValue("Lady Jane")]
    LadyJane,
    [StringValue("Smoking Elvis")]
    SmokingElvis,
    [StringValue("Simple Simon")]
    SimpleSimon,
    [StringValue("Boyfriend Sweater")]
    BoyfriendSweater,
    [StringValue("Sad Girl Tik Tok")]
    SadGirlTikTok,
    [StringValue("Grey Gardner")]
    GreyGardner,
    [StringValue("Blantano")]
    Blantano,
    [StringValue("Baby Bell")]
    BabyBell,
    [StringValue("Girlfriend Smirlfriend")]
    GirlfriendSmirlfriend,
    [StringValue("Sunburnt County")]
    SunburntCounty,
    [StringValue("Viley Curt")]
    VileyCurt,
    [StringValue("Adrian")]
    Adrian,
    [StringValue("Oodles Of Poodles")]
    OodlesOfPoodles,
    [StringValue("Danzig Ostrifier")]
    DanzigOstrifier,
    [StringValue("A Felt Mountain")]
    AFeltMountain,
    [StringValue("Buried Pleasures")]
    BuriedPleasures,
    [StringValue("Chuck & Steven")]
    ChuckAndSteven,
    [StringValue("Unleashed Collier")]
    UnleashedCollier,
    [StringValue("Twelve Tone Moan")]
    TwelveToneMoan,
    [StringValue("Cornell's Kernels")]
    CornellsKernels,
    [StringValue("Mealy Adam")]
    MealyAdam,
    [StringValue("Shit Parade")]
    ShitParade,
    [StringValue("Slick Rick")]
    SlickRick,
}

public enum TaskNumber
{
    Tutorial,
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    INVALID,
}
public class StringValue : System.Attribute
{
    private readonly string _value;

    public StringValue(string value)
    {
        _value = value;
    }

    public string Value
    {
        get { return _value; }
    }

}

public static class StringEnum
{
    public static string GetStringValue(Enum value)
    {
        string output = null;
        Type type = value.GetType();
        FieldInfo fi = type.GetField(value.ToString());
        StringValue[] attrs =
           fi.GetCustomAttributes(typeof(StringValue),
                                   false) as StringValue[];
        if (attrs.Length > 0)
        {
            output = attrs[0].Value;
        }

        return output;
    }
}