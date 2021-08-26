using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System;
using TMPro;



public enum Song
{
    INVALID,
    [StringValue("Angelina")]
    Angelina,
    [StringValue("Los Angeles")]
    LosAngeles,
    [StringValue("Cowboy")]
    Cowboy,
    [StringValue("See The Moon")]
    SeeTheMoon,
    [StringValue("Ever Been")]
    EverBeen,
}
public class SongManager : PulloutManager
{
    public static SongManager instance;

    [SerializeField] private SOMusicLookup SoMusicLookup;

    [SerializeField] private List<PositionToObjectInList> PositionToObjectInLists;
    [SerializeField] private Vector2 Buffer;
    [SerializeField] private FMODUnity.StudioEventEmitter RadioAudioSource;
    [SerializeField] private Toggle RadioToggle;
    
    public Vector2 GetBuffer { get { return Buffer; } }
    [SerializeField] private Transform SongPlayer;
    [SerializeField] private GameObject SongButtonPrefab;
    [SerializeField] private List<GameObject> SongButtons;
    public List<Song> SongTracklist;
    public bool SequenceOrShuffle;
    public int SongIndexSelection;

    [SerializeField] private LyricManager LyricManager;
    FMOD.Studio.EventInstance musicInstance;
    FMOD.Studio.EventDescription TimeLineDesc;
    [SerializeField] private Slider Slider;
    bool CanMoveSlider;
    int TimeLineLength;

    //LYRICS
    [SerializeField] private TextMeshProUGUI LyricText;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        LyricManager = GetComponent<LyricManager>();
        TargetPosition = AwayPosition;
        HardCodeValues();
        
    }

    #region Manager Setup

    public override void OutOrAway()
    {
        base.OutOrAway();
        PutAwayMutuallyExclusiveObjects("MutualPullout");
    }

    #endregion

    #region Song Playback

    public void SetRadioOrMusic()
    {
        if (RadioToggle.isOn)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Volume", 1);
            Debug.Log("SET");
        }
        else
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Volume", 0);
        }
    }

    IEnumerator CheckSongIsPlaying()
    {
        yield return new WaitForSeconds(1f);
        FMOD.Studio.PLAYBACK_STATE PlaybackState;
        musicInstance.getPlaybackState(out PlaybackState);
        if(PlaybackState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            StartCoroutine(CheckSongIsPlaying());
        }
        else if (PlaybackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        {
            SelectSong();
        }
    }

    public void MoveSlider(bool b)
    {
        CanMoveSlider = b;
    }

    public void SetTimeLineSpecs()
    {
        musicInstance.getDescription(out TimeLineDesc);
        TimeLineDesc.getLength(out TimeLineLength);
        Slider.maxValue = TimeLineLength;
    }

    public void GetTimeLineTime()
    {
        int time;
        musicInstance.getTimelinePosition(out time);
        if (!CanMoveSlider)
        {
            Slider.value = time;
        }
    }

    public void SetTimeLineTime()
    {
        if (CanMoveSlider)
        {
            musicInstance.setTimelinePosition((int)Slider.value);
        }
        int time;
        musicInstance.getTimelinePosition(out time);
        if (time >= TimeLineLength)
        {
            SelectSong();
        }
    }

    void SelectSong()
    {
        if (SequenceOrShuffle)
        {
            if (SongIndexSelection < SongTracklist.Count)
            {
                SongIndexSelection += 1;
            }
            else
            {
                SongIndexSelection = 0;
            }
        }
        else
        {
            SongIndexSelection = returnNewSelection(SongIndexSelection);
        }

        Song SongChoice = SongTracklist[SongIndexSelection];
        PlaySong(SongChoice);
    }

    int returnNewSelection(int num)
    {
        int newNum = UnityEngine.Random.Range(0, SongTracklist.Count);
        return newNum;
    }

    public void TurnUpSong()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Volume", 0);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Mute", 0);
    }

    public void PlaySong(Song Song)
    {
        
        FMOD.Studio.PLAYBACK_STATE PlaybackState;
        musicInstance.getPlaybackState(out PlaybackState);

        if (PlaybackState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            Debug.Log("Song Stopped");
            musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        else
        {
            Debug.Log("Song Stopped");
        }

        string evt = SoMusicLookup.SongToFMODictionary[Song];
        if (evt != null)
        {
            StartCoroutine(CheckSongIsPlaying());
            musicInstance = FMODUnity.RuntimeManager.CreateInstance(evt);
            musicInstance.start();
            LyricManager.AssignBeatEvent(musicInstance);
            SetTimeLineSpecs();
        }
    }

    public void AddSong(DeepNetLinkName CharName, Song Song, int num = 0)
    {
        if (!SongTracklist.Contains(Song) || num != 0)
        {
            GameObject NewSongButton = Instantiate(SongButtonPrefab);
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

#endregion

    #region SongSetup;

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

    #endregion

    #region Lyrics

    public void SetLyrics(string LyricLine)
    {
        LyricText.text = LyricLine;
        StartCoroutine(StartSet(LyricLine.Length));
        IEnumerator StartSet(int num)
        {
            for (int i = 0; i <= num; i++)
            {
               LyricText.maxVisibleCharacters = i;
               yield return new WaitForSeconds(.05f);
            }
        }
    }

    #endregion

    private void Update()
    {
        foreach (PositionToObjectInList posObj in PositionToObjectInLists)
        {
            Vector3 pos = posObj.Object.transform.localPosition;
            posObj.Object.transform.localPosition = Vector3.Lerp(pos, posObj.Position, .1f);
        }
        transform.localPosition = Vector2.Lerp(transform.localPosition, TargetPosition, Time.deltaTime * 5);

        GetTimeLineTime();
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

class TimelineInfo
{
    public bool ended = false;
    public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
}


