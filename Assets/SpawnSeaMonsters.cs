using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSeaMonsters : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;
    public int[] numPrefabsToSpawn;
    public float minDistanceBetweenPrefabs;

    public string islandTag; // The tag of the objects to avoid spawning on

    public float innerRadiusFactor; // A value between 0 and 1 that specifies the size of the inner radius relative to the outer radius

    int counter = 0;
    private GameObject objects;

    private void Start()
    {
        objects = GameObject.FindGameObjectWithTag("obj");
    }

    public void generateSeaMonsters(float radius)
    {
        GameObject[] islands = GameObject.FindGameObjectsWithTag(islandTag);
        float innerRadius = radius * innerRadiusFactor;

        for (int i = 0; i < prefabsToSpawn.Length; i++)
        {
            for (int j = 0; j < numPrefabsToSpawn[i]; j++)
            {
                float angle = Random.Range(0f, 360f);
                float randomRadius = Random.Range(innerRadius, radius);
                Vector3 position = transform.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * randomRadius;

                bool positionIsValid = true;
                foreach (Transform child in transform)
                {
                    if (Vector3.Distance(position, child.position) < minDistanceBetweenPrefabs)
                    {
                        positionIsValid = false;
                        break;
                    }
                }
                foreach (GameObject island in islands)
                {
                    if (Vector3.Distance(position, island.transform.position) < minDistanceBetweenPrefabs)
                    {
                        positionIsValid = false;
                        break;
                    }
                }

                if (positionIsValid)
                {
                    position.y = 0f;
                    counter = 0;
                    //GameObject newObject3 = Instantiate(objects, position, Quaternion.identity);

                    GameObject newPrefab = Instantiate(prefabsToSpawn[i], position, Quaternion.identity);

                    //newPrefab.transform.parent = newObject3.transform;

                    newPrefab.transform.parent = transform;

                }
                else
                {
                    counter++;
                    if (counter <= 100)
                    {
                        j--;
                    }
                }
            }
        }
    }
}
