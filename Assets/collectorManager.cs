using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class collectorManager : MonoBehaviour
{
    // Start is called before the first frame update

    public QuestScriptableObject[] questsCollectable;
    public QuestScriptableObject[] questsTreasure;
    public QuestScriptableObject[] questsOcean;
    public GameObject prefab;

    public QuestManager questManager;

    private int day = 0;

    private int counter = 0;

    public GameObject g;
    public GameObject g2;
    public GameObject g3;

    public QuestScriptableObject questEscolhida1;
    public QuestScriptableObject questEscolhida2;
    public QuestScriptableObject questEscolhida3;

    void Start()
    {
        SpawnCollectable();

        SpawnTreasure();

        SpawnOcean();
    }

    // Update is called once per frame
    void Update()
    {
        if (g.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "Obtained")
        {
            if (questManager.GetComponent<QuestManager>().isActiveQuest(questEscolhida1) == false)
            {
                Destroy(g);
                SpawnCollectable();
            }
        }

        if (g2.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "Obtained")
        {
            if (questManager.GetComponent<QuestManager>().isActiveQuest(questEscolhida2) == false)
            {
                Destroy(g2);

                SpawnTreasure();
            }
        }

        if (g3.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "Obtained")
        {
            if (questManager.GetComponent<QuestManager>().isActiveQuest(questEscolhida3) == false)
            {
                Destroy(g3);

                SpawnOcean();
            }
        }

    }

 

    void SpawnCollectable()
    {
        g = Instantiate(prefab);
        int randomIndex = Random.Range(0, questsCollectable.Length);

        g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questsCollectable[randomIndex].questName;
        g.transform.GetChild(1).GetComponent<Image>().sprite = questsCollectable[randomIndex].img;

        g.transform.GetChild(2).GetComponent<buttonStartQuest>().quest = questsCollectable[randomIndex];
        g.transform.GetChild(2).GetComponent<buttonStartQuest>().questManager = questManager;
        g.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = questsCollectable[randomIndex].questDescription;
        g.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Gold Reward: " + questsCollectable[randomIndex].rewardGold + "\n Xp: " + questsCollectable[randomIndex].rewardExperience;


        questEscolhida1 = questsCollectable[randomIndex];

        g.transform.parent = transform;
    }

    void SpawnTreasure()
    {
        g2 = Instantiate(prefab);
        int randomIndex2 = Random.Range(0, questsTreasure.Length);

        g2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questsTreasure[randomIndex2].questName;
        g2.transform.GetChild(1).GetComponent<Image>().sprite = questsTreasure[randomIndex2].img;

        g2.transform.GetChild(2).GetComponent<buttonStartQuest>().quest = questsTreasure[randomIndex2];
        g2.transform.GetChild(2).GetComponent<buttonStartQuest>().questManager = questManager;
        g2.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = questsTreasure[randomIndex2].questDescription;
        g2.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Gold Reward: " + questsTreasure[randomIndex2].rewardGold + "\n Xp: " + questsTreasure[randomIndex2].rewardExperience;

        questEscolhida2 = questsTreasure[randomIndex2];

        g2.transform.parent = transform;
    }

    void SpawnOcean()
    {
        g3 = Instantiate(prefab);
        int randomIndex3 = Random.Range(0, questsOcean.Length);

        g3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questsOcean[randomIndex3].questName;
        g3.transform.GetChild(1).GetComponent<Image>().sprite = questsOcean[randomIndex3].img;

        g3.transform.GetChild(2).GetComponent<buttonStartQuest>().quest = questsOcean[randomIndex3];
        g3.transform.GetChild(2).GetComponent<buttonStartQuest>().questManager = questManager;
        g3.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = questsOcean[randomIndex3].questDescription;
        g3.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Gold Reward: " + questsOcean[randomIndex3].rewardGold + "\n Xp: " + questsOcean[randomIndex3].rewardExperience;

        questEscolhida3 = questsOcean[randomIndex3];

        g3.transform.parent = transform;
    }




}
