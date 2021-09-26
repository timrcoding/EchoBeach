using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchableDatabaseManager : PulloutManager
{
    public static SearchableDatabaseManager instance;

    [SerializeField] private List<CharacterID> CharacterIDs;
    [SerializeField] private Transform ResultsParent;
    [SerializeField] private TMP_InputField TMPInput;
    [SerializeField] private GameObject CharacterIDButtonPrefab;
    [SerializeField] private GameObject LargeIDParent;
    [SerializeField] private GameObject LargeIDStartingPoint;
    [SerializeField] private GameObject LargeIDPrefab;
    [FMODUnity.EventRef]
    public string PrintSound;
    [FMODUnity.EventRef]
    public string TypewriterSound;
    public GameObject GetIDParent { get { return LargeIDParent; } }
    public GameObject GetIDStartingPoint { get { return LargeIDStartingPoint; } }
    public GameObject GetIDPrefab { get { return LargeIDPrefab; } }

    private void Awake()
    {
        instance = this;
        HardCodeValues();
    }
    void Start()
    {
        StartCoroutine(PopulateIDs());

    }

    IEnumerator PopulateIDs()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        for(int i = 1; i < Enum.GetNames(typeof(CharName)).Length; i++)
        {
            if(DataResources.ReturnChToSo((CharName)i, DataResources.instance.GetCharacterPrefabData) != null)
            {
            CharacterIDs.Add(new CharacterID((CharName)i));
            }   
        }        
    }


    public void DisplayIDsOnSearch()
    {
        FMODUnity.RuntimeManager.PlayOneShot(TypewriterSound);
        string Input = TMPInput.text;
        var children = ResultsParent.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child != ResultsParent)
            {
                Destroy(child.gameObject);
            } 
        }

        //SEARCH FOR CHARACTERS REAL NAMES
        List<CharIDAndIdent> TempCharacterList = new List<CharIDAndIdent>();

        foreach (CharacterID CharID in CharacterIDs)
        {
            SearchForMatch(Input, CharID.RealNameString, TempCharacterList, CharID,IdentType.Name);
            SearchForMatch(Input, CharID.DOBString, TempCharacterList, CharID,IdentType.DOB);
            SearchForMatch(Input, CharID.AddressString, TempCharacterList, CharID,IdentType.Address);
            SearchForMatch(Input, CharID.OccupString, TempCharacterList, CharID,IdentType.Occupation);
        }
        //ORDER ALPHABETICALLY
        TempCharacterList = TempCharacterList.OrderBy(x => x.CharacterID.RealNameString).ToList();
        //CREATE BUTTONS
        int count = new int();
        foreach (var ID in TempCharacterList)
        {
            if (count < 5)
            {
                GameObject newCharId = Instantiate(CharacterIDButtonPrefab);
                newCharId.transform.SetParent(ResultsParent);
                newCharId.transform.localScale = Vector3.one;
                newCharId.GetComponent<CharIDButton>().SetCharacterName(ID.CharacterID, ID.IdentType);
                count++;
            }
        }
    }

    void SearchForMatch(string Input, string SearchTerm, List<CharIDAndIdent> CharIdList, CharacterID CharID, IdentType IDType)
    {
        if (SearchTerm != null)
        {
            string ConvertedSearch = SearchTerm.ToUpper();

            if (ConvertedSearch.Contains(Input.ToUpper()))
            {
                if (Input != "")
                {
                    CharIdList.Add(new CharIDAndIdent(CharID, IDType));
                }
            }
        }
    }

    public void DestroyLargeIDCollection()
    {
        if (LargeIDParent.transform.childCount > 0)
        {
            Destroy(LargeIDParent.transform.GetChild(0).gameObject);
        }
    }
}

[System.Serializable]
public class CharIDAndIdent
{
    public CharIDAndIdent(CharacterID CharID, IdentType IDType)
    {
        CharacterID = CharID;
        IdentType = IDType;
    }

    public CharacterID CharacterID;
    public IdentType IdentType;
}

[System.Serializable]
public class CharacterID
{
    public CharacterID(CharName CharName)
    {
        MCharacterName = CharName;
        SetNameAndAddress();
    }

    public CharName MCharacterName;
    public RealName MRName;
    [HideInInspector]
    public string RealNameString;
    public DOBParts MDob;
    [HideInInspector]
    public string DOBString;
    public Address MAddress;
    [HideInInspector]
    public string AddressString;
    public Occupation occupation;
    [HideInInspector]
    public string OccupString;

    void SetNameAndAddress()
    {
        SOCharacter SoChar = DataResources.ReturnChToSo(MCharacterName, DataResources.instance.GetCharacterPrefabData);
        RealNameString = StringEnum.GetStringValue(SoChar.RealName);
        DOBString = SoChar.ReturnDOBPartsAsString();
        AddressString = StringEnum.GetStringValue(SoChar.Address);
        OccupString = StringEnum.GetStringValue(SoChar.Occupation);
    }
}

