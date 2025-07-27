using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PopulateIsland : MonoBehaviour
{


    public GameObject[] addOnIslands; // The prefabs to be spawned
    public int numberOfAddonIslands; // The number of times to spawn each prefab

    public GameObject[] objectsToSpawn; // The prefabs to be spawned
    public int[] numberOfObjects; // The number of times to spawn each prefab

    public GameObject island;
    public GameObject islandChild;

    private GameObject player;
    public float radius;                    // the radius around the island to spawn objects
    public LayerMask layerMask;             // the layer mask to use for raycasts

    public string navMeshTag = "island";               // the tag of the navmesh to check for spawning

    private Vector3[] spawnPoints;          // array to store the spawn points
    private int totalSpawnedObjects;        // total number of objects that have been spawned


    public float minSpawnDistance; // minimum distance between spawn points


    public GameObject spawnerParaMiniIlhas;


    private GameObject objects;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        objects = GameObject.FindGameObjectWithTag("obj");

        for (int i = 1; i <= 8; i++)
        {
            if (island.name == "Island" + i + "(Clone)")
            {
                islandChild = island.transform.GetChild(i - 1).gameObject;
            }
            else if (island.name == "mini island " + i + "(Clone)")
            {
                islandChild = island.transform.GetChild(i - 1).gameObject;
            }
        }


        // create a new bounds object
        //Bounds meshBounds = islandChild.GetComponent<MeshFilter>().mesh.bounds;



        if (addOnIslands.Length > 0)
        {
            for (int i = 0; i < numberOfAddonIslands; i++)
            {
                GameObject islandAddOnsToSpawn = addOnIslands[Random.Range(0, addOnIslands.Length - 1)];
                Quaternion rot = Quaternion.identity;
                Vector3 spawnPosition = GetRandomSpawnPoint(rot);
                GameObject newObject = Instantiate(islandAddOnsToSpawn, spawnPosition, rot);

                //spawnPosition.y = 0.5f;


                GameObject newObject2 = Instantiate(spawnerParaMiniIlhas, spawnPosition, rot);

                newObject2.GetComponent<ItemAreaSpawner>().islandPraMini = newObject;

                //newObject2.transform.parent = 

            }
        }

        GameObject newObject3 = Instantiate(objects, island.transform.position, island.transform.rotation);


        for (int i = 0; i < objectsToSpawn.Length; i++)
        {
            for (int j = 0; j < numberOfObjects[i]; j++)
            {
                Quaternion rot = Quaternion.identity;
                Vector3 spawnPosition = GetRandomSpawnPoint(rot);
                if (spawnPosition != Vector3.zero)
                {
                    GameObject newObject = Instantiate(objectsToSpawn[i], spawnPosition, rot);

                    newObject.transform.parent = newObject3.transform;
                }
            }
        }
    }



    // Helper method to get a random point within the bounds of the specified layer
    private Vector3 GetRandomSpawnPoint(Quaternion rot)
    {

        // Get the bounds of the specified layer
        MeshRenderer renderer = islandChild.GetComponent<MeshRenderer>();
        Bounds bounds = renderer.bounds;

        // Calculate the direction from the island's center to the player's position
        Vector3 playerPos = player.transform.position;
        Vector3 islandCenter = transform.position;
        Vector3 playerDirection = (playerPos - islandCenter).normalized;

        // Determine which quarter of the bounds is opposite to the player
        Vector3 oppositeQuarter = new Vector3(
            Mathf.Abs(playerDirection.x) > 0.5f ? -1 : 1,
            Mathf.Abs(playerDirection.y) > 0.5f ? -1 : 1,
            Mathf.Abs(playerDirection.z) > 0.5f ? -1 : 1
        );

        // Generate a random point in the opposite quarter of the bounds

        int tries = 0;

        Vector3 randomPoint = Vector3.zero;

        while (tries <= 50)
        {
            randomPoint = Random.insideUnitSphere;
            randomPoint.Scale(bounds.extents);
            randomPoint.Scale(new Vector3(
                Mathf.Abs(oppositeQuarter.x),
                Mathf.Abs(oppositeQuarter.y),
                Mathf.Abs(oppositeQuarter.z)
            ));
            randomPoint = transform.TransformPoint(randomPoint);

            // Cast a ray downwards from the random position to find a point within the layer bounds
            RaycastHit hit;
            if (Physics.Raycast(randomPoint, Vector3.down, out hit, Mathf.Infinity, NavMesh.AllAreas))
            {
                if (hit.collider.tag == navMeshTag)
                {
                    randomPoint.y = hit.transform.position.y;

                    rot = hit.transform.rotation;

                    return hit.point;
                }
            }

            tries++;

        }

        rot = Quaternion.identity;

        // If no point is found, return the random position
        return randomPoint;
    }



}
