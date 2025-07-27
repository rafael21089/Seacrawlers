using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectorController : MonoBehaviour
{
    // Start is called before the first frame update

    private bool canInteract; // flag to indicate if the player is close enough to the NPC to interact with it
    [SerializeField] float interactionDistance = 5f; // the distance at which the player can interact with the NPC
    [SerializeField] GameObject player;
    [SerializeField] Canvas interactionCanvas; // the canvas to be opened when the player interacts with the NPC

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");


    }

    void Update()
    {
        // check if the player is close enough to the NPC to interact with it
        float distance = Vector3.Distance(transform.position, player.transform.position);
        canInteract = distance <= interactionDistance;

        // if the player can interact with the NPC and clicks the left mouse button, open the interaction canvas
        if (canInteract && Input.GetMouseButtonDown(0))
        {
            if (interactionCanvas != null)
            {
                interactionCanvas.gameObject.SetActive(true);
            }
        }

        if (interactionCanvas != null)
        {
            if (interactionCanvas.gameObject.activeInHierarchy && distance > 10)
            {
                interactionCanvas.gameObject.SetActive(false);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                interactionCanvas.gameObject.SetActive(false);
            }
        }
    }
}
