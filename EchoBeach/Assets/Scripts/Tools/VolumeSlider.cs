using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private string VCAName;
    private Slider Slider;

    private void Start()
    {
        Slider = GetComponent<Slider>();
        Slider.value = 1;
    }
    public void ChangeSongVolume()
    {
        string vcaPath = VCAName;
        FMOD.Studio.VCA vca = FMODUnity.RuntimeManager.GetVCA(vcaPath);
        vca.setVolume(Slider.value);
    }
}
