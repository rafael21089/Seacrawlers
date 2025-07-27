using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCameraSwitch : MonoBehaviour
{
    [SerializeField] private Transform boatTransform;  // Reference to the boat transform
    [SerializeField] private float switchDistance = 10f;  // Distance at which to switch camera view
    [SerializeField] private Camera boatCamera;  // Reference to the boat camera
    [SerializeField] private Camera playerCamera;  // Reference to the player camera
    [SerializeField] private KeyCode switchKey = KeyCode.E;  // Key to switch camera view
    [SerializeField] GameObject canvasAbilityGO;
    [SerializeField] private Camera boatMiniCamera;

    public movement mv;


    public bool isBoatView = false;  // Flag to indicate if we're in boat view or player view

    void Update()
    {
        // Calculate the distance between the player and the boat
        float distance = Vector3.Distance(transform.position, boatTransform.position);

        // Check if we're within range of the boat and not already in boat view
        if (distance < switchDistance && !isBoatView)
        {
            // Check if the switch key is pressed
            if (Input.GetKeyDown(switchKey))
            {
                // Switch to boat view
                playerCamera.gameObject.SetActive(false);
                boatCamera.gameObject.SetActive(true);

                boatTransform.gameObject.GetComponent<BoatToPlayer>().PlayRandomSong();

                boatTransform.gameObject.GetComponent<BoatToPlayer>().enabled = true;
                boatTransform.gameObject.transform.GetChild(13).GetComponent<TrailRenderer>().enabled = true;



                this.gameObject.SetActive(false);
                canvasAbilityGO.SetActive(false);

                mv.isMoving = false;

                isBoatView = true;
            }
        }
    }
}
