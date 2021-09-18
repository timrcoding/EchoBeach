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
    private Transform Parent;
    public DeepNetLinkName GetDeepNetLink { get { return DeepNetLink; } }

    private void Awake()
    {
       
    }

    private void Start()
    {
        Name.text = StringEnum.GetStringValue(DeepNetLink);
        GameObject P = GameObject.FindGameObjectWithTag("MapParent");
        Parent = P.transform;
        SetupImage();
        SetupLines();
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z + Random.Range(-5 , 5));
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
