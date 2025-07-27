using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class GenerateIslandObjects : MonoBehaviour
{

    [SerializeField] GameObject[] prefab;
    [Header("Raycast Settings")]
    [SerializeField] int densityRocks;
    [SerializeField] int densityEnemies;
    [Space]
    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;
    [SerializeField] Vector2 xRangeRocks;
    [SerializeField] Vector2 zRangeRocks;
    [SerializeField] Vector2 xRangeEnemies;
    [SerializeField] Vector2 zRangeEnemies;
    //[Header("Prefab Variation Settings")]
    //[SerializeField, Range(0, 1)] float rotateTowardsNormal;
    //[SerializeField] Vector2 rotationRange;
    //[SerializeField] Vector3 minScale;
    //[SerializeField] Vector3 maxScale;



    public void Start()
    {
        //test();


    }


    public void test()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
    public GameObject GenerateRocks()
    {

            float sampleX = Random.Range(xRangeRocks.x, xRangeRocks.y);
            float sampley = Random.Range(zRangeRocks.x, zRangeRocks.y);
            Vector3 rayStart = new Vector3(sampleX + transform.position.x, maxHeight, sampley + transform.position.z);

            RaycastHit hit;

            if (!Physics.Raycast(rayStart, Vector3.down, out hit, Mathf.Infinity))
            {
                Debug.Log("not hitted");


                GameObject instantiatedPrefab2 = Instantiate(prefab[0], rayStart, Quaternion.identity);
                return instantiatedPrefab2;
            }
            else
            {
                    Debug.Log(" hitted");
                    Debug.Log(hit.collider.tag);

                    return null;
            }

    }

    public GameObject GenerateEnemies(Collider c)
    {

        // Get the terrain collider
        Collider terrainCollider = c;

        
            // Generate a random position within the area
            Vector3 position = transform.position + new Vector3(Random.Range(xRangeRocks.x, xRangeRocks.y), 0f, Random.Range(zRangeRocks.x, zRangeRocks.y));

            // Raycast downwards to find a valid spawn point on the terrain
            RaycastHit hit;
            if (Physics.Raycast(position + Vector3.up, Vector3.down, out hit, 200f))
            {
            Debug.Log("fodas2");

            // Check if the hit point is within the terrain bounds
            if (terrainCollider.bounds.Contains(hit.point))
                {
                    // Instantiate the tree at the hit point
                    GameObject tree = Instantiate(prefab[0], hit.point, Quaternion.identity);

                    return tree;
                }
        }
        else
        {
            Debug.Log("fodas");
        }

        return null;


    }

    public void Clear()
    {
        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

    }
}
