using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToogle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTransform;
    [SerializeField] Color backgroundOnColor;
    [SerializeField] Color backgroundOffColor;
    [SerializeField] Text onText;
    [SerializeField] Text offText;

    [SerializeField] GameObject musicOn;
    [SerializeField] GameObject musicOff;




    Image bgImg;

    Toggle toggle;
    Vector2 handlePosition;

    public static bool muted = false;

    
    

    void Start()
    {
        toggle = GetComponent<Toggle>();
        handlePosition = uiHandleRectTransform.anchoredPosition;
        bgImg = uiHandleRectTransform.parent.GetComponent<Image>();

        toggle.onValueChanged.AddListener(OnSwitch);

        LoadSoundState(); // Carregar estado do som

        // Definir estado do Toggle com base no valor carregado
        toggle.isOn = !muted;

        if (toggle.isOn)
        {
            OnSwitch(true);
        }
        else
        {
            OnSwitch(false);
        }
    }

    void OnSwitch(bool on)
    {
        if (on)
        {
            uiHandleRectTransform.anchoredPosition = handlePosition * 9;
            bgImg.color = backgroundOnColor;
            onText.gameObject.SetActive(true);
            offText.gameObject.SetActive(false);
            musicOn.SetActive(true);
            musicOff.SetActive(false);
            muted = false;
        }
        else
        {
            uiHandleRectTransform.anchoredPosition = handlePosition;
            bgImg.color = backgroundOffColor;
            offText.gameObject.SetActive(true);
            onText.gameObject.SetActive(false);
            musicOff.SetActive(true);
            musicOn.SetActive(false);
            muted = true;
        }

        SaveSoundState(muted);
    }

    void SaveSoundState(bool isSoundMuted)
    {
        int soundState = isSoundMuted ? 1 : 0;
        PlayerPrefs.SetInt("soundState", soundState);
        PlayerPrefs.Save();
    }

    public static void LoadSoundState()
    {
        if (PlayerPrefs.HasKey("soundState"))
        {
            int soundState = PlayerPrefs.GetInt("soundState");
            muted = soundState == 1;
        }
        else
        {
            muted = false; // Valor padrão caso a chave "soundState" não exista
        }
    }
}