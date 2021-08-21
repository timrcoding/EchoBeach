using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeepnetManager : MonoBehaviour
{
    [SerializeField] private GameObject ScreenBlackout;
    private Vector3 TargetScale;
    [SerializeField] private bool ScreenIsOn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ScreenIsOn)
        {
            TargetScale = Vector3.one;
        }
        else
        {
            TargetScale = Vector3.zero;
        }

        ScreenBlackout.transform.localScale = Vector3.Lerp(ScreenBlackout.transform.localScale, TargetScale, Time.deltaTime * 5);
    }

    public void SwitchScreenOn()
    {
        if (ScreenIsOn)
        {
            TargetScale = Vector3.one;
        }
        else
        {
            TargetScale = Vector3.zero;
        }
    }
}
