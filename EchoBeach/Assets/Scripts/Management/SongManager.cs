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
    [StringValue("For Her")]
    ForHer,
    [StringValue("Los Angeles")]
    LosAngeles,
    [StringValue("Ever Been")]
    EverBeen,
    [StringValue("See The Moon")]
    SeeTheMoon,
    [StringValue("Harvest Moon")]
    HarvestMoon,
    [StringValue("Delta")]
    Delta,
    [StringValue("Overcoat")]
    Overcoat,
    [StringValue("Jack & Jane")]
    Lettuce,
    [StringValue("Listening")]
    Listening,
    [StringValue("Like A River")]
    River,
    [StringValue("To The End")]
    ToTheEnd,
}
public class SongManager : PulloutManager
{
    public static SongManager instance;

    [SerializeField] private SOMusicLookup SoMusicLookup;

    [SerializeField] private List<PositionToObjectInList> PositionToObjectInLists;
    [SerializeField] private Vector2 Buffer;
    
    public Vector2 GetBuffer { get { return Buffer; } }
    [SerializeField] private Transform SongPlayer;
    [SerializeField] private GameObject SongButtonPrefab;
    [SerializeField] private List<GameObject> SongButtons;
    public List<Song> SongTracklist;
    [SerializeField] private bool SequenceOrShuffle;
    private int SongIndexSelection;

    private LyricManager LyricManager;
    private RecordManager RecordManager;
    public FMOD.Studio.EventInstance musicInstance;
    FMOD.Studio.EventDescription TimeLineDesc;
    [SerializeField] private Slider TimeLineSlider;
    bool CanMoveSlider;
    int TimeLineLength;
    bool StopButtonPressed;
    [FMODUnity.EventRef]
    [SerializeField] private String SongAddedSound;
    [SerializeField] private TextMeshProUGUI SongCounter;
    [SerializeField] private CounterButton CounterButton;

    [SerializeField] private Song[] TrickSongs;
    [SerializeField] private int TrickSongStartOffset = 50000 ;

    public FMOD.Studio.EventInstance AmbienceInstance;
    [FMODUnity.EventRef]
    [SerializeField] private string Ambience;

    [SerializeField] private Slider VolumeSlider;

    //LYRICS
    [SerializeField] private TextMeshProUGUI LyricText;
    [SerializeField] private CanvasGroup LyricCanvasGroup;
    [Range(0, 3)]
    [SerializeField] private float LyricFadeInTime;
    [SerializeField] private RectTransform LyricMask;
    [SerializeField] private float LyricMaskSpeed;


    private void Awake()
    {
        instance = this;
        HardCodeValues();
    }
    void Start()
    {
        LyricManager = GetComponent<LyricManager>();
        RecordManager = GetComponent<RecordManager>();
        SetLyrics("");
        foreach (var song in SaveManager.instance.ActiveSave.SongTracklist)
        {
            SongTracklist.Add(song.Song);
            AddSong(song.LinkName, song.Song, 1);
        };

        AmbienceInstance = FMODUnity.RuntimeManager.CreateInstance(Ambience);
        AmbienceInstance.start();
        SetCounterText();
    }

    #region Manager Setup

    public void SetCounterText()
    {
        if (SongCounter != null)
        {
            if (SaveManager.instance != null)
            {
                int count = SaveManager.instance.ActiveSave.SongPlays;
                SongCounter.text = count.ToString();
                if(count > 30)
                {
                    SongCounter.color = Color.red;
                }
                else
                {
                    SongCounter.color = Color.white;
                }
            }
            else
            {
                SongCounter.text = "0";
            }
        }
        else
        {
            Debug.Log("Song Counter Not Assigned");
        }
    }

    #endregion

    #region Song Playback


    IEnumerator CheckSongIsPlaying()
    {
        yield return new WaitForSeconds(1f);
        FMOD.Studio.PLAYBACK_STATE PlaybackState;
        musicInstance.getPlaybackState(out PlaybackState);
        if(PlaybackState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            StartCoroutine(CheckSongIsPlaying());
        }
        else if (PlaybackState == FMOD.Studio.PLAYBACK_STATE.STOPPED && !StopButtonPressed)
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
        TimeLineSlider.maxValue = TimeLineLength;
    }

    public void GetTimeLineTime()
    {
        int time;
        musicInstance.getTimelinePosition(out time);
        if (!CanMoveSlider)
        {
            TimeLineSlider.value = time;
        }
    }

    public void SetTimeLineTime()
    {
        if (CanMoveSlider)
        {
            musicInstance.setTimelinePosition((int)TimeLineSlider.value);
        }
        int time;
        musicInstance.getTimelinePosition(out time);
        if (time >= TimeLineLength && !StopButtonPressed)
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

    public void ResetSongVolume()
    {
        LeanTween.value(gameObject, VolumeSlider.value, 1, 1).setOnUpdate((value) =>
        {
            VolumeSlider.value = value;
        }).setEaseInQuad();
      
    }

    public void PlaySong(Song song)
    {
        SaveManager.instance.ActiveSave.SongPlays++;
        ResetSongVolume();
        StopSong();
        StopButtonPressed = false;
        RecordManager.CreateRecord(song);
        if (CounterButton != null)
        {
            CounterButton.MoveAndSetCounterText();
        }
        GameSceneManager.instance.PlayClick();
        float vol;
        AmbienceInstance.getVolume(out vol);
        LeanTween.value(gameObject,vol, 0,5).setOnUpdate((value) =>
        {
            AmbienceInstance.setVolume(value);
        });

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

        string evt = SoMusicLookup.SongToFMODictionary[song];
        if (evt != null)
        {
            StartCoroutine(CheckSongIsPlaying());
            musicInstance = FMODUnity.RuntimeManager.CreateInstance(evt);
            musicInstance.start();
            if(TrickSongs.Contains(song))
            {
                musicInstance.setTimelinePosition(7000);
            }
            LyricManager.AssignBeatEvent(musicInstance);
            SetTimeLineSpecs();
            
            for(int i = 0; i < SongTracklist.Count; i++)
            {
                if(song == SongTracklist[i])
                {
                    SongIndexSelection = i;
                }
            }
        }

        foreach(var button in SongButtons)
        {
            if(button.GetComponent<SongButton>().MSong == song)
            {
                button.GetComponent<Button>().image.color = Color.black;
            }
            else
            {
                button.GetComponent<Button>().image.color = Color.white;
            }
        }
    }

    public void StopSong()
    {
        StopButtonPressed = true;
        SetLyrics("");
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
        float vol;
        AmbienceInstance.getVolume(out vol);
        LeanTween.value(gameObject, vol, 1, 5).setOnUpdate((value) =>
        {
            AmbienceInstance.setVolume(value);
        });
        RecordManager.RemoveAllExistingRecords();
    }

    public void AddSong(DeepNetLinkName CharName, Song Song, int num = 0)
    {
        if (!SongTracklist.Contains(Song) || num != 0)
        {
            if (Song != Song.Lettuce || (int)SaveManager.instance.ActiveSave.MTaskNumber >= (int) TaskNumber.Six)
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
                if (num == 0)
                {
                    SongTracklist.Add(Song);
                }
                AddToList(NewSongButton);
                SongButtons.Add(NewSongButton);
                if (SaveManager.instance.ActiveSave.MTaskNumber != TaskNumber.Tutorial)
                {
                    TabManager.instance.SetTab(TabManager.instance.ReturnButton(GetComponent<PulloutManager>()), true);
                }
                if (num == 0)
                {
                    SaveManager.instance.ActiveSave.SongTracklist.Add(new LinkAndSong(CharName, Song));
                }
            }

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
            }
            if (PositionToObjectInLists[i].Object == objOther)
            {
                ObjOtherIndex = i;
            }
        }
        GameObject other = objOther;
        PositionToObjectInLists[ObjOtherIndex].Object = obj;
        PositionToObjectInLists[ObjIndex].Object = other;
    }

    public void ResortSongList()
    {
        SongButtons = SongButtons.OrderBy(x => -x.transform.position.y ).ToList();

        for(int i = 0; i < SongPlayer.childCount; i++)
        {
            SongTracklist[i] = SongButtons[i].GetComponent<SongButton>().MSong;
        }
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
        LyricCanvasGroup.alpha = 0;
        LyricMask.sizeDelta = new Vector2(0, 50);
        if (LyricLine != "//END")
        {
            LyricText.text = LyricLine;
        }
        else
        {
            LyricText.text = "";
        }
        LeanTween.value(gameObject, 0, 1, LyricFadeInTime).setOnUpdate((value) =>
           {
               LyricCanvasGroup.alpha = value;
           });
        LeanTween.value(gameObject, 0, 1, LyricMaskSpeed).setOnUpdate((value) =>
        {
            float right = Mathf.Lerp(0, 800, value);
            LyricMask.sizeDelta = new Vector2(right, 50);
        }).setEaseInQuad();
    }

    #endregion

    private void Update()
    {
        foreach (PositionToObjectInList posObj in PositionToObjectInLists)
        {
            Vector3 pos = posObj.Object.transform.localPosition;
            posObj.Object.transform.localPosition = Vector3.Lerp(pos, posObj.Position, .1f);
        }
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


