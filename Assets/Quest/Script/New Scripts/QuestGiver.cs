using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    public QuestScriptableObject quest;
    public GameObject questWindow;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public QuestManager questManager;

    public GameObject prefab;
    
    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);

    }

    public void AcceptQuest()
    {
        questManager.StartQuest(quest);
        
    }
}
