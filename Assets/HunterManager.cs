using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HunterManager : MonoBehaviour
{
    // Start is called before the first frame update

    public QuestScriptableObject[] questsLavaIsland;
    public QuestScriptableObject[] questsIceIsland;
    public QuestScriptableObject[] questsDesertIsland;
    public QuestScriptableObject[] questsForestIsland;
    public GameObject prefab;

    public QuestManager questManager;

    private int day = 0;

    private int counter = 0;


    public GameObject g;
    public GameObject g2;
    public GameObject g3;
    public GameObject g4;

    public QuestScriptableObject questEscolhida1;
    public QuestScriptableObject questEscolhida2;
    public QuestScriptableObject questEscolhida3;
    public QuestScriptableObject questEscolhida4;

    void Start()
    {
        spawnLava();
        spawnIce();
        spawnDesert();
        spawnForest();
    }

    // Update is called once per frame
    void Update()
    {

        if (g.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "Obtained")
        {
            if (questManager.GetComponent<QuestManager>().isActiveQuest(questEscolhida1) == false)
            {
                Destroy(g);
                spawnLava();
            }
        }

        if (g2.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "Obtained")
        {
            if (questManager.GetComponent<QuestManager>().isActiveQuest(questEscolhida2) == false)
            {
                Destroy(g2);

                spawnIce();
            }
        }

        if (g3.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "Obtained")
        {
            if (questManager.GetComponent<QuestManager>().isActiveQuest(questEscolhida3) == false)
            {
                Destroy(g3);

                spawnDesert();
            }
        }

        if (g4.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "Obtained")
        {
            if (questManager.GetComponent<QuestManager>().isActiveQuest(questEscolhida4) == false)
            {
                Destroy(g4);

                spawnForest();
            }
        }

    }




    void spawnLava()
    {
        g = Instantiate(prefab);
        int randomIndex = Random.Range(0, questsLavaIsland.Length);

        g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questsLavaIsland[randomIndex].questName;
        g.transform.GetChild(1).GetComponent<Image>().sprite = questsLavaIsland[randomIndex].img;
        g.transform.GetChild(2).GetComponent<buttonStartQuest>().quest = questsLavaIsland[randomIndex];
        g.transform.GetChild(2).GetComponent<buttonStartQuest>().questManager = questManager;
        g.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = questsLavaIsland[randomIndex].questDescription;

        g.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Gold Reward: " + questsLavaIsland[randomIndex].rewardGold + "\n Xp: " + questsLavaIsland[randomIndex].rewardExperience; 

        questEscolhida1 = questsLavaIsland[randomIndex];


        g.transform.parent = transform;
    }

    void spawnIce()
    {
        g2 = Instantiate(prefab);
        int randomIndex2 = Random.Range(0, questsIceIsland.Length);

        g2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questsIceIsland[randomIndex2].questName;
        g2.transform.GetChild(1).GetComponent<Image>().sprite = questsIceIsland[randomIndex2].img;

        g2.transform.GetChild(2).GetComponent<buttonStartQuest>().quest = questsIceIsland[randomIndex2];
        g2.transform.GetChild(2).GetComponent<buttonStartQuest>().questManager = questManager;
        g2.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = questsIceIsland[randomIndex2].questDescription;
        g2.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Gold Reward: " + questsIceIsland[randomIndex2].rewardGold + "\n Xp: " + questsIceIsland[randomIndex2].rewardExperience;

        questEscolhida2 = questsIceIsland[randomIndex2];

        g2.transform.parent = transform;
    }


    void spawnDesert()
    {
        g3 = Instantiate(prefab);
        int randomIndex3 = Random.Range(0, questsDesertIsland.Length);

        g3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questsDesertIsland[randomIndex3].questName;
        g3.transform.GetChild(1).GetComponent<Image>().sprite = questsDesertIsland[randomIndex3].img;

        g3.transform.GetChild(2).GetComponent<buttonStartQuest>().quest = questsDesertIsland[randomIndex3];
        g3.transform.GetChild(2).GetComponent<buttonStartQuest>().questManager = questManager;
        g3.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = questsDesertIsland[randomIndex3].questDescription;
        g3.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Gold Reward: " + questsDesertIsland[randomIndex3].rewardGold + "\n Xp: " + questsDesertIsland[randomIndex3].rewardExperience;

        questEscolhida3 = questsDesertIsland[randomIndex3];

        g3.transform.parent = transform;
    }

    void spawnForest()
    {
        g4 = Instantiate(prefab);
        int randomIndex4 = Random.Range(0, questsForestIsland.Length);

        g4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questsForestIsland[randomIndex4].questName;
        g4.transform.GetChild(1).GetComponent<Image>().sprite = questsForestIsland[randomIndex4].img;

        g4.transform.GetChild(2).GetComponent<buttonStartQuest>().quest = questsForestIsland[randomIndex4];
        g4.transform.GetChild(2).GetComponent<buttonStartQuest>().questManager = questManager;
        g4.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = questsForestIsland[randomIndex4].questDescription;
        g4.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Gold Reward: " + questsForestIsland[randomIndex4].rewardGold + "\n Xp: " + questsForestIsland[randomIndex4].rewardExperience;

        questEscolhida4 = questsForestIsland[randomIndex4];

        g4.transform.parent = transform;
    }

    
}
