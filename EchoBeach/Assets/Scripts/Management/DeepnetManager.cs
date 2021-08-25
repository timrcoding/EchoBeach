using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeepnetManager : MonoBehaviour
{
    public static DeepnetManager instance;
    public AccessAtLevel LevelOfAccess;
    //PAGE ELEMENTS
    [SerializeField] private TextMeshProUGUI TMPHeader;
    [SerializeField] private TextMeshProUGUI TMPBody;
    [SerializeField] private Image Background;
    [SerializeField] private Image BackgroundPattern;
    [SerializeField] private Transform LinkParent;
    [SerializeField] private GameObject LinkButtonPrefab;
    [SerializeField] private Scrollbar Scrollbar;
    [SerializeField] private ScrollRect ScrollRect;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        LoadPageText(DeepNetLinkName.EllaNella);
    }

    public void LoadPageText(DeepNetLinkName DeepNetLink)
    {
        //Get Page
        SODeepNetPage Page = DeepNetLookupFunctions.ReturnDeepNetLinkToPage(DeepNetLink, DeepNetLookupFunctions.instance.MSODeepNetLookup);
        if (Page != null)
        {
            //Set Background
            Background.color = Page.BackgroundColor;

            BackgroundPattern.sprite = Page.BackgroundPattern;
            //Add header text
            TMPHeader.text = StringEnum.GetStringValue(DeepNetLink);
            //Read textasset and write body text
            TMPHeader.font = DeepNetLookupFunctions.ReturnFontToTMPFont(Page.Font, DeepNetLookupFunctions.instance.MSODeepNetLookup);
            TMPBody.text = "";
            TMPBody.text += '\n' + '\n';
            TMPBody.text += Page.BodyText;
            CreateLinks(Page);
            ScrollToTop(ScrollRect);
            if (Page.Song != Song.INVALID && !SongManager.instance.SongTracklist.Contains(Page.Song))
            {
                SongManager.instance.AddSong(DeepNetLink, Page.Song);
                SongManager.instance.PutOut();
            }
        }
        else
        {
            Debug.Log("DeepNet Page Not Set");
        }
            MapManager.instance.ActivateMapElement(DeepNetLink);
    }

    public void ScrollToTop(ScrollRect scrollRect)
    {
        scrollRect.normalizedPosition = new Vector2(0, 1);
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
        foreach (DeepNetLinkToLevel Link in Page.DeepNetLinksAndLevelsOfAccess) 
        {
            GameObject LinkButton = Instantiate(LinkButtonPrefab);
            LinkButton.transform.SetParent(LinkParent);
            LinkButton.transform.localScale = Vector3.one;
            LinkButton.GetComponent<LinkButton>().SetDeepNetLink(Link);
            Debug.Log("LinksCreated");
        }
    }

    



}
