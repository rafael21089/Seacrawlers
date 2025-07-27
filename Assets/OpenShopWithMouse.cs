using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShopWithMouse : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 2f; // the distance at which the player can interact with the NPC
    [SerializeField] private Canvas interactionCanvas; // the canvas to be opened when the player interacts with the NPC
    [SerializeField] GameObject player;
    [SerializeField] GameObject canvas;

    private bool canInteract; // flag to indicate if the player is close enough to the NPC to interact with it

    void Start()
    {
        // disable the interaction canvas at start
        if (interactionCanvas != null)
        {
            interactionCanvas.gameObject.SetActive(false);
        }
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
                canvas.SetActive(false);
            }
        }
    }

    public void CloseShop()
    {
        Debug.Log("aabbcc");
        interactionCanvas.gameObject.SetActive(false);
        canvas.SetActive(true);

    }
}
