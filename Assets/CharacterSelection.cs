using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public Button gunnerButton;
    public Button swordsmanButton;
    public Button mageButton;
    public Text classChoosed;
    ObjectiveManager objectiveManager;
    //public Canvas Class;

    [SerializeField] float interactionDistance = 2f; // the distance at which the player can interact with the NPC
    [SerializeField] Canvas interactionCanvas; // the canvas to be opened when the player interacts with the NPC
    [SerializeField] GameObject player;
    private bool canInteract; // flag to indicate if the player is close enough to the NPC to interact with it

    public static string selectedClass = "";

    [SerializeField] GameObject hB;

    void Start()
    {
        
        // disable the interaction canvas at start
        if (interactionCanvas != null)
        {
            interactionCanvas.gameObject.SetActive(false);
        }
        objectiveManager = FindObjectOfType<ObjectiveManager>();
        // Add listeners to each button
        //gunnerButton.onClick.AddListener(SelectGunner);
        //swordsmanButton.onClick.AddListener(SelectSwordsman);
        //mageButton.onClick.AddListener(SelectMage);
    }

    void Update()
    {
        // check if the player is close enough to the NPC to interact with it
        float distance = Vector3.Distance(transform.position, player.transform.position);
        canInteract = distance <= interactionDistance;

        // if the player can interact with the NPC and clicks the left mouse button, open the interaction canvas
        if (canInteract && Input.GetMouseButtonDown(0) && selectedClass == "")
        {
            if (interactionCanvas != null)
            {
                interactionCanvas.gameObject.SetActive(true);
                hB.SetActive(false);
                objectiveManager.TalktoAlistair();
            }
        }
    }

    public void SelectGunner()
    {
        player.GetComponent<movement>().classeEscolhida = movement.Class.Gunner;
        selectedClass = "Gunner";
        classChoosed.text = selectedClass;
        interactionCanvas.gameObject.SetActive(false);
        hB.SetActive(true);
        Debug.Log("classeG: " + selectedClass);
    }

    public void SelectSwordsman()
    {
        player.GetComponent<movement>().classeEscolhida = movement.Class.Swordsman;
        selectedClass = "Swordsman";
        classChoosed.text = selectedClass;
        interactionCanvas.gameObject.SetActive(false);
        hB.SetActive(true);
        Debug.Log("classeS: " + selectedClass);
    }
    public void SelectMage()
    {
        player.GetComponent<movement>().classeEscolhida = movement.Class.Mage;
        selectedClass = "Mage";
        classChoosed.text = selectedClass;
        interactionCanvas.gameObject.SetActive(false);
        hB.SetActive(true);
        Debug.Log("classeM: " + selectedClass);
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(selectedClass == null)
                interactionCanvas.gameObject.SetActive(true);
        }
    }*/
}
