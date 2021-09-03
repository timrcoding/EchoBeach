using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;

    [SerializeField] private Volume Volume;
    private DepthOfField DOF;

    private void Awake()
    {
        instance = this;
        Volume.profile.TryGet<DepthOfField>(out DOF);
    }
    public void BlurBackground()
    {
        LeanTween.value(gameObject, 20, 0, 1).setOnUpdate((value) =>
        {
            DOF.focusDistance.value = value;
        });
    }

    public void UnBlurBackground()
    {
        LeanTween.value(gameObject, 0, 20, 1).setOnUpdate((value) =>
        {
            DOF.focusDistance.value = value;
        });
    }
}
