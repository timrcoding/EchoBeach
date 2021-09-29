using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordManager : MonoBehaviour
{
    public static RecordManager instance;

    [SerializeField] private GameObject RecordPrefab;
    [SerializeField] private Transform RecordParent;
    public Transform RecessedParent;
    [SerializeField] private Transform RecordStart;
    [SerializeField] private Transform RecordEnd;
    [SerializeField] private List<SongToRecordCover> SongToRecordCovers;
    public Dictionary<Song, Sprite> SongToRecordDictionary;
    public AnimationCurve AnimCurve;
    public float AnimSpeed;
    public float RecordSize = .6f;

    private void Awake()
    {
        instance = this;
        SongToRecordDictionary = new Dictionary<Song, Sprite>();
        foreach(var SoToRe in SongToRecordCovers)
        {
            SongToRecordDictionary.Add(SoToRe.Song, SoToRe.RecordCover);
        }
        RecordSize = .9f;
    }
    
    public void CreateRecord(Song song)
    {
        RemoveAllExistingRecords();
        GameObject NewCover = Instantiate(RecordPrefab);
        NewCover.GetComponent<RecordCover>().SetImage(SongToRecordDictionary[song]);
        NewCover.transform.SetParent(RecordParent);
        NewCover.transform.localScale = new Vector3(RecordSize, RecordSize, 0);
        NewCover.transform.position = RecordStart.transform.position;
        NewCover.GetComponent<RecordCover>().MoveCover(RecordEnd.transform.position);
        //StartCoroutine(NewCover.GetComponent<RecordCover>().PrintOut(RecordEnd.transform.position));
    }

    public void RemoveAllExistingRecords()
    {
        for(int i = 0; i < RecordParent.childCount; i++)
        {
            RecordParent.GetChild(i).GetComponent<RecordCover>().PutAwayAndDestroy(RecordStart.transform.position);
        }
    }

    
}

[System.Serializable]
public struct SongToRecordCover
{
    public Song Song;
    public Sprite RecordCover;
}
