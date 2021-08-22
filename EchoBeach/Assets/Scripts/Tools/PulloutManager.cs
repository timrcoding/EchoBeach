using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulloutManager : MonoBehaviour
{
    [SerializeField] protected Vector2 OutPosition;
    [SerializeField] protected Vector2 AwayPosition;
    public Vector2 TargetPosition;
    [SerializeField] public bool outAway;

    public virtual void OutOrAway()
    {
        outAway = !outAway;
        if (outAway)
        {
            TargetPosition = OutPosition;
            transform.SetSiblingIndex(transform.parent.childCount);
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
