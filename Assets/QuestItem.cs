using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    // Start is called before the first frame update

    public QuestScriptableObject quest;
    public QuestManager QuestManager;

    public bool isAlreadyOn = false;

    void Start()
    {
        QuestManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();

        // Get the Renderer component attached to the GameObject
        Renderer renderer = this.GetComponent<Renderer>();

        // If the Renderer component is null, search the children for a Renderer
        if (renderer == null)
        {
            SetVisibilityRecursive(this.gameObject, false);
        }

        // If a Renderer was found, set its visibility
        if (renderer != null)
        {
            renderer.enabled = false; // makes the GameObject visible
        }
    }

    private void Update()
    {
        if (QuestManager.isActiveQuest(quest) && isAlreadyOn == false)
        {
            isAlreadyOn = true;

            // Get the Renderer component attached to the GameObject
            Renderer renderer = this.GetComponent<Renderer>();

            // If the Renderer component is null, search the children for a Renderer
            if (renderer == null)
            {
                SetVisibilityRecursive(this.gameObject, true);
            }

            // If a Renderer was found, set its visibility
            if (renderer != null)
            {
                renderer.enabled = false; // makes the GameObject visible
            }
        }
        else if (isAlreadyOn == true && !QuestManager.isActiveQuest(quest))
        {
            isAlreadyOn = false;
            // Get the Renderer component attached to the GameObject
            Renderer renderer = this.GetComponent<Renderer>();

            // If the Renderer component is null, search the children for a Renderer
            if (renderer == null)
            {
                SetVisibilityRecursive(this.gameObject , false);
            }

            // If a Renderer was found, set its visibility
            if (renderer != null)
            {
                renderer.enabled = false; // makes the GameObject visible
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") || other.CompareTag("boat"))
        {
            if (QuestManager.isActiveQuest(quest))
            {
                Debug.Log("Completou");
                QuestManager.CompleteObjective(quest.currentObjective);
                Destroy(this.gameObject);
            }
        }
    }


    // Define a function to recursively search for Renderer components
    void SetVisibilityRecursive(GameObject obj, bool visible)
    {
        // Get the Renderer component attached to the GameObject
        Renderer renderer = obj.GetComponent<Renderer>();

        // If a Renderer was found, set its visibility
        if (renderer != null)
        {
            renderer.enabled = visible; // makes the GameObject visible or invisible
        }

        // Recursively search through all the children of the GameObject
        foreach (Transform child in obj.transform)
        {
            SetVisibilityRecursive(child.gameObject, visible);
        }
    }
}
