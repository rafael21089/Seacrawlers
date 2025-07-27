using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsChangePage : MonoBehaviour
{
    [SerializeField] Canvas audioCanvas;
    [SerializeField] Canvas commandsCanvas;
    [SerializeField] Canvas creditsCanvas;

    public void ChangeToAudio()
    {
        audioCanvas.gameObject.SetActive(true);
        commandsCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(false);
    }

    public void ChangeToCommands()
    {
        commandsCanvas.gameObject.SetActive(true);
        audioCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(false);
    }

    public void ChangeToCredits()
    {
        creditsCanvas.gameObject.SetActive(true);
        audioCanvas.gameObject.SetActive(false);
        commandsCanvas.gameObject.SetActive(false);
    }
}
