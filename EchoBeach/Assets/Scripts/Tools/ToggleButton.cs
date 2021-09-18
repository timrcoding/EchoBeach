using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    private Button Button;
    public bool StartingButton;
    void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(SetTab);
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

    public void SetTab()
    {
            GameSceneManager.instance.PlayClick();
            TabManager.instance.SetTab(Button);
    }
}
