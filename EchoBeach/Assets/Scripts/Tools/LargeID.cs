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

    public void SetCharacterID(CharacterID CharID)
    {
        CharacterID = CharID;
        gameObject.name = CharID.MRNameStr;
        SetText();
    }

    void SetText()
    {
        TMPName.text = $"NAME: {CharacterID.MRNameStr}";
        TMPDOB.text = $"DOB: {CharacterID.MDobString}";
        TMPAddress.text = $"ADDRESS: {CharacterID.MAddressStr}";
    }


}
