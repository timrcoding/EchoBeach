using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SongButton : MonoBehaviour
{
    private Dictionary<CharacterName, Song> CharacterAndSongDictionary;
    [SerializeField] private TextMeshProUGUI TMP;
    private FMODUnity.StudioEventEmitter AudioSource;
    void Start()
    {
        CharacterAndSongDictionary = new Dictionary<CharacterName, Song>();
    }

    public void SetCharacterAndSong(CharacterName CharName, Song Song)
    {
       // CharacterAndSongDictionary.Add(CharName, Song);
        TMP.text = $"{CharName} : {Song}";
    }

    //THIS WILL REQUIRE A LOOKUP FOR SONG TO FMOD EVENT

    void SetEmitter()
    {
        AudioSource.Event = "";
    }

    public void PlayEmitter()
    {
        AudioSource.Play();
    }
    
    public void PauseEmitter()
    {
        //SAVE POSITION
    }

    public void StopEmitter()
    {
        AudioSource.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GetComponent<DragAndDrop>().ReturnCanMove())
        {
            Debug.Log("Trigger Entered");
            SongManager.instance.SwapObjects(gameObject, other.gameObject);
        }
    }
}
