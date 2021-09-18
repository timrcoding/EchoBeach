using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleLoader : MonoBehaviour
{
    private Image Circle;
    void Start()
    {
        Circle = GetComponent<Image>();
        Circle.fillAmount = Random.Range(0, 360);
        FillCircleLoader();
    }

    void FillCircleLoader()
    {
        float ran = Random.Range(2, 5);
        LeanTween.value(gameObject, 0, 1, ran).setEaseInQuad().setOnUpdate((value) =>
        {
            Circle.fillAmount = value;
        }).setOnComplete(EmptyCircleLoader);
    }

    void EmptyCircleLoader()
    {
        float ran = Random.Range(2, 5);
        LeanTween.value(gameObject, 1, 0, ran).setEaseInQuad().setOnUpdate((value) =>
        {
            Circle.fillAmount = value;
        }).setOnComplete(FillCircleLoader);
    }
}
