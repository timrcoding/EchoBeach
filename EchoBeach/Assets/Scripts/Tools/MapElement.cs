using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapElement : MonoBehaviour
{
    [SerializeField] private DeepNetLinkName DeepNetLink;
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private GameObject LineRendererObject;
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
        SetupLines();
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
