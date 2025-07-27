using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    private QuestManager questManager;
    GameObject dagger;
    GameObject captainSword;
    GameObject captainCrown;
    GameObject oats;
    public List<GameObject> objectsWithNameDagger;
    public List<GameObject> objectsWithNameSword;
    public List<GameObject> objectsWithNameCrown;
    public List<GameObject> objectsWithNameOat;
    public bool oneTimeDagger = false;
    public bool oneTimeSword = false;
    public bool oneTimeCrown = false;
    public bool oneTimeOats = false;
    private void Start()
    {
        // Get a reference to the QuestManager script
        questManager = FindObjectOfType<QuestManager>();
        dagger = GameObject.Find("Dagger(Clone)");
        captainSword = GameObject.Find("Captain큦 Sword(Clone)");
        captainCrown = GameObject.Find("Captain큦 Crown(Clone)");
        oats = GameObject.Find("Oats(Clone)");
    }

    public void Update()
    {
        if (oneTimeDagger == false)
        {
            // Find all objects with the given name
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            objectsWithNameDagger = new List<GameObject>();
            oneTimeDagger = true;

            foreach (GameObject obj in allObjects)
            {
                if (obj.name == "Dagger(Clone)")
                {
                    // Add the object to the list
                    objectsWithNameDagger.Add(obj);
                    obj.gameObject.SetActive(false);
                }
            }
        }

        if (oneTimeSword == false)
        {
            // Find all objects with the given name
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            objectsWithNameSword = new List<GameObject>();
            oneTimeSword = true;

            foreach (GameObject obj in allObjects)
            {
                if (obj.name == "Captain큦 Sword(Clone)")
                {
                    // Add the object to the list
                    objectsWithNameSword.Add(obj);
                    obj.gameObject.SetActive(false);
                }
            }
        }


        if (oneTimeCrown == false)
        {
            // Find all objects with the given name
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            objectsWithNameCrown = new List<GameObject>();
            oneTimeCrown = true;

            foreach (GameObject obj in allObjects)
            {
                if (obj.name == "Captain큦 Crown(Clone)")
                {
                    // Add the object to the list
                    objectsWithNameCrown.Add(obj);
                    obj.gameObject.SetActive(false);
                }
            }
        }

        if (oneTimeOats == false)
        {
            // Find all objects with the given name
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            objectsWithNameOat = new List<GameObject>();
            oneTimeOats = true;

            foreach (GameObject obj in allObjects)
            {
                if (obj.name == "Oats(Clone)")
                {
                    // Add the object to the list
                    objectsWithNameOat.Add(obj);
                    obj.gameObject.SetActive(false);
                }
            }
        }

        SetDaggerActive();
        SetSwordActive();
        SetCrownActive();
        SetOatActive();
    }

    public void SetDaggerActive()
    {
        var objective1 = questManager.GetObjectiveByName("Find the Dagger");
        if (objective1 != null && objective1.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            foreach (GameObject obj in objectsWithNameDagger)
            {
                    obj.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach(GameObject obj in objectsWithNameDagger)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }

    public void SetOatActive()
    {
        var objective1 = questManager.GetObjectiveByName("Collect Wheat");
        if (objective1 != null && objective1.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            foreach (GameObject obj in objectsWithNameOat)
            {
                obj.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject obj in objectsWithNameOat)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }

    public void SetCrownActive()
    {
        var objective1 = questManager.GetObjectiveByName("Find the Captain큦 Crown");
        if (objective1 != null && objective1.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            foreach (GameObject obj in objectsWithNameCrown)
            {
                obj.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject obj in objectsWithNameCrown)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }

    public void SetSwordActive()
    {
        var objective1 = questManager.GetObjectiveByName("Find the Captain큦 sword");
        if (objective1 != null && objective1.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            foreach (GameObject obj in objectsWithNameSword)
            {
                obj.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject obj in objectsWithNameSword)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }

    public void Kill10Sandworms()
    {
        var objective = questManager.GetObjectiveByName("Kill 10 Sandworms");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }

    public void Kill15FrostEnemies()
    {
        var objective = questManager.GetObjectiveByName("Kill 15 Enemies in the Frost Lands");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }

    public void Kill3Lizards()
    {
        var objective = questManager.GetObjectiveByName("Kill 3 Lizards");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }

    public void Kill10VulcanicEnemies()
    {
        var objective = questManager.GetObjectiveByName("Kill 10 Vulcanic Enemies");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }

    public void Kill2Mantis()
    {
        var objective = questManager.GetObjectiveByName("Kill 2 Mantis");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }

    public void KillForestBoss()
    {
        var objective = questManager.GetObjectiveByName("Forest Boss");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }

    public void KillVulcanicBoss()
    {
        var objective = questManager.GetObjectiveByName("Defeat the Vulcanic Boss");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }
    public void KillFrostBoss()
    {
        var objective = questManager.GetObjectiveByName("Ice Boss");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }

    public void KillCyclopes()
    {
        var objective = questManager.GetObjectiveByName("Defeat the Cyclopes");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }

    public void FindDagger()
    {
        var objective = questManager.GetObjectiveByName("Find the Dagger");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }

    public void FindCrown()
    {
        var objective = questManager.GetObjectiveByName("Find the Captain큦 Crown");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }

    public void FindSword()
    {
        var objective = questManager.GetObjectiveByName("Find the Captain큦 sword");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }

    public void FindOats()
    {
        var objective = questManager.GetObjectiveByName("Collect Wheat");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }


    public void TalktoKali()
    {
        var objective = questManager.GetObjectiveByName("Talk to Kali");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }

    public void TalktoAlistair()
    {
        var objective = questManager.GetObjectiveByName("Talk to Alistair");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }

    public void ReturntoIsaiah()
    {
        var objective = questManager.GetObjectiveByName("Return to Isaiah");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }

    public void EnterDunes()
    {
        var objective = questManager.GetObjectiveByName("Explore the Dunes");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }

    public void EnterVulcanicWastelands()
    {
        var objective = questManager.GetObjectiveByName("Explore the Vulcanic Wastelands");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }

    public void EnterTropical()
    {
        var objective = questManager.GetObjectiveByName("Explore the Tropical Islands");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }

    public void EnterFrostLands()
    {
        var objective = questManager.GetObjectiveByName("Explore the Frost Lands");
        if (objective != null && objective.status == Objective.ObjectiveStatus.Active) // Check if objective is active
        {
            objective.completionCriteria.currentValue++;
            if (objective.completionCriteria.IsComplete(objective))
            {
                questManager.CompleteObjective(objective);
            }
        }
    }
}