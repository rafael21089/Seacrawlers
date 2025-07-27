using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfPlay : MonoBehaviour
{
    [SerializeField] AudioSource audioMainMenu;
    void Start()
    {
        SwitchToogle.LoadSoundState();

        if (SwitchToogle.muted == false)
        {
            audioMainMenu.Play();
        }
    }

    
}
