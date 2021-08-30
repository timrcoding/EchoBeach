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
    private Dictionary<TutSlide, SOTutorial> TutorialDictionary;
    private int TutorialCount = 2;
    [SerializeField] private GameObject TutorialWindow;
    [SerializeField] private TextMeshProUGUI HeaderText;
    [SerializeField] private TextMeshProUGUI DescriptText;
    [SerializeField] private Image Image;
    [SerializeField] private Volume Volume;
    [SerializeField] private DepthOfField DOF;
    private float TargetFloat;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        CompileDictionary();
        SetSlide();
        StartCoroutine(MoveTutorialWindow());
        Volume.profile.TryGet<DepthOfField>(out DOF);
    }

    private void Update()
    {
        DOF.focusDistance.value = Mathf.Lerp(DOF.focusDistance.value, TargetFloat, .1f);
    }

    public void SetTargetFloat(float f)
    {
        TargetFloat = f;
    }

    void CompileDictionary()
    {
        TutorialDictionary = new Dictionary<TutSlide, SOTutorial>();
        foreach (var Tut in TutorialScriptableObjects)
        {
            TutorialDictionary.Add(Tut.TutorialSlide, Tut);
        }
    }


    IEnumerator MoveTutorialWindow()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        TargetFloat = 0.1f;
        if(TaskManager.instance.TaskNumber == TaskNumber.Tutorial)
        {
            LeanTween.moveLocal(TutorialWindow,Vector3.zero,1f).setEaseInOutBack();
        }
    }

    void SetSlide()
    {
        var Tutorial = TutorialDictionary[(TutSlide)TutorialCount];
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

    public void RemoveTutorial()
    {
        TargetFloat = 20;
        Debug.Log("REMOVED");
        LeanTween.moveLocal(TutorialWindow,new Vector3(1550,0,0), 1f).setEaseInOutBack();
        if(TaskManager.instance.TaskNumber == TaskNumber.Tutorial)
        {
            TaskManager.instance.TaskNumber = TaskNumber.One;
            TaskManager.instance.SetupTask();
        }
    }
}

public enum TutSlide
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
