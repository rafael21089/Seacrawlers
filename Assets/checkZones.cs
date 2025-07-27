using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class checkZones : MonoBehaviour
{
    bool alreadyStarted = false, alreadyDid = false;


    GameObject iceObject, forestObject, sandObject, lavaObject;
    float closestDistance = float.MaxValue;
    GameObject closestObject = null;

    public Image[] arrows;//N, arrowS, arrowW, arrowE, arrowNE, arrowNW, arrowSE, arrowSW;

    
    void Update()
    {
        if (!alreadyDid && GameObject.Find("LavaItemSpreaderOverlap(Clone)") != null  && GameObject.Find("SandItemSpreaderOverlap(Clone)") != null && GameObject.Find("ForestItemSpreaderOverlap(Clone)") != null && GameObject.Find("IceItemSpreaderOverlap(Clone)") != null)
        {
            lavaObject = GameObject.Find("LavaItemSpreaderOverlap(Clone)");
            sandObject = GameObject.Find("SandItemSpreaderOverlap(Clone)");
            forestObject = GameObject.Find("ForestItemSpreaderOverlap(Clone)");
            iceObject = GameObject.Find("IceItemSpreaderOverlap(Clone)");
            alreadyStarted = true;
   
            if (alreadyStarted)
            {
                float distanceToLava = Vector3.Distance(transform.position, lavaObject.transform.position);
    
                if (distanceToLava < closestDistance)
                {
                    closestDistance = distanceToLava;
                    closestObject = lavaObject;
                }

                float distanceToSand = Vector3.Distance(transform.position, sandObject.transform.position);
                if (distanceToSand < closestDistance)
                {
                    closestDistance = distanceToSand;
                    closestObject = sandObject;
                }

                float distanceToForest = Vector3.Distance(transform.position, forestObject.transform.position);
                if (distanceToForest < closestDistance)
                {
                    closestDistance = distanceToForest;
                    closestObject = forestObject;
                }

                float distanceToIce = Vector3.Distance(transform.position, iceObject.transform.position);
                if (distanceToIce < closestDistance)
                {
                    closestDistance = distanceToIce;
                    closestObject = iceObject;
                }

                if (closestObject != null)
                {
                    // Change the name of the closest object
                    if(closestObject == iceObject)
                        gameObject.name = "IceZone";
                    else if (closestObject == forestObject)
                        gameObject.name = "ForestZone";
                    else if (closestObject == sandObject)
                        gameObject.name = "SandZone";
                    else if (closestObject == lavaObject)
                        gameObject.name = "LavaZone";
                }

                alreadyDid = true;

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("boat"))
        {
            if (gameObject.name == "IceZone")
            {
                for (int i = 0; i < arrows.Length; i++)
                {
                    // Check if the current game object's color is yellow
                    if (arrows[i].color == Color.blue)
                    {
                        // Perform actions for the yellow object
                        arrows[i].gameObject.SetActive(false);
                    }
                }
            }
            else if (gameObject.name == "ForestZone")
            {
                for (int i = 0; i < arrows.Length; i++)
                {
                    // Check if the current game object's color is yellow
                    if (arrows[i].color == Color.green)
                    {
                        // Perform actions for the yellow object
                        arrows[i].gameObject.SetActive(false);
                    }
                }
            }
            else if (gameObject.name == "SandZone")
            {
                
                for (int i = 0; i < arrows.Length; i++)
                {
                    // Check if the current game object's color is yellow
                    if (arrows[i].color == Color.yellow)
                    {
                        // Perform actions for the yellow object
                        arrows[i].gameObject.SetActive(false);
                    }
                }
            }
            else if (gameObject.name == "LavaZone")
            {
                for (int i = 0; i < arrows.Length; i++)
                {
                    // Check if the current game object's color is yellow
                    if (arrows[i].color == Color.red)
                    {
                        // Perform actions for the yellow object
                        arrows[i].gameObject.SetActive(false);
                    }
                }
            }

                
        }
    }
}
