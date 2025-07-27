using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIslands : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;


    public GameObject[] prefabsToSpawnIce;
    public GameObject[] prefabsToSpawnLava;
    public GameObject[] prefabsToSpawnSand;
    public GameObject[] prefabsToSpawnForest;

    public GameObject[] listOfprefabspawners;
    public GameObject prefabToSpawn2;
    public int numPrefabsToSpawn;
    public float minDistanceBetweenPrefabs;

    int counter = 0;
    public float innerRadiusFactor; // A value between 0 and 1 that specifies the size of the inner radius relative to the outer radius


    public void Awake()
    {
        ShuffleArray(listOfprefabspawners);
    }

    public void generateIslands(Vector3 posi, float radius , string spawner)
    {

            if (spawner == "Spawner1")
            {
                prefabToSpawn2 = listOfprefabspawners[0];
                prefabsToSpawn = Finder(listOfprefabspawners[0]);

            }
            else if (spawner == "Spawner2")
            {
                prefabToSpawn2 = listOfprefabspawners[1];
                prefabsToSpawn = Finder(listOfprefabspawners[1]);


            }
            else if (spawner == "Spawner3")
            {
                prefabToSpawn2 = listOfprefabspawners[2];
                prefabsToSpawn = Finder(listOfprefabspawners[2]);


            }
            else if (spawner == "Spawner4")
            {
                prefabToSpawn2 = listOfprefabspawners[3];
                prefabsToSpawn = Finder(listOfprefabspawners[3]);


            }



        float innerRadius = radius * innerRadiusFactor; // Calculate the size of the inner radius


        for (int i = 0; i < numPrefabsToSpawn; i++)
        {
           
            // Generate a random position between the two radii
            float angle = Random.Range(0f, 360f);
            float randomRadius = Random.Range(innerRadius, radius);
            Vector3 position = posi + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * randomRadius;

            // Check if the position is too close to any previously spawned prefabs
            bool positionIsValid = true;
            foreach (Transform child in transform)
            {
                if (Vector3.Distance(position, child.position) < minDistanceBetweenPrefabs)
                {
                    positionIsValid = false;
                    break;
                }
            }

            // If the position is valid, spawn the prefab and add it as a child of this object
            if (positionIsValid)
            {
                position.y = -2f;
                counter = 0;

                GameObject prefabToSpawn = prefabsToSpawn[Random.Range(1,6)];
                GameObject newPrefab = Instantiate(prefabToSpawn, position, Quaternion.identity);
                GameObject newPrefab2 = Instantiate(prefabToSpawn2, position, Quaternion.identity);
                newPrefab.transform.parent = transform;

                newPrefab2.transform.parent = newPrefab.transform;
            }
            else
            {

                counter++;
                if (counter <= 100)
                {
                    i--;
                }
            }
        }
    }


    void ShuffleArray(GameObject[] array)
    {
        System.Random random = new System.Random();

        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = random.Next(i + 1);

            GameObject temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }


    public GameObject[] Finder(GameObject isl)
    {

        for (int i = 0; i < 4; i++)
        {
            if (isl.name == "ForestItemSpreader")
            {
                return prefabsToSpawnForest;
            }
            else if (isl.name == "IceItemSpreader")
            {
                return prefabsToSpawnIce;
            }
            else if (isl.name == "LavaItemSpreader")
            {
                return prefabsToSpawnLava;
            }
            else if (isl.name == "SandItemSpreader")
            {
                return prefabsToSpawnSand;
            }
        }

        return prefabsToSpawnForest;


    }


    
}
