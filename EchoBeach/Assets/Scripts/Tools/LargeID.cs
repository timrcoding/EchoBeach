using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LargeID : MonoBehaviour
{
    [SerializeField] public CharacterID CharacterID;
    [SerializeField] private TextMeshProUGUI TMPName;
    [SerializeField] private TextMeshProUGUI TMPDOB;
    [SerializeField] private TextMeshProUGUI TMPAddress;
    [SerializeField] private TextMeshProUGUI TMPPet;

    public void SetCharacterID(CharacterID CharID)
    {
        CharacterID = CharID;
        gameObject.name = CharID.RealNameString;
        SetText();
    }

    void SetText()
    {
        TMPName.text = $"NAME: {CharacterID.RealNameString}";
        TMPDOB.text = $"DOB: {CharacterID.DOBString}";
        TMPAddress.text = $"ADDRESS: {CharacterID.AddressString}";
        TMPPet.text = $"PET: {CharacterID.PetString}";
    }


}
