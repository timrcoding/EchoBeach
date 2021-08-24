using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDropChordChoice : DragAndDrop
{
    Vector3 OriginalPosition;
    public ChordType MChordChoice;
    public ChordPad ChordOver;
    [SerializeField] private Image image;
    void Start()
    {
        OriginalPosition = transform.localPosition;
        image.color = ChordManager.instance.ChordToColorDictionary[MChordChoice];
    }

    // Update is called once per frame
    void Update()
    {
        SetMove();
    }

    public override void StopDrag()
    {
        base.StopDrag();
        transform.localPosition = OriginalPosition;
        if(ChordOver != null)
        {
            ChordOver.SetChord(MChordChoice);
        }
    }

    public override void SetMove()
    {
        base.SetMove();
        if (CanMove && DragCooldown > 0)
        {
            Vector3 Pos = returnCameraPoint(Input.mousePosition) + dragOffset;
            transform.position = Pos;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ChordPad")
        {
            ChordOver = other.GetComponent<ChordPad>();
            ChordOver.SetChord(MChordChoice);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "ChordPad" && CanMove)
        {
            ChordOver.RevertColor();
            ChordOver = null;
        }
        else if(other.tag == "ChordPad" && !CanMove)
        {
            ChordOver.StoreChord();
            ChordOver = null;
        }
    }
}

public enum ChordType
{
    INVALID,
    ChordOne,
    ChordTwo,
    ChordThree,
    ChordFour,
}


