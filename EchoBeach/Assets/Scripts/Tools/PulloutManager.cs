using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulloutManager : MonoBehaviour
{
    [SerializeField] protected Vector2 OutPosition;
    [SerializeField] protected Vector2 AwayPosition;
    public Vector2 TargetPosition;
    [SerializeField] public bool outAway;
    [SerializeField] private bool DontChangeSibling;

    protected void HardCodeValues()
    {
        OutPosition = new Vector2(OutPosition.x, 200);
        AwayPosition = new Vector2(AwayPosition.x,200);
        TargetPosition = AwayPosition;
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

    public IEnumerator PutAway(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        outAway = false;
        TargetPosition = AwayPosition;
    }

    public void PutOut()
    {
        outAway = true;
        TargetPosition = OutPosition;
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
                    StartCoroutine(obj.GetComponent<PulloutManager>().PutAway(0));
                }
            }
        }
    }
}
