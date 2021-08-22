using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SongManager : PulloutManager
{
    public static SongManager instance;

    [SerializeField] private List<PositionToObjectInList> PositionToObjectInLists;
    [SerializeField] private Vector2 Buffer;
    
    public Vector2 GetBuffer { get { return Buffer; } }
    [SerializeField] private Transform SongPlayer;
    [SerializeField] private GameObject SongButtonPrefab;
    [SerializeField] private List<GameObject> SongButtons;
    public List<Song> SongTracklist;
    void Start()
    {
        instance = this;
        //Buffer.y = SongButtonPrefab.GetComponent<RectTransform>().rect.height/3;
        TargetPosition = AwayPosition;
    }

    public override void OutOrAway()
    {
        base.OutOrAway();
        PutAwayMutuallyExclusiveObjects("MutualPullout");
    }
    

    public void AddSong(CharacterName CharName, Song Song, int num = 0)
    {
        if (!SongTracklist.Contains(Song) || num != 0)
        {
            GameObject NewSongButton = Instantiate(SongButtonPrefab);
            NewSongButton.name = Random.Range(0, 10000).ToString();
            NewSongButton.transform.SetParent(SongPlayer);
            if (PositionToObjectInLists.Count != 0)
            {
                NewSongButton.transform.localPosition = PositionToObjectInLists[PositionToObjectInLists.Count - 1].Position;
            }
            else
            {
                NewSongButton.transform.localPosition = new Vector3(Buffer.x, Buffer.y, 0);
            }
            NewSongButton.transform.localScale = Vector3.one;
            NewSongButton.GetComponent<SongButton>().SetCharacterAndSong(CharName, Song);
            SongTracklist.Add(Song);
            AddToList(NewSongButton);
            SongButtons.Add(NewSongButton);
        }
    }

    private void Update()
    {
        foreach (PositionToObjectInList posObj in PositionToObjectInLists)
        {
            Vector3 pos = posObj.Object.transform.localPosition;
            posObj.Object.transform.localPosition = Vector3.Lerp(pos, posObj.Position, .1f);  
        }

        transform.localPosition = Vector2.Lerp(transform.localPosition, TargetPosition, Time.deltaTime * 5);
    }

    public void SwapObjects(GameObject obj, GameObject objOther)
    {
        int ObjIndex = new int(); ;
        int ObjOtherIndex = new int();

        for(int i = 0; i < PositionToObjectInLists.Count; i++)
        {
            if(PositionToObjectInLists[i].Object == obj)
            {
                ObjIndex = i;
                Debug.Log("OBJ" + i);
            }
            if (PositionToObjectInLists[i].Object == objOther)
            {
                ObjOtherIndex = i;
                Debug.Log("OBJOTHER" + i);
            }
        }
        GameObject other = objOther;
        PositionToObjectInLists[ObjOtherIndex].Object = obj;
        PositionToObjectInLists[ObjIndex].Object = other;

    }


    void AddToList(GameObject Obj)
    {
        float ButtonYBounds = SongButtonPrefab.GetComponent<RectTransform>().rect.height;
        PositionToObjectInLists.Add(new PositionToObjectInList(new Vector2(Buffer.x, -Buffer.y - ButtonYBounds * PositionToObjectInLists.Count), Obj));
    }

    void SortAndTransferButtons()
    {
        SongButtons = SongButtons.OrderBy(x => x.transform.localPosition.y).ToList();
        for(int i = 0; i < SongButtons.Count; i++)
        {
            PositionToObjectInLists[i].Object = SongButtons[i];
        }
    }
}

[System.Serializable]
public class PositionToObjectInList
{
    public PositionToObjectInList(Vector2 pos, GameObject obj)
    {
        Position = pos;
        Object = obj;
    }

    public Vector2 Position;
    public GameObject Object;
}
