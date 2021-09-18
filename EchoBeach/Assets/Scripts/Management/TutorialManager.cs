using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    [SerializeField] private List<SOTutorial> TutorialScriptableObjects;
    private Dictionary<TutorialSlide, SOTutorial> TutorialDictionary;
    public int TutorialCount;
    [SerializeField] private GameObject TutorialWindow;
    [SerializeField] private TextMeshProUGUI HeaderText;
    [SerializeField] private TextMeshProUGUI DescriptText;

    [SerializeField] private ToggleNameAndItemForTutorial[] ToggleNameList;
    
    private float TargetFloat;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        TutorialWindow.SetActive(false);
        CompileDictionary();
        if (SaveManager.instance.ActiveSave.MTaskNumber == TaskNumber.Tutorial)
        {
            SetSlide();
            IntroduceTutorialWindow();
            SetToggles(false);
        }
        
    }

    void SetToggles(bool b)
    {
        for(int i = 0; i < ToggleNameList.Length; i++)
        {
            ToggleNameList[i].ToggleButton.GetComponent<Button>().interactable = b;
        }
    }

    public void SetTargetFloat(float f)
    {
        TargetFloat = f;
    }

    void CompileDictionary()
    {
        TutorialDictionary = new Dictionary<TutorialSlide, SOTutorial>();
        foreach (var Tut in TutorialScriptableObjects)
        {
            TutorialDictionary.Add(Tut.TutorialSlide, Tut);
        }
    }

    void IntroduceTutorialWindow()
    {
        TutorialWindow.transform.localPosition = Vector3.zero;
        TutorialWindow.SetActive(true);
        DeepnetManager.instance.SetMatToOpaque();
    }

    public void RemoveTutorial()
    {
        TutorialWindow.SetActive(false);
        if (SaveManager.instance.ActiveSave.MTaskNumber == TaskNumber.Tutorial)
        {
            SaveManager.instance.ActiveSave.MTaskNumber = TaskNumber.One;
            TaskManager.instance.SetTaskFromSave();
            TaskManager.instance.SetupTask();
        }
        SetToggles(true);
        DeepnetManager.instance.ChangeMat();
    }

    void SetSlide()
    {
        
        DeepnetManager.instance.ChangeMat();
        var Tutorial = TutorialScriptableObjects[TutorialCount];
        HeaderText.text = Tutorial.Header;
        DescriptText.text = Tutorial.SlideDescription;

        for(int i = 0; i < ToggleNameList.Length; i++)
        {
            if(Tutorial.ToggleWithTutorial == ToggleNameList[i].ToggleWithTutorial)
            {
                ToggleNameList[i].ToggleButton.SetTab();
                Debug.Log("OPEN");
            }
            else
            {
              
            }
        }

    }

    public void NextSlide()
    {
        if(TutorialCount < TutorialScriptableObjects.Count-1)
        {
            TutorialCount++;
            SetSlide();
        }
        else
        {
            RemoveTutorial();
        }
        GameSceneManager.instance.PlayClick();
    }

    
}
public enum TutorialSlide
{
    INVALID,
    Intro,
    DeepNet,
    Map,
    TaskManager,
    SearchableData,
    Clues,
    SongHolder,
}

public enum ToggleWithTutorial
{
    INVALID,
    TargetToggle,
    SongToggle,
    SearchToggle,
}

[System.Serializable]
public struct ToggleNameAndItemForTutorial
{
    public ToggleWithTutorial ToggleWithTutorial;
    public ToggleButton ToggleButton;
}
