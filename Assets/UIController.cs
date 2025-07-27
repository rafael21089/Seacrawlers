using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;
    public Button _musicBtnOn, _musicBtnOFF, _sfxBtnON, _sfxBtnOFF;

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();

        if(_musicBtnOFF.gameObject.activeInHierarchy)
        {
            _musicBtnOn.gameObject.SetActive(true);
            _musicBtnOFF.gameObject.SetActive(false);
        }
        else
        {
            _musicBtnOn.gameObject.SetActive(false);
            _musicBtnOFF.gameObject.SetActive(true);
        }
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();

        if (_sfxBtnOFF.gameObject.activeInHierarchy)
        {
            _sfxBtnON.gameObject.SetActive(true);
            _sfxBtnOFF.gameObject.SetActive(false);
        }
        else
        {
            _sfxBtnON.gameObject.SetActive(false);
            _sfxBtnOFF.gameObject.SetActive(true);
        }
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.MusicVolume(_sfxSlider.value);
    }
}
