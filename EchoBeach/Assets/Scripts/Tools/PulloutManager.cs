using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulloutManager : MonoBehaviour
{
    [SerializeField] public Vector2 OutPosition;
    [SerializeField] public Vector2 AwayPosition;
    public Vector2 TargetPosition;
    [SerializeField] public bool outAway;
    [SerializeField] private bool DontChangeSibling;

    protected void HardCodeValues()
    {
        OutPosition = new Vector2(-850, 300);
        AwayPosition = new Vector2(-1450,300);
    }

    public virtual void OutOrAway()
    {
        outAway = !outAway;
        if (outAway)
        {
            TargetPosition = OutPosition;
            if (!DontChangeSibling)
            {
                transform.SetSiblingIndex(transform.parent.childCount);
            }
        }
        else
        {
            TargetPosition = AwayPosition;
        }
    }

    public void PutAway()
    {
        LeanTween.moveLocal(gameObject, AwayPosition, .5f).setEaseInOutBack();
    }

    public void PutOut()
    {
        LeanTween.moveLocal(gameObject, OutPosition, .5f).setEaseInOutBack();
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
