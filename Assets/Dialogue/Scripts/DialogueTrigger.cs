using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] message;
    public Actor[] actors;

    public void StartDialogue() 
    {
        QuestGiver questGiver = GetComponent<QuestGiver>();
        FindObjectOfType<DialogueManager>().OpenDialogue(message, actors, questGiver);
    }
}

    [System.Serializable]
    public class Message 
    {
        public int actorId;
        public string message;
    }

    [System.Serializable]
    public class Actor
    {
        public string name;
        public Sprite sprite;
    }

