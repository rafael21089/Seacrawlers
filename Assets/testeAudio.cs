using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testeAudio : MonoBehaviour
{
    [SerializeField] AudioSource a;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SwitchToogle.LoadSoundState();

            if (SwitchToogle.muted == false)
            {
                a.Play();
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            a.Stop();
        }
    }
}
