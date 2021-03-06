using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SongButton : MonoBehaviour
{
    private DeepNetLinkName MDeepNetLinkName;
    public Song MSong;
    [SerializeField] private TextMeshProUGUI TMP;
    private Button Button;
    private Vector3 OriginalPosition;
    private float TimeBetweenClicks;
    void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(PlayPrintSound);
        name = MSong.ToString();
    }

    private void Update()
    {
        TimeBetweenClicks++;
        if(TimeBetweenClicks > 100000)
        {
            ResetClicks();
        }
    }

    private void PlayPrintSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(SearchableDatabaseManager.instance.PrintSound);
    }

    public void SetCharacterAndSong(DeepNetLinkName CharName, Song Song)
    {
        MSong = Song;
        MDeepNetLinkName = CharName;
        TMP.text = $"{StringEnum.GetStringValue(CharName)} : {StringEnum.GetStringValue(Song)}";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GetComponent<DragAndDrop>().ReturnCanMove())
        {
            Debug.Log("Trigger Entered");
            SongManager.instance.SwapObjects(gameObject, other.gameObject);
        }
    }

    public void ResetClicks()
    {
        TimeBetweenClicks = 0;
    }

     public void PlaySong()
    {
        if (TimeBetweenClicks < 10)
        {
            SongManager.instance.PlaySong(MSong);
        }
    }
}
