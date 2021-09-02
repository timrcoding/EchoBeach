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
    void Start()
    {
        Button = GetComponent<Button>();
        name = MSong.ToString();
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

    public void StorePosition()
    {
        Debug.Log("POSITION STORED");
        OriginalPosition = transform.position;
    }

     public void PlaySong()
    {
        
        SongManager.instance.PlaySong(MSong);
        SongManager.instance.TurnUpSong();
    }
}
