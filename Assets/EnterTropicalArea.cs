using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTropicalArea : MonoBehaviour
{
    ObjectiveManager objectiveManager;
    bool playerEntered;

    void Start()
    {
        objectiveManager = FindObjectOfType<ObjectiveManager>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerEntered = true;
            Debug.Log("Player entered");
            objectiveManager.EnterTropical();
        }
    }
}
