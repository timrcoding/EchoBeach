using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LinkButton : MonoBehaviour
{
    private DeepNetLink DeepNetLink;
    [SerializeField] private TextMeshProUGUI TText;
    private Button Button;
    void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(CreateNewPage);
    }

    public void CreateNewPage()
    {
        DeepnetManager.instance.LoadPageText(DeepNetLink);
    }

    public void SetDeepNetLink(DeepNetLink Link)
    {
        DeepNetLink = Link;
        SetText();
    }

    void SetText()
    {
        SODeepNetPage Page = DeepNetLookupFunctions.ReturnDeepNetLinkToPage(DeepNetLink, DeepNetLookupFunctions.instance.MSODeepNetLookup);
        TText.text = Page.LinkTitleForButton;
    }

   
}
