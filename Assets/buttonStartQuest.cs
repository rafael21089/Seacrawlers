using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class buttonStartQuest : MonoBehaviour
{

    public QuestScriptableObject quest;
    public QuestManager questManager;
    
    public void StartingTheQuest()
    {

        if (!questManager.activeQuests.Contains(quest))
        {

            questManager.StartQuest(quest);

            transform.GetComponent<Button>().interactable = false;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Obtained";
        }

    }
}
