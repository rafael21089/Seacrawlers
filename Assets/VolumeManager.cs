using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] Slider sliderVolume;
    [SerializeField] GameObject volumeOn;
    [SerializeField] GameObject volumeOff;

    private void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            LoadVolumeState();
        }
        else
        {
            LoadVolumeState();
        }

        ChangeIconsONOFF();
    }

    void ChangeIconsONOFF()
    {
        if (sliderVolume.value > 0)
        {
            volumeOn.SetActive(true);
            volumeOff.SetActive(false);
        }
        else
        {
            volumeOff.SetActive(true);
            volumeOn.SetActive(false);
        }
    }

    public void ChangeVolumeState()
    {
        AudioListener.volume = sliderVolume.value;
        ChangeIconsONOFF();
        SaveVolumeState();
    }

    void SaveVolumeState()
    {
        PlayerPrefs.SetFloat("musicVolume", sliderVolume.value);
    }

    void LoadVolumeState()
    {
        sliderVolume.value = PlayerPrefs.GetFloat("musicVolume");
    }
}
