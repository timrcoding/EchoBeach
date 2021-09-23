using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeIDDragAndDrop : DragAndDrop
{
    public float FullSizeScale = 1;
    public float ShrunkScale = .3f;
    public float TargetScale;
    public int LerpScaler = 5;
    public bool TransferToAnswers;
    private GameObject AnswerArea;
    public Vector3 OriginalPosition;
    private bool CanShrinkOnce;
    public ToggleButton TargetToggle;

    private void Start()
    {
        TargetScale = FullSizeScale;
        transform.localScale =new Vector3(FullSizeScale, FullSizeScale,0);
        OriginalPosition = transform.position;
        GameObject target = GameObject.FindGameObjectWithTag("TargetsToggle");
        TargetToggle = target.GetComponent<ToggleButton>();
    }

    void Update()
    {
        SetMove();
        if(!CanShrinkOnce && Vector2.Distance(transform.position,OriginalPosition) > .5f)
        {
            ShrinkCard();
            CanShrinkOnce = true;
        }
    }

    public void SetTransferToAnswers(bool b, GameObject AnsArea )
    {
        TransferToAnswers = b;
        AnswerArea = AnsArea;
    }

    public override void SetMove()
    {
        base.SetMove();
        if (CanMove && DragCooldown > 0)
        {
            Vector3 Pos= returnCameraPoint(Input.mousePosition) + dragOffset;
            transform.position = Pos;
        }
    }

    public override void StartDrag()
    {
        base.StartDrag();
        transform.SetParent(TableContentsManager.instance.TableCardsParent);
        transform.SetSiblingIndex(transform.parent.childCount);
        TargetToggle.SetTab();

    }

    public override void StopDrag()
    {
        base.StopDrag();
        if (TransferToAnswers)
        {
            AnswerArea.GetComponent<AnswerArea>().SetCharacterID(GetComponent<LargeID>().CharacterID);
            LeanTween.scale(gameObject, Vector3.zero, .3f).setEaseInOutBack().setOnComplete(DestroyObject);
        }
        else
        {
            LeanTween.scale(gameObject, Vector3.zero, .3f).setEaseInOutBack().setOnComplete(DestroyObject);
        }
    }

    void ShrinkCard()
    {
        LeanTween.scale(gameObject,new Vector3(ShrunkScale,ShrunkScale,0),.3f);
        dragOffset *= ShrunkScale;
    }

    void ExpandCard()
    {
        LeanTween.scale(gameObject, new Vector3(FullSizeScale, FullSizeScale, 0), .3f);
    }

    public void DestroyObject()
    {
        StartCoroutine(DestroyAnimation());
    }

    IEnumerator DestroyAnimation()
    {
        LerpScaler = 10;
        TargetScale = TargetScale + .15f;
        yield return new WaitForSeconds(.1f);
        TargetScale = 0;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    
}
