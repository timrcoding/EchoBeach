using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LinkButton : MonoBehaviour
{
    private Link MLink;
    private TextMeshProUGUI TText;
    private Button UButton;
    void Start()
    {
        TText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(PressLink);
        SetText();
    }

    public void SetLink(Link link)
    {
        MLink = link;
    }

    void SetText()
    {
        TText.text = MLink.LinkName;
    }

    public void PressLink()
    {
        PageLoader.instance.SetCharacter(MLink.Character);
    }

   
}
