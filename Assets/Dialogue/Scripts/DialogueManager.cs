using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;
    public QuestGiver[] questGivers;
    public GameObject characterSelectionObject;
    Animator move;


    Message[] currentMessage;
    Actor[] currentActors;
    int activeMessage = 0;
    public static bool isActive = false;
    private QuestGiver questGiver;

    ObjectiveManager objectiveManager;
  
    public void OpenDialogue(Message[] messages, Actor[] actors, QuestGiver questGiver)
    {
        move.SetBool("isMoving", false);
        currentMessage = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        this.questGiver = questGiver;
        Debug.Log("Started Conversation! Loaded messages" + messages.Length);
        DisplayMessage();
        backgroundBox.LeanScale(new Vector3(10.19219f, 1.7184f, 1.0f), 0.5f).setEaseInOutExpo();
    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessage[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessage.Length)
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Conversation ended");
            backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
            isActive = false;
            CharacterSelection characterSelectionScript = characterSelectionObject.GetComponent<CharacterSelection>();
            characterSelectionScript.enabled = true; // Enable the script
            Npc npcScript = characterSelectionObject.GetComponent<Npc>();
            npcScript.enabled = false;
            objectiveManager.TalktoKali();
            

            foreach (QuestGiver qg in questGivers)
            {
                if (qg == questGiver)
                {
                    qg.OpenQuestWindow();
                    qg.AcceptQuest();
                    break;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        move = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        backgroundBox.transform.localScale = Vector3.zero;
        objectiveManager = FindObjectOfType<ObjectiveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isActive == true)
        {
            NextMessage();
        }
    }
}