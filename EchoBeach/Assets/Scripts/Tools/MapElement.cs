using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapElement : MonoBehaviour
{
    [SerializeField] private DeepNetLinkName DeepNetLink;
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private GameObject LineRendererObject;
    [SerializeField] private Image BackgroundImage;
    [SerializeField] private Image CheckImage;
    [SerializeField] private GameObject CaughtImage;
    private Transform Parent;
    public DeepNetLinkName GetDeepNetLink { get { return DeepNetLink; } }

    private void Awake()
    {
       
    }

    private void Start()
    {
        CaughtImage.SetActive(false);
        SearchTargetList();
        Name.text = StringEnum.GetStringValue(DeepNetLink);
        GameObject P = GameObject.FindGameObjectWithTag("MapParent");
        Parent = P.transform;
        SetupImage();
        SetupLines();
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z + Random.Range(-5 , 5));
    }

    void SearchTargetList()
    {
        int num = (int)SaveManager.instance.ActiveSave.MTaskNumber;
        for (int i = 0; i < TaskManager.instance.SOTasks.Tasks.Count; i++)
        {
            if (i < num-1)
            {
                if (TaskManager.instance.SOTasks.Tasks[i].CharacterNames.Contains(DeepNetLink))
                {
                    CaughtImage.SetActive(true);
                }
            }
        }
    }

    void SetupImage()
    {
        int ran = Random.Range(0, MapManager.instance.MapSprites.Length);
        Sprite s = MapManager.instance.MapSprites[ran];
        BackgroundImage.sprite = s;
        CheckImage.sprite = s;
    }

    void SetupLines()
    {
        SODeepNetPage Page = DeepNetLookupFunctions.ReturnDeepNetLinkToPage(DeepNetLink, DeepNetLookupFunctions.instance.MSODeepNetLookup);
        foreach(var Link in Page.DeepNetLinksAndLevelsOfAccess)
        {  
                GameObject NewLine = Instantiate(LineRendererObject);
                NewLine.transform.SetParent(Parent);
                NewLine.transform.localScale = Vector3.one;
                NewLine.transform.localPosition = transform.localPosition;
                //SETLINEPOSITION
                NewLine.GetComponent<LineRenderObject>().StartPosition = DeepNetLink;
                NewLine.GetComponent<LineRenderObject>().EndPosition = Link.DeepNetLink;
        }
    }
}
