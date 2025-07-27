using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest")]
public class QuestScriptableObject : ScriptableObject
{
    public enum QuestStatus
    {
        Inactive,
        Active,
        Completed
    }
    
    public string questName;
    public string questDescription;
    public int rewardGold;
    public int rewardExperience;
    public QuestStatus questStatus;

    public List<Objective> objectives = new List<Objective>();
    public int currentObjectiveIndex;
    public int currentObjectiveIndexMonster;
    public Objective currentObjective;

    public Sprite questTypeImg;

    public GameObject[] currentKillingMonster;

    public Sprite img;

    public Objective GetCurrentObjective()
    {
        return objectives[currentObjectiveIndex];
    }

    public GameObject GetCurrentMonsterObjective()
    {
        return currentKillingMonster[currentObjectiveIndexMonster];
    }

    public bool IsComplete()
    {
        return currentObjectiveIndex >= objectives.Count;
    }
}