using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerArea : MonoBehaviour
{
    [SerializeField] private CharacterName CharacterName;
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
    }
    public void SetCharacter(CharacterName CharName)
    {
        CharacterName = CharName;
    }

    public void SetCharacterID(CharacterID CharID)
    {
        MCharacterID = CharID;
        TMPRealName.text = $"AKA: {MCharacterID.MRNameStr}";
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
