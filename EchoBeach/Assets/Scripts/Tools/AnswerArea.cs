using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerArea : MonoBehaviour
{
    [SerializeField] private CharName CharacterName;
    [SerializeField] private TextMeshProUGUI TMPArtistName;
    [SerializeField] private TextMeshProUGUI TMPRealName;
    [SerializeField] private Image BackgroundImage;
    [SerializeField] private Color OriginalColor;
    [SerializeField] private Color ChangeColor;
    [SerializeField] private CharacterID MCharacterID;
    [SerializeField] private bool IsCorrect = false;

    private void Start()
    {
        ClearField();
        SetArtistName();
    }
    public void SetCharacter(CharName CharName)
    {
        CharacterName = CharName;
    }


    void SetArtistName()
    {
        TMPArtistName.text = StringEnum.GetStringValue(CharacterName);
    }
 
    public void SetCharacterID(CharacterID CharID)
    {
        MCharacterID = CharID;
        TMPRealName.text = $"AKA: {MCharacterID.RealNameString}";
        CheckForCorrect();
        BackgroundImage.color = OriginalColor;
    }

    public void ClearField()
    {
        ClearText();
    }

    public void ClearText()
    {
        TMPRealName.text = $"AKA: ";
        IsCorrect = false;
        BackgroundImage.color = OriginalColor;
    }

    void CheckForCorrect()
    {
        if(CharacterName == MCharacterID.MCharacterName)
        {
            IsCorrect = true;
        }
        else
        {
            IsCorrect = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "IDCard")
        {
            BackgroundImage.color = ChangeColor;
            other.GetComponent<LargeIDDragAndDrop>().SetTransferToAnswers(true,gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "IDCard")
        {
            BackgroundImage.color = OriginalColor;
            other.GetComponent<LargeIDDragAndDrop>().SetTransferToAnswers(false,gameObject);
        }
    }
}
