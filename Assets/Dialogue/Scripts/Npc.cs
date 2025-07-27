using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public DialogueTrigger trigger;
    ObjectiveManager objectiveManager;

    [SerializeField] private float interactionDistance = 2f; // the distance at which the player can interact with the NPC
    [SerializeField] GameObject player;

    private bool canInteract; // flag to indicate if the player is close enough to the NPC to interact with it

    //private void OnTriggerEnter(Collider collision) 
    //{
    //    if (collision.gameObject.CompareTag("Player") == true)
    //        trigger.StartDialogue();
        
    //}

    void Update()
    {
        // check if the player is close enough to the NPC to interact with it
        float distance = Vector3.Distance(transform.position, player.transform.position);
        canInteract = distance <= interactionDistance;

        // if the player can interact with the NPC and clicks the left mouse button, open the interaction canvas
        if (canInteract && Input.GetMouseButtonDown(0))
        {
            trigger.StartDialogue();
        }
    }

}
