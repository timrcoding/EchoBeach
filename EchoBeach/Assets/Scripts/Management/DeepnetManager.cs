using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

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

    [SerializeField] private Material CoverScreenMaterial;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(LoadInitialPage());
    }


    IEnumerator LoadInitialPage()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        
        LoadPageText(DeepNetLinkName.EllaNella);
    }

    void CoverPage()
    {
        CoverScreenMaterial.SetFloat("_Fade", 1);
        StartCoroutine(ChangeMat());
    }

    IEnumerator ChangeMat()
    {
        // Debug.Log("RUN");
        yield return new WaitForSeconds(Time.deltaTime);
        float fadeVal = CoverScreenMaterial.GetFloat("_Fade");
        if (fadeVal > 0)
        {
            float alph = CoverScreenMaterial.GetFloat("_Fade") - (2 * Time.deltaTime);
            CoverScreenMaterial.SetFloat("_Fade", alph);
            StartCoroutine(ChangeMat());
        }
    }


    public void LoadPageText(DeepNetLinkName DeepNetLink)
    {
        CoverPage();
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
