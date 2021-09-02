using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterBase", menuName = "ScriptableObjects/Character/CharacterBase")]
public class SOCharacter : ScriptableObject
{
    public CharacterCategory CharacterCategory;
    public CharName CharacterName;
    public RealName RealName;
    public Address Address;
    public DOB DateOfBirth;
    public Occupation Occupation;
}

public enum IdentType
{
    INVALID,
    Name,
    DOB,
    Address,
    Occupation
}

public enum CharName
{
    INVALID,
    [StringValue("Ella Nella")]
    EllaNella,
    [StringValue("Cuckoo Song")]
    CuckooSong,
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

public enum RealName
{
    INVALID,
    [StringValue("Rosie Jones")]
    RosieJones,
    [StringValue("David Pritchard")]
    DavidPritchard,
    [StringValue("Ron Cox")]
    RonCox,
    [StringValue("Elvin Pudding")]
    ElvinPudding,
    [StringValue("Wolf Damzer")]
    WolfDamzer,
    [StringValue("Becky Young")]
    BeckyYoung,
    [StringValue("Phoebe Buckets")]
    PhoebeBuckets,
    [StringValue("Gavin Dross")]
    GavinDross,
    [StringValue("Sam Mulgraw")]
    SamMulgraw,
    [StringValue("Daniel Elms")]
    DanElms,
    [StringValue("Alex Horowitz")]
    AlexHorowitz,
    [StringValue("Gwendolyn")]
    Gwendolyn,
    [StringValue("Matt Shatt")]
    MattShatt,
    [StringValue("Vinnie Wiley")]
    VinnieWiley,
    [StringValue("Jet Jetson")]
    JetJetson,
    [StringValue("Bonnie Smiler")]
    BonnieSmiler,
    [StringValue("Lucy Bristol")]
    LucyBristol,
    [StringValue("Zippy Simons")]
    ZippySimons,
    [StringValue("Carol Leafer")]
    CarolLeafer,
    [StringValue("Leif Blower")]
    LeifBlower,
    [StringValue("Gail Adams")]
    GailAdams,
    [StringValue("David Lovechild")]
    DavidLoveChild,
    [StringValue("Amadeus Smith")]
    AmadeusSmith,
    [StringValue("Simeon Timeon")]
    SimeonTimeon,
    [StringValue("Friedrich Snuller")]
    FriedrichSnuller,
    [StringValue("Jane Ponce")]
    JanePonce
}

public enum DOB
{
    INVALID,
    [StringValue("10/10/1986")]
    TenOctoberEightySix,
}

public enum Address
{
    INVALID,
    [StringValue("7 Ruebaker Street")]
    SevenRuebakerStreet,
    [StringValue("12 Hourglass Road")]
    TwelveHourglassRoad,
    [StringValue("1 Studebaker Mansions")]
    OneStudebakerMansions,
}

public enum Occupation
{
    INVALID,
    [StringValue("Teacher")]
    Teacher,
    [StringValue("Vet")]
    Vet,
    [StringValue("Lawyer")]
    Lawyer,
    [StringValue("Policeman")]
    Policeman,
    [StringValue("Butcher")]
    Butcher,
    [StringValue("Plumber")]
    Plumber,
}


public enum CharacterCategory
{
    INVALID,
    Musician,
    Journalist,
    Theorist,
}