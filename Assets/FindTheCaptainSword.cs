using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTheCaptainSword : MonoBehaviour
{
    ObjectiveManager objectiveManager;
    bool playerEntered;
    private QuestManager questManager;

    void Start()
    {
        objectiveManager = FindObjectOfType<ObjectiveManager>();
        questManager = FindObjectOfType<QuestManager>();

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerEntered = true;
            Debug.Log("Player entered");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerEntered = false;
            Debug.Log("Player exited");
        }
    }

    public void DestroyItem()
    {
        if (playerEntered && Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("ObjectDestroys");
            Destroy(gameObject);
            objectiveManager.FindSword();

        }
    }

    public void Update()
    {
        
        DestroyItem();
    }
}
