using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public static TextManager instance;

    [SerializeField] private TextAsset TextAsset;
    private List<string> InitList;
    public List<NameToTextList> NameToTextLists;
    public Dictionary<DeepNetLinkName, List<string>> NameToTextDictionary;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(SetText());
        NameToTextDictionary = new Dictionary<DeepNetLinkName, List<string>>();
    }

    IEnumerator SetText()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        InitList = new List<string>(TextAsset.text.Split('\n'));
        for (int i = 0; i < InitList.Count; i++)
        {
            List<string> TempList = new List<string>(InitList[i].Split(';'));
            NameToTextList NewTextList = new NameToTextList((DeepNetLinkName)i + 1);

            for (int j = 0; j < TempList.Count; j++)
            {
                NewTextList.Texts.Add(TempList[j]);
            }
            NewTextList.Texts.RemoveAt(0);
            NameToTextLists.Add(NewTextList);
        }
        

        foreach(NameToTextList nToText in NameToTextLists)
        {
            NameToTextDictionary.Add(nToText.Link, nToText.Texts);
        }
    }

    public List<string> ReturnTextListForCharacter(DeepNetLinkName name)
    {
        return NameToTextDictionary[name];
    }
}



[System.Serializable]
public class NameToTextList
{
    public NameToTextList(DeepNetLinkName LinkName)
    {
        Link = LinkName;
        Texts = new List<string>();
    }

    public DeepNetLinkName Link;
    public List<string> Texts;
}
