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
    [Header("DOB")]
    public DOBParts DOBParts;
    public Occupation Occupation;

    public string ReturnDOBPartsAsString()
    {
        return $"{StringEnum.GetStringValue(DOBParts.Day)}/{StringEnum.GetStringValue(DOBParts.Month)}/{StringEnum.GetStringValue(DOBParts.Year)}";
    }

    private void OnEnable()
    {
        CharacterCategory = CharacterCategory.Musician;

        if (DOBParts.Day == Day.INVALID || DOBParts.Month == Month.INVALID || DOBParts.Year == Year.INVALID)
        {
            DOBParts.Day = (Day)Random.Range(0, 28);
            DOBParts.Month = (Month)Random.Range(0, System.Enum.GetValues(typeof(Month)).Length);
            DOBParts.Year = (Year)Random.Range(0, System.Enum.GetValues(typeof(Year)).Length);
        }

        if (Occupation == Occupation.INVALID)
        {
            Occupation = (Occupation)Random.Range(0, System.Enum.GetValues(typeof(Occupation)).Length);
        }

        if (RealName != RealName.INVALID)
        {
            int charint = (int)CharacterName;
            RealName = (RealName)charint;
        }
        
    }
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
    [StringValue("Camille Rose")]
    CamilleRose,
    [StringValue("Daisy Richards")]
    DaisyRichards,
    [StringValue("David Pritchard")]
    DavidPritchard,
    [StringValue("Elsie Bristol")]
    ElsieBristol,
    [StringValue("Richard Sandwich")]
    RichardSandwich,
    [StringValue("Sam Partridge")]
    SamPartridge,
    [StringValue("Carey Elden")]
    CareyElden,
    [StringValue("Jane Baldrey")]
    JaneBaldrey,
    [StringValue("Connor Sackville")]
    ConnorSackville,
    [StringValue("Anthony Flatte")]
    AnthonyFlatte,
    [StringValue("Caroline Black")]
    CarolineBlack,
    [StringValue("Angelina Smyth")]
    AngelinaSmyth,
    [StringValue("Charlie Samuels")]
    CharlieSamuels,
    [StringValue("Brad Tweddle")]
    BradTweddle,
    [StringValue("Gwilym Jones")]
    GwilymJones,
    [StringValue("Rob Toodle")]
    RobToodle,
    [StringValue("Gunther Rosen")]
    GuntherRosen,
    [StringValue("Elvis Runkin")]
    ElvisRunkin,
    [StringValue("Georgina Walsh")]
    GeorginaWalsh,
    [StringValue("Leslie Tompkins")]
    LeslieTompkins,
    [StringValue("Joseph Tail")]
    JosephTail,
    [StringValue("Josef Ellams")]
    JosefEllams,
    [StringValue("Charles Babcock")]
    CharlesBabcock,
    [StringValue("Guthrie Simpkins")]
    GuthrieSimpkins,
    [StringValue("Fred Eagle")]
    FredEagle,
    [StringValue("Manny Robins")]
    MannyRobins,
    //NPCS

}

public enum Day
{
    INVALID,
    [StringValue("01")]
    one,
    [StringValue("02")]
    two,
    [StringValue("03")]
    three,
    [StringValue("04")]
    four,
    [StringValue("05")]
    five,
    [StringValue("06")]
    six,
    [StringValue("07")]
    seven,
    [StringValue("08")]
    eight,
    [StringValue("09")]
    nine,
    [StringValue("10")]
    ten,
    [StringValue("11")]
    eleven,
    [StringValue("12")]
    twelve,
    [StringValue("13")]
    thirteen,
    [StringValue("14")]
    fourteen,
    [StringValue("15")]
    fifteen,
    [StringValue("16")]
    sixteen,
    [StringValue("17")]
    seventeen,
    [StringValue("18")]
    eighteen,
    [StringValue("19")]
    nineteen,
    [StringValue("20")]
    twenty,
    [StringValue("21")]
    twentyone,
    [StringValue("22")]
    twentytwo,
    [StringValue("23")]
    twentythree,
    [StringValue("24")]
    twentyfour,
    [StringValue("25")]
    twentyfive,
    [StringValue("26")]
    twentysix,
    [StringValue("27")]
    twentyseven,
    [StringValue("28")]
    twentyeight,
    [StringValue("29")]
    twentynine,
    [StringValue("30")]
    thirty,
    [StringValue("31")]
    thirtyone,
}

public enum Month
{
    INVALID,
    [StringValue("01")]
    one,
    [StringValue("02")]
    two,
    [StringValue("03")]
    three,
    [StringValue("04")]
    four,
    [StringValue("05")]
    five,
    [StringValue("06")]
    six,
    [StringValue("07")]
    seven,
    [StringValue("08")]
    eight,
    [StringValue("09")]
    nine,
    [StringValue("10")]
    ten,
    [StringValue("11")]
    eleven,
    [StringValue("12")]
    twelve
}

public enum Year
{
    INVALID,
    [StringValue("10")]
    ten,
    [StringValue("11")]
    eleven,
    [StringValue("12")]
    twelve,
    [StringValue("13")]
    thirteen,
    [StringValue("14")]
    fourteen,
    [StringValue("15")]
    fifteen,
    [StringValue("16")]
    sixteen,
    [StringValue("17")]
    seventeen,
    [StringValue("18")]
    eighteen,
    [StringValue("19")]
    nineteen,
    [StringValue("20")]
    twenty,
    [StringValue("21")]
    twentyone,
}

public enum Address
{
    INVALID,
    [StringValue("Ruebaker Street")]
    RuebakerStreet,
    [StringValue("Ohio Drive")]
    OhioDrive,
    [StringValue("Studebaker Mansions")]
    StudebakerMansions,
    [StringValue("Rosen Road")]
    RosenRoad,
    [StringValue("Warsaw Street")]
    WarsawStreet,
    [StringValue("Moscow Villas")]
    MoscowVillas,
    [StringValue("Chicago Corner")]
    ChicagoCorner,
    [StringValue("Minnesota Avenue")]
    MinnesotaAvenue,
    [StringValue("Berlin Road")]
    BerlinRoad,
    [StringValue("Mississippi Crescent")]
    MississippiCrescent,
    [StringValue("Satie Drive")]
    SatieDrive,
    [StringValue("Ravel Road")]
    RavelRoad,
    [StringValue("Petersburg Lane")]
    PetersburgLane,

}

public enum Occupation
{
    INVALID,
    [StringValue("Teacher")]
    Teacher,
    [StringValue("Vet")]
    Vet,
    [StringValue("Dock Worker")]
    DockWorker,
    [StringValue("Painter")]
    Painter,
    [StringValue("Butcher")]
    Butcher,
    [StringValue("Plumber")]
    Plumber,
    [StringValue("Carpenter")]
    Carpenter,
    [StringValue("Driver")]
    Driver,
    [StringValue("Doctor")]
    Doctor,
    [StringValue("Porter")]
    Porter,
}


public enum CharacterCategory
{
    INVALID,
    Musician,
    Journalist,
    Theorist,
}

[System.Serializable]
public class DOBParts
{
    public Day Day;
    public Month Month;
    public Year Year;
}