using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeepnetManager : MonoBehaviour
{
    public static DeepnetManager instance;

    //PAGE ELEMENTS
    [SerializeField] private TextMeshProUGUI TMPHeader;
    [SerializeField] private TextMeshProUGUI TMPBody;
    [SerializeField] private Image BackgroundPattern;
    [SerializeField] private Transform LinkParent;
    [SerializeField] private GameObject LinkButtonPrefab;
    [SerializeField] private Scrollbar Scrollbar;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        LoadPageText(DeepNetLink.EllaHome);
    }

    public void LoadPageText(DeepNetLink DeepNetLink)
    {
        //Get Page
        SODeepNetPage Page = DeepNetLookupFunctions.ReturnDeepNetLinkToPage(DeepNetLink, DeepNetLookupFunctions.instance.MSODeepNetLookup);
        if (Page != null)
        {
            //Set Background
            BackgroundPattern.sprite = Page.BackgroundPattern;
            //Add header text
            TMPHeader.text = Page.HeaderText;
            //Read textasset and write body text
            TMPBody.text = "";
            TMPBody.text += '\n';
            TMPBody.text += Page.BodyText;
            Scrollbar.value = 0;
        }
        else
        {
            Debug.Log("DeepNet Page Not Set");
        }
        CreateLinks(Page);
    }

    public void CreateLinks(SODeepNetPage DeepNetPage)
    {
        //Destroy all existing links
        for(int i = 0; i < LinkParent.transform.childCount; i++)
        {
            Destroy(LinkParent.transform.GetChild(i).gameObject);
        }

        SODeepNetPage Page = DeepNetPage;
        //Setup Buttons
        foreach (DeepNetLink Link in Page.DeepNetLinks) 
        {
            GameObject LinkButton = Instantiate(LinkButtonPrefab);
            LinkButton.transform.SetParent(LinkParent);
            LinkButton.transform.localScale = Vector3.one;
            LinkButton.GetComponent<LinkButton>().SetDeepNetLink(Link);
        }
    }



}
