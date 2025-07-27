using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoatToPlayer : MonoBehaviour
{

    [SerializeField] private string teleportTag;  // Tag of the object to teleport to
    [SerializeField] private float teleportDistance = 5f;  // Distance at which to teleport
    [SerializeField] private Transform playerTransform;  // Reference to the player's transform
    [SerializeField] private KeyCode switchKey = KeyCode.V;  // Key to switch camera view
    [SerializeField] private GameObject savePlayer;  // Key to switch camera view
    [SerializeField] GameObject canvasAbilityGO;  // Key to switch camera view

    public Camera CameraBoat;
    public Camera CameraPlayer;

    bool isFirstTime = false;
    [SerializeField] List<AudioSource> navigationMusic;
    AudioSource currentAudioSource;

    public void PlayRandomSong()
    {
        SwitchToogle.LoadSoundState();
        if (SwitchToogle.muted == false)
        {
            // Select a random AudioSource from the list
            int randomIndex = Random.Range(0, navigationMusic.Count);
            currentAudioSource = navigationMusic[randomIndex];

            // Play the selected song
            currentAudioSource.Play();
        }

       
    }

    private void StopCurrentSong()
    {
        // Stop the currently playing song
        if (currentAudioSource != null)
        {
            currentAudioSource.Stop();
            currentAudioSource = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("aaaaa0" + isFirstTime);
        Debug.Log("aaaaa0" + CameraBoat.gameObject.activeSelf);

        
        


        if (playerTransform.GetComponent<BoatCameraSwitch>().isBoatView == true)
        {
            this.gameObject.GetComponent<BoatMovement>().enabled = true;

                if (Input.GetKeyDown(switchKey))
                {

                    Vector3 pos = GetPointOnCircle(transform.position, teleportDistance);

                    if (pos != Vector3.zero)
                    {
                        // Switch to boat view
                        StopCurrentSong();
                        playerTransform.gameObject.SetActive(true);
                        CameraBoat.gameObject.SetActive(false);
                        CameraPlayer.gameObject.SetActive(true);
                        canvasAbilityGO.SetActive(true);
                        playerTransform.gameObject.GetComponent<NavMeshAgent>().enabled = false;


                        Vector3 targetPosition = pos;
                        savePlayer.transform.position = targetPosition;

                        playerTransform.gameObject.GetComponent<NavMeshAgent>().enabled = true;

                        playerTransform.GetComponent<BoatCameraSwitch>().isBoatView = false;
                        this.gameObject.GetComponent<BoatMovement>().enabled = false;
                        this.gameObject.GetComponent<BoatToPlayer>().enabled = false;
                        this.gameObject.transform.GetChild(13).GetComponent<TrailRenderer>().enabled = false;
                    } 

                }      
            
        }
        else
        {
            this.gameObject.GetComponent<BoatMovement>().enabled = false;

        }

    }

    Vector3 GetPointOnCircle(Vector3 center, float radius)
    {
        int attempts = 0;

        

        while (attempts < 100)
        {
            Vector3 position = center + Random.insideUnitSphere * radius;
            RaycastHit hit;

            if (Physics.Raycast(position, Vector3.down, out hit, 100f, LayerMask.GetMask("Island")))
            {
                if (hit.transform.tag == "island")
                {
                    Debug.Log(hit.transform.tag);
                    return hit.point;
                }
                
            }

            attempts++;
        }

        Debug.Log("Could not find island after 100 attempts.");
        return Vector3.zero;
    }
}
