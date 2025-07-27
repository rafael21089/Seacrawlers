using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public List<QuestScriptableObject> quests = new List<QuestScriptableObject>();
    private Dictionary<QuestScriptableObject, Objective> currentObjectives = new Dictionary<QuestScriptableObject, Objective>();
    public List<QuestScriptableObject> activeQuests = new List<QuestScriptableObject>();
    public List<QuestScriptableObject> completedQuests = new List<QuestScriptableObject>();
    public GameObject questWindow;
    private PlayerRewards goldReward;
    private PlayerRewards xpReward;
    public PlayerRewards goldRewardComponent;
    public PlayerRewards xpRewardComponent;

    public List<QuestScriptableObject> activeQuestsTemp;

    public GameObject prefab;

    public GameObject[] listaDeQuestUi;
    public GameObject[] listaDeQuestUiAntiga;


    private void Start()
    {

        goldReward = goldRewardComponent;
        xpReward = xpRewardComponent;
        // Set all objectives to inactive when the game starts
        foreach (var quest in quests)
        {
            foreach (var objective in quest.objectives)
            {
                objective.SetInactive();
                
            }
        }

        activeQuestsTemp = new List<QuestScriptableObject>(activeQuests);
        StartQuest(quests[0]);

    }

    public void Update()
    {
        listaDeQuestUi = new GameObject[activeQuests.Count];
        int i = 0;


        //Elimina os null , os que foram eliminados na lista de gameobjects
        for (int d = 0; d < listaDeQuestUiAntiga.Length; d++)
        {
            if (listaDeQuestUiAntiga[d] == null)
            {
                // Move down one position
                for (int j = d; j < listaDeQuestUiAntiga.Length - 1; j++)
                {
                    listaDeQuestUiAntiga[j] = listaDeQuestUiAntiga[j + 1];
                }
                listaDeQuestUiAntiga[listaDeQuestUiAntiga.Length - 1] = null;
            }
        }


        if (activeQuests != null)
        {
            // Check if anything new was added to the list
            foreach (QuestScriptableObject item in activeQuests)
            {
                if (!activeQuestsTemp.Contains(item))
                {

                    GameObject g = Instantiate(prefab);
                    g.transform.parent = questWindow.transform;
                    g.name = item.currentObjective.objectiveDescription;
                    g.GetComponent<TextMeshProUGUI>().text = item.questName;
                    Debug.Log("item quest name:" + item.questName);
                    g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.currentObjective.objectiveDescription;
                    g.transform.GetChild(2).GetComponent<Image>().sprite = item.questTypeImg;
                    g.transform.GetChild(3).name = item.currentObjective.questPlace.ToString();

                    listaDeQuestUi[i] = g.gameObject;
                    i++;
                }
                else
                {

                    listaDeQuestUi[i] = listaDeQuestUiAntiga[i];
                    i++;
                }

            }


            QuestDescriptionUpdater(listaDeQuestUi, activeQuests);
            
        }

        


        // Update the previous objects list
        activeQuestsTemp = new List<QuestScriptableObject>(activeQuests);
        listaDeQuestUiAntiga = (GameObject[])listaDeQuestUi.Clone();

    }


    public void QuestDescriptionUpdater(GameObject[] lista , List<QuestScriptableObject> listaQuestAtivas)
    {

        for (int i = 0; i < lista.Length; i++)
        {
            foreach (QuestScriptableObject item in listaQuestAtivas)
            {
                if (item.questName == lista[i].GetComponent<TextMeshProUGUI>().text)
                {
                    if (lista[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text != item.currentObjective.objectiveDescription)
                    {
                        lista[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.currentObjective.objectiveDescription;
                    }
                }
            }
        }




    }


    public void StartQuest(QuestScriptableObject quest)
    {
        if (quest == null) return;

        // Mark quest as active
        quest.questStatus = QuestScriptableObject.QuestStatus.Active;

        // Set current objective to first objective in list
        quest.currentObjectiveIndex = 0;
        quest.currentObjectiveIndexMonster = 0;

        // Set current objective as active
        quest.currentObjective = quest.objectives[quest.currentObjectiveIndex];
        quest.currentObjective.status = Objective.ObjectiveStatus.Active;
        currentObjectives[quest] = quest.currentObjective;

        quest.currentObjective.completionCriteria.currentValue = 0;
        // Check if it is the same one

        if (!activeQuests.Contains(quest))
        {
           activeQuests.Add(quest);
            
        }
        

    }
    public void CompleteObjective(Objective objective)
    {
        if (objective == null) return;

        // Get the quest for this objective
        var quest = GetQuestForObjective(objective);

        // Mark the current objective as completed
        objective.status = Objective.ObjectiveStatus.Completed;

        //give gold to player 

        goldReward.IncreaseGold(objective.rewardGold);
        xpReward.SetExperience(objective.rewardExperience);

        // Increment the current objective index for this quest

        quest.currentObjectiveIndex++;

        // Check if the quest is complete
        if (quest.IsComplete())
        {
            CompleteQuest(quest);

            // Remove completed quest from UI
            foreach (Transform child in questWindow.transform)
            {
                if (child.GetComponent<TextMeshProUGUI>() != null && child.GetComponent<TextMeshProUGUI>().text == quest.questName)
                {
                    Destroy(child.gameObject);
                    break; 
                }
            }
        }
        else
        {
            // Set the next objective as the current objective for this quest
            quest.currentObjective = quest.GetCurrentObjective();
            quest.currentObjective.status = Objective.ObjectiveStatus.Active;
            currentObjectives[quest] = quest.currentObjective;
            quest.currentObjective.completionCriteria.currentValue = 0;

        }
    }


    public Objective GetObjectiveByName(string objectiveName)
    {
        foreach (var quest in quests)
        {
            foreach (var objective in quest.objectives)
            {
                if (objective.objectiveName == objectiveName)
                {
                    return objective;
                }
            }
        }
        return null;
    }

    public QuestScriptableObject GetQuestByName(string questName)
    {
        foreach (var quest in quests)
        {
            if (quest.questName == questName)
            {
                return quest;
            }
        }
        return null;
    }

    public void CompleteQuest(QuestScriptableObject quest)
    {
        // Mark quest as completed
        quest.questStatus = QuestScriptableObject.QuestStatus.Completed;

        // Remove quest from current objectives
        currentObjectives.Remove(quest);
        activeQuests.Remove(quest);
        activeQuestsTemp.Remove(quest);

        completedQuests.Add(quest);
    }

    public Objective GetCurrentObjective(QuestScriptableObject quest)
    {
        if (quest == null) return null;

        return currentObjectives.ContainsKey(quest) ? currentObjectives[quest] : null;
    }

    private QuestScriptableObject GetQuestForObjective(Objective objective)
    {
        foreach (var quest in activeQuests)
        {
            if (quest.objectives.Contains(objective))
            {
                return quest;
            }
        }

        return null;
    }


    public bool isActiveQuest(QuestScriptableObject quest2)
    {
        foreach (var quest in activeQuests)
        {
            if (quest == quest2)
            {
                return true;
            }
        }

        return false;
    }


    public void Counter(GameObject enemy)
    {
        if (activeQuests.Count > 0)
        {
            foreach (var quest in activeQuests)
            {
                if (quest.currentObjective.questType == Objective.QuestType.Killing)

                    if (quest.currentKillingMonster != null)
                    {
                        
                        if ((quest.currentKillingMonster[quest.currentObjectiveIndexMonster].gameObject.name +"(Clone)") == enemy.name  || quest.currentKillingMonster[quest.currentObjectiveIndexMonster].gameObject.name == enemy.name)
                        {
                            quest.currentObjective.completionCriteria.currentValue++;

                            if (quest.currentObjective.completionCriteria.IsComplete(quest.currentObjective))
                            {
                                CompleteObjective(quest.currentObjective);
                                quest.currentObjectiveIndexMonster++;
                            }
                        }
                    }

            }
        }
    }
       

 }

