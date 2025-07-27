using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPositions : MonoBehaviour
{
    public GameObject[] zones;
     Transform main;
    public GameObject mainObjt;
    bool alreadyDid = false;
    private void Update()
    {
        if(zones[0].name != "zone1" && zones[1].name != "zone2" && zones[2].name != "zone3" && zones[3].name != "zone4" && !alreadyDid)
            FindClosestObject();
    }

    public void FindClosestObject()
    {
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject obj in zones)
        {
            float distance = Vector3.Distance(obj.transform.position, transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }

        mainObjt.name = closestObject.name;
        alreadyDid = true;
        Debug.Log("name is:" + closestObject.name);
    }
}
