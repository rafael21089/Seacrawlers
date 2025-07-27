using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestGoal 
{
    public GoalType goalType;

    public int requiredAmount = 0;
    public int currentAmount = 0;

    public bool isReached() 
    {
        Debug.Log(currentAmount);
        Debug.Log(requiredAmount);
        return (currentAmount >= requiredAmount);
    }

    public void EnemyKilled() 
    {
        if (goalType == GoalType.Kill) 
        {
            currentAmount++;
            Debug.Log("bruh");
        }
        
    }
}

public enum GoalType 
{
    Kill,
    Talk
}
