using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewObjective", menuName = "Objective")]
public class Objective : ScriptableObject
{
    [System.Serializable]
    public struct CompletionCriteria
    {
        public int requiredValue;
        public int currentValue;

        public bool IsComplete(Objective obj)
        {
            if (obj.status == ObjectiveStatus.Inactive)
            {
                currentValue = 0;
                return false;
            }
            return currentValue >= requiredValue;
        }
    }

    public string objectiveName;
    public string objectiveDescription;
    public ObjectiveStatus status;
    public QuestType questType;
    public QuestPlace questPlace;
    public int rewardGold;
    public float rewardExperience;

    public CompletionCriteria completionCriteria;


    public enum QuestType
    {
        Killing,
        Exploring,
        Collecting,
    }

    public enum ObjectiveStatus
    {
        Inactive,
        Active,
        Completed,
    }

    public enum QuestPlace
    {
        Lava,
        Ice,
        Desert,
        Forest,
        Base,
        None,
    }

    public void SetInactive()
    {
        status = ObjectiveStatus.Inactive;
        completionCriteria.currentValue = 0;
    }

    public void Start()
    {
        status = ObjectiveStatus.Active;
        completionCriteria.currentValue = 0;
    }
}