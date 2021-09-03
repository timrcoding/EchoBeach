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
    private int TutorialCount = 2;
    [SerializeField] private GameObject TutorialWindow;
    [SerializeField] private TextMeshProUGUI HeaderText;
    [SerializeField] private TextMeshProUGUI DescriptText;
    [SerializeField] private Image Image;
    
    private float TargetFloat;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        CompileDictionary();
        if (SaveManager.instance.ActiveSave.MTaskNumber == TaskNumber.Tutorial)
        {
            SetSlide();
            IntroduceTutorialWindow();
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
        GameSceneManager.instance.BlurBackground();
        LeanTween.moveLocal(TutorialWindow,Vector3.zero,1f).setEaseInOutBack();
    }

    public void RemoveTutorial()
    {
        TargetFloat = 20;
        Debug.Log("REMOVED");
        LeanTween.moveLocal(TutorialWindow, new Vector3(1750, 0, 0), 1f).setEaseInOutBack();
        if (SaveManager.instance.ActiveSave.MTaskNumber == TaskNumber.Tutorial)
        {
            SaveManager.instance.ActiveSave.MTaskNumber = TaskNumber.One;
            TaskManager.instance.SetTaskFromSave();
            TaskManager.instance.SetupTask();
            DeepnetManager.instance.LoadPageText(DeepNetLinkName.EllaNella);
        }
    }

    void SetSlide()
    {
        var Tutorial = TutorialDictionary[(TutorialSlide)TutorialCount];
        HeaderText.text = Tutorial.Header;
        DescriptText.text = Tutorial.SlideDescription;
        if (Image.sprite != null)
        {
            Image.sprite = Tutorial.Image;
        }
    }

    public void NextSlide()
    {
        if(TutorialCount < TutorialScriptableObjects.Count)
        {
            TutorialCount++;
            SetSlide();
        }
        else
        {
            RemoveTutorial();
        }
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
