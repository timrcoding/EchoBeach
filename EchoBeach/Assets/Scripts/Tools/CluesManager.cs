using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluesManager : PulloutManager
{
    private void Update()
    {
        transform.localPosition = Vector2.Lerp(transform.localPosition, TargetPosition, Time.deltaTime * 5);
    }

    public override void OutOrAway()
    {
        base.OutOrAway();
        PutAwayMutuallyExclusiveObjects("MutualPullout");
    }
}
