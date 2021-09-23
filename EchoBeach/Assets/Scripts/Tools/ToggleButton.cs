using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ToggleButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Button Button;
    public bool StartingButton;
    public GameObject Tooltip;
    public TextMeshProUGUI TooltipHeaderTMP;
    public string ToolTipHeaderText;
    public TextMeshProUGUI TooltipToggleTMP;
    public string ToolTipToggleText;
    public KeyCode MKeycode;
    [SerializeField] private TMP_InputField InputField;
    [SerializeField] private bool IgnoreRemoveIDCard;
    void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(SetTab);
        TooltipHeaderTMP.text = ToolTipHeaderText;
        TooltipToggleTMP.text = $"Press 'Shift + {ToolTipToggleText}' to toggle";
        Tooltip.SetActive(false);
        StartCoroutine(SetStarter());
    }

    IEnumerator SetStarter()
    {
        yield return new WaitForSeconds(0.1f);
        if (StartingButton && SaveManager.instance.ActiveSave.MTaskNumber != TaskNumber.Tutorial)
        {
            SetTab();
        }
    }

    public void SelectInput()
    {
        if (InputField != null)
        {
            InputField.Select();
            InputField.text = "";
        }
    }

    public void SetTab()
    {
        GameSceneManager.instance.PlayClick();
        TabManager.instance.SetTab(Button);
        SelectInput();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.SetActive(true);
        CanvasGroup CG = Tooltip.GetComponent<CanvasGroup>();
        LeanTween.value(gameObject, 0, 1, .5f).setOnUpdate((value) =>
           {
               CG.alpha = value;
           });
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (Input.GetKeyDown(MKeycode) && Button.interactable)
            {
                SetTab();
                if (!IgnoreRemoveIDCard)
                {
                    RemoveIDCards();
                }
            }
        }
    }

    void RemoveIDCards()
    {
        GameObject[] Cards = GameObject.FindGameObjectsWithTag("IDCard");
        for (int i = 0; i < Cards.Length; i++)
        {
            Destroy(Cards[i]);
        }
    }
}
