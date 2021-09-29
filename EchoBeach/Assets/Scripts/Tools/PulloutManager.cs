using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulloutManager : MonoBehaviour
{
    [SerializeField] private Vector2 OutPosition;
    public Vector2 GetOutPosition { get { return OutPosition; } }

    [SerializeField] private Vector2 AwayPosition;
    public Vector2 GetAwayPosition { get { return AwayPosition; } }
    [SerializeField] private bool DontChangeSibling;

    protected void HardCodeValues()
    {
        OutPosition = new Vector2(-355, 100);
        AwayPosition = new Vector2(-355,-860);
    }
    public void PutAway()
    {
        LeanTween.moveLocal(gameObject, AwayPosition, .5f).setEaseInOutBack();
    }

    public void PutOut()
    {
        LeanTween.moveLocal(gameObject, OutPosition, .5f).setEaseInOutBack();
        transform.SetSiblingIndex(transform.parent.childCount);
    }

    public void PutAwayMutuallyExclusiveObjects(string tag = null)
    {
        GameObject[] MutuallyExclusiveObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in MutuallyExclusiveObjects)
        {
            if (obj != gameObject)
            {
                if (obj.GetComponent<PulloutManager>())
                {
                    obj.GetComponent<PulloutManager>().PutAway();
                }
            }
        }
    }
}
