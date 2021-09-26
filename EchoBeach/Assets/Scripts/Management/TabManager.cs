using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public static TabManager instance;
    public Button LastButton;
    public Button SpareButton;
    [SerializeField] private Color SelectedButtonColor;

    public List<Tab> Tabs;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetTab(null);
    }

    public Button ReturnButton(PulloutManager pullout)
    {
        foreach (Tab tab in Tabs)
        {
            if (tab.Pullout == pullout)
            {
                return tab.Button;
            }
        }
        return null;
    }

    public void SetTab(Button mButton, bool b = false)
    {
        Tab MTab = new Tab();

        foreach (Tab tab in Tabs)
        {
            if (tab.Button == mButton)
            {
                MTab = tab;

            }
            tab.Button.image.color = Color.white;
        }

        foreach (Tab tab in Tabs)
        {
            if (mButton != null)
            {
                if (tab.Button == mButton)
                {
                    if (mButton != LastButton)
                    {
                        tab.Pullout.gameObject.SetActive(true);
                        SetTween(tab.Pullout.gameObject, tab.Pullout.GetOutPosition, true,tab.TurnOffWhenOffScreen);
                        LastButton = mButton;

                    }
                    else
                    {
                        if (!b)
                        {
                            SetTween(tab.Pullout.gameObject, tab.Pullout.GetAwayPosition, false, tab.TurnOffWhenOffScreen);
                            LastButton = null;
                            return;
                        }
                    }
                }
                else if (MTab.IncompatibleWithTabs.Contains(tab.TabType))
                {
                    SetTween(tab.Pullout.gameObject, tab.Pullout.GetAwayPosition, false, tab.TurnOffWhenOffScreen);
                }
            }
        }
        if (LastButton != null)
        {
            LastButton.image.color = SelectedButtonColor;
        }
    }

    void SetTween(GameObject obj, Vector3 TargetPos, bool active, bool turnOff)
    {
        if (!active)
        {
            if (turnOff)
            {
                LeanTween.moveLocal(obj, TargetPos, .5f).setEaseInOutBack().setOnComplete(TurnOffObject).setOnCompleteParam(obj as object);
            }
            else
            {
                LeanTween.moveLocal(obj, TargetPos, .5f).setEaseInOutBack();
            }
        }
        else
        {
            LeanTween.moveLocal(obj, TargetPos, .5f).setEaseInOutBack();
        }
    }

    void TurnOffObject(object Gobj)
    {
      //  GameObject obj = Gobj as GameObject;
      //  obj.SetActive(false);
    }

    [System.Serializable]
    public struct Tab
    {
        public Button Button;
        public PulloutManager Pullout;
        public TabType TabType;
        public List<TabType> IncompatibleWithTabs;
        public bool TurnOffWhenOffScreen;
    }
}

public enum TabType
{
    INVALID,
    TaskTool,
    DrumMachine,
    ChordMachine,
    Map,
}
