using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluesManager : PulloutManager
{

    private void Awake()
    {
        HardCodeValues();
    }

    public override void OutOrAway()
    {
        base.OutOrAway();
        PutAwayMutuallyExclusiveObjects("MutualPullout");
    }
}
