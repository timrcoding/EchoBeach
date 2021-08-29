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
    [SerializeField] public GameObject LargeIDParent;
    [SerializeField] public GameObject LargeIDPrefab;

    private void Awake()
    {
        instance = this;
        HardCodeValues();
    }
    void Start()
    {
        PopulateIDs();
    }

    public override void OutOrAway()
    {
        base.OutOrAway();
        PutAwayMutuallyExclusiveObjects("MutualPullout");
    }

    void PopulateIDs()
    {
        for(int i = 1; i < Enum.GetNames(typeof(CharacterName)).Length; i++)
        {
            if(DataResources.ReturnChToSo((CharacterName)i, DataResources.instance.GetCharacterPrefabData) != null)
            {
            CharacterIDs.Add(new CharacterID((CharacterName)i));
            }   
        }        
    }


    public void DisplayIDsOnSearch()
    {
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
            SearchForMatch(Input, CharID.PetString, TempCharacterList, CharID,IdentType.Pet);
        }
        //ORDER ALPHABETICALLY
        TempCharacterList = TempCharacterList.OrderBy(x => x.CharacterID.RealNameString).ToList();
        //CREATE BUTTONS
        foreach(var ID in TempCharacterList)
        {
            GameObject newCharId = Instantiate(CharacterIDButtonPrefab);
            newCharId.transform.SetParent(ResultsParent);
            newCharId.transform.localScale = Vector3.one;
            newCharId.GetComponent<CharIDButton>().SetCharacterName(ID.CharacterID,ID.IdentType);
        }
    }

    void SearchForMatch(string Input, string SearchTerm, List<CharIDAndIdent> CharIdList, CharacterID CharID, IdentType IDType)
    {
        string ConvertedSearch = SearchTerm.ToUpper();

        if (ConvertedSearch.StartsWith(Input.ToUpper()))
        {
            if (Input != "")
            {
                CharIdList.Add(new CharIDAndIdent(CharID,IDType));
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
    public CharacterID(CharacterName CharName)
    {
        MCharacterName = CharName;
        SetNameAndAddress();
    }

    public CharacterName MCharacterName;
    public RealName MRName;
    [HideInInspector]
    public string RealNameString;
    public DOB MDob;
    [HideInInspector]
    public string DOBString;
    public Address MAddress;
    [HideInInspector]
    public string AddressString;
    public Pet Pet;
    [HideInInspector]
    public string PetString;

    void SetNameAndAddress()
    {
        SOCharacter SoChar = DataResources.ReturnChToSo(MCharacterName, DataResources.instance.GetCharacterPrefabData);
        RealNameString = StringEnum.GetStringValue(SoChar.RealName);
        DOBString = StringEnum.GetStringValue(SoChar.DateOfBirth);
        AddressString = StringEnum.GetStringValue(SoChar.Address);
        PetString = StringEnum.GetStringValue(SoChar.Pet);
    }
}

