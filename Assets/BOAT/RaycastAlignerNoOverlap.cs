using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastAlignerNoOverlap : MonoBehaviour
{
    public GameObject[] itemsToPickFrom;
    public float raycastDistance = 100f;
    public float overlapTestBoxSize = 1f;
    public LayerMask spawnedObjectLayer;
    public int maxSpawnTries = 10;

    public int order;



    // Start is called before the first frame update
    public void PositionRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            // Get the size of the prefab
            Vector3 prefabSize = itemsToPickFrom[order].GetComponentInChildren<Renderer>().bounds.size;

            // Set the overlap test box size to the maximum size of the prefab
            float overlapTestBoxSize = Mathf.Max(prefabSize.x, prefabSize.y, prefabSize.z);
            Vector3 overlapTestBoxScale = new Vector3(overlapTestBoxSize, overlapTestBoxSize, overlapTestBoxSize);

            // Try to spawn the object at the hit point
            Collider[] collidersInsideOverlapBox = new Collider[1];
            int numberOfCollidersFound = Physics.OverlapBoxNonAlloc(hit.point, overlapTestBoxScale, collidersInsideOverlapBox, spawnRotation, spawnedObjectLayer);

            if (numberOfCollidersFound == 0)
            {
               
                Pick(hit.point, spawnRotation);
            }
            else
            {
                // If there is a collider found, keep trying to spawn the object in random locations until a suitable location is found
                int numSpawnTries = 0;
                while (numSpawnTries < maxSpawnTries)
                {
                    Vector3 randomOffset = new Vector3(Random.Range(-overlapTestBoxSize / 2f, overlapTestBoxSize / 2f), 0f, Random.Range(-overlapTestBoxSize / 2f, overlapTestBoxSize / 2f));
                    Vector3 randomPoint = hit.point + randomOffset;

                    numberOfCollidersFound = Physics.OverlapBoxNonAlloc(randomPoint, overlapTestBoxScale, collidersInsideOverlapBox, spawnRotation, spawnedObjectLayer);

                    if (numberOfCollidersFound == 0)
                    {
                        Pick(randomPoint, spawnRotation);
                        break;
                    }

                    numSpawnTries++;
                }
            }
        }

        Destroy(this.gameObject);
    }




    void Pick(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        if (order == 0 || order == 2)
        {
            positionToSpawn.y = -0.1f;
        }
        else
        {
            positionToSpawn.y = 2;
        }

        GameObject clone = Instantiate(itemsToPickFrom[order], positionToSpawn, Quaternion.identity);



    }
}
