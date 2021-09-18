using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : PulloutManager
{
    public static MapManager instance;

    [SerializeField] public Dictionary<DeepNetLinkName, GameObject> MapElementDictionary;
    [HideInInspector]
    public List<Toggle> ListOfToggles;
    public Sprite[] MapSprites;

    [SerializeField] private Color SelectedPostItColor;
    [SerializeField] private Color DeSelectedPostItColor;

    private void Awake()
    {
        instance = this;
        MapElementDictionary = new Dictionary<DeepNetLinkName, GameObject>();
        CompileDictionary();
    }

    private void Start()
    {
        foreach (var musician in SaveManager.instance.ActiveSave.MusiciansEncountered)
        {
            ActivateMapElement(musician);
        }
    }

    public void ActivateMapElement(DeepNetLinkName DeepNetLink)
    {
        if (!SaveManager.instance.ActiveSave.MusiciansEncountered.Contains(DeepNetLink))
        {
            SaveManager.instance.ActiveSave.MusiciansEncountered.Add(DeepNetLink);
        }
        foreach(Toggle toggle in ListOfToggles)
        {
            toggle.isOn = false;
        }

        GameObject Obj = MapElementDictionary[DeepNetLink];
        Toggle Toggle = Obj.GetComponentInChildren<Toggle>();
        if (Obj != null && !Obj.activeInHierarchy)
        {
            Obj.SetActive(true);
            Toggle.isOn = true;
        }
        else
        { 
            Toggle.isOn = true;
        }
    }

    void CompileDictionary()
    {
        Transform[] ChildObjects = gameObject.transform.GetComponentsInChildren<Transform>();
        foreach(Transform Child in ChildObjects)
        {
            if(Child.tag == "MapElement")
            {
                MapElement ME = Child.GetComponent<MapElement>();
                MapElementDictionary.Add(ME.GetDeepNetLink, Child.gameObject);
                Toggle Toggle = Child.transform.GetComponentInChildren<Toggle>();
                ListOfToggles.Add(Toggle);
                Child.gameObject.SetActive(false);         
            }
        }
        
    }
}
