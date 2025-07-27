using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartTheTrials : MonoBehaviour
{

    public QuestScriptableObject quest;
    public GameObject questWindow;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public QuestManager questManager;

    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        OpenQuestWindow1();
        AcceptQuest1();
        
    }

    public void OpenQuestWindow1()
    {
        questWindow.SetActive(true);
        
    }

    public void AcceptQuest1()
    {
        questManager.StartQuest(quest);

    }

}

