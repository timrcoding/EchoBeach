using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class DeepnetManager : MonoBehaviour
{
    public static DeepnetManager instance;
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
    [SerializeField] private Image CircleLoader;
    public Transform TutTaskPos;


    [SerializeField] List<DeepNetLinkName> PreviouslyVisitedPages;
    private bool BackButtonPressed;

    [SerializeField]
    private List<LinksAvailableAtLevel> LinksAndLevelsForSetup;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(LoadInitialPages());
        SetMatToOpaque();
        ChangeMat();
    }

    public IEnumerator LoadInitialPages()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        Debug.Log("LOADED PAGES");
        LoadPageText(DeepNetLinkName.CuckooSong);
        for (int i = 0; i < LinksAndLevelsForSetup.Count; i++)
        {
            for(int j = 0; j < LinksAndLevelsForSetup[i].LinksAvailable.Count; j++)
            {
                if (i <= (int)SaveManager.instance.ActiveSave.MTaskNumber)
                {
                    DeepNetLinkName Name = LinksAndLevelsForSetup[i].LinksAvailable[j];
                    LoadPageText(Name);
                }
            }
        }
        LoadPageText(DeepNetLinkName.CuckooSong);
    }

    void CoverPage()
    {
        if (TaskManager.instance.TaskIntroductionAway)
        {
            ChangeMat();
        }
    }

    public void SetMatToOpaque()
    {
        CoverScreenMaterial.SetFloat("_Fade", 1);
    }

    public void ChangeMat()
    {
        LeanTween.value(gameObject, 1, 0, 1).setOnUpdate((value) =>
           {
               CoverScreenMaterial.SetFloat("_Fade", value);
           }).setEaseInQuad();
    }

    void ScrollDownScrollbar()
    {
        LeanTween.value(gameObject, Scrollbar.value, 0, 1f).setOnUpdate((value) =>
        {
            Scrollbar.value = value;
        });
    }

    public void VisitPreviousPage()
    {
        if(PreviouslyVisitedPages.Count > 1)
        {
            BackButtonPressed = true;
            PreviouslyVisitedPages.RemoveAt(PreviouslyVisitedPages.Count - 1);
            LoadPageText(PreviouslyVisitedPages[PreviouslyVisitedPages.Count - 1]);
        }
    }

    public void LoadPageText(DeepNetLinkName DeepNetLink)
    {
        CoverPage();
        SODeepNetPage Page = DeepNetLookupFunctions.ReturnDeepNetLinkToPage(DeepNetLink, DeepNetLookupFunctions.instance.MSODeepNetLookup);
        if (Page != null)
        {

            TMPHeader.text = StringEnum.GetStringValue(DeepNetLink);
            TMPBody.text = "";
            
            TMPBody.text += '\n';
            TMPBody.text += '\n';

            List<string> TempList = TextManager.instance.ReturnTextListForCharacter(DeepNetLink);
            for(int i = 0; i < TempList.Count; i++)
            {
                if(i < (int)SaveManager.instance.ActiveSave.MTaskNumber)
                {
                    string s = TempList[i];
                    if (!s.StartsWith('/'.ToString())) 
                    {
                        TMPBody.text += '\n';
                        TMPBody.text += s.TrimEnd('(', ')', '1', '2', '3', '4', '5', '6', '7', '8', ' ');
                        TMPBody.text += '\n';
                        TMPBody.text += "-----------";
                        TMPBody.text += '\n';
                    }
                }
            }
            
            CreateLinks(Page);
            ScrollToTop(ScrollRect);
            if (Page.Song != Song.INVALID && !SongManager.instance.SongTracklist.Contains(Page.Song))
            {
                SongManager.instance.AddSong(DeepNetLink, Page.Song);
            }
        }
        else
        {
            Debug.Log("DeepNet Page Not Set");
        }
        MapManager.instance.ActivateMapElement(DeepNetLink);
        ScrollDownScrollbar();
    }

    public void ScrollToTop(ScrollRect scrollRect)
    {
       // scrollRect.normalizedPosition = new Vector2(0, 1);
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
        }
    }

    [System.Serializable]
    public struct LinksAvailableAtLevel
    {
        public TaskNumber TaskNumber;
        public List<DeepNetLinkName> LinksAvailable;
    }
}
