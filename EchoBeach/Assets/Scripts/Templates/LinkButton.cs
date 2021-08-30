using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LinkButton : MonoBehaviour
{
    private DeepNetLinkName DeepNetLink;
    private bool LinkActive;
    [SerializeField] private TextMeshProUGUI TText;
    [SerializeField] private Button Button;
    void Start()
    {
        Button.onClick.AddListener(CreateNewPage);
    }

    public void CreateNewPage()
    {
        DeepnetManager.instance.LoadPageText(DeepNetLink);
    }

    public void SetDeepNetLink(DeepNetLinkToLevel Link)
    {
        DeepNetLink = Link.DeepNetLink;
        if((int)Link.TaskNumber <= (int) TaskManager.instance.TaskNumber)
        {
            LinkActive = true;
        }
        else
        {
            LinkActive = false;
        }
        SetText();
    }

    void SetText()
    {
        SODeepNetPage Page = DeepNetLookupFunctions.ReturnDeepNetLinkToPage(DeepNetLink, DeepNetLookupFunctions.instance.MSODeepNetLookup);
        foreach(char c in DeepNetLink.ToString())
        {
            TText.text += c;
            if (char.IsUpper(c))
            {
                TText.text += "_";
            }
        }
        TText.text = DeepNetLink.ToString();//Page.LinkTitleForButton;
        if (!LinkActive)
        {
            Button.interactable = false;
            TText.fontStyle = TMPro.FontStyles.Strikethrough;
        }
        
    }

   
}
