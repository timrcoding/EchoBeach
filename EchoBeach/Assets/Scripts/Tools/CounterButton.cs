using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CounterButton : MonoBehaviour
{
    [SerializeField] private GameObject CounterText;
    [SerializeField] private Transform DownPos;
    [SerializeField] private Transform UpPos;
    public void MoveAndSetCounterText()
    {
        MoveCenterToDown();
    }

    void MoveCenterToDown()
    {
        LeanTween.value(gameObject, 0, 1, .5f).setOnUpdate((value) =>
           {
               CounterText.transform.localPosition = Vector3.Lerp(Vector3.zero, DownPos.localPosition, value);
           }).setEaseInOutBack().setOnComplete(MoveUpToCenter);
    }

    void MoveUpToCenter()
    {
        SongManager.instance.SetCounterText();
        LeanTween.value(gameObject, 0, 1, .5f).setOnUpdate((value) =>
        {
            CounterText.transform.localPosition = Vector3.Lerp(UpPos.localPosition, Vector3.zero, value);
        }).setEaseOutBounce();
    }
}
