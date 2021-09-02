using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManDragAndDrop : DragAndDrop
{
    public override void StartDrag()
    {
        base.StartDrag();
        transform.SetSiblingIndex(transform.parent.childCount);
    }

    private void Update()
    {
        SetMove();
    }

    public override void SetMove()
    {
        base.SetMove();
        if (CanMove && DragCooldown > 0)
        {
            float Returned = returnCameraPoint(Input.mousePosition).y + dragOffset.y;
            transform.position = new Vector3(transform.position.x, Returned, 0);
        }
    }

    public override void StopDrag()
    {
        base.StopDrag();
        SongManager.instance.ResortSongList();
    }
}
