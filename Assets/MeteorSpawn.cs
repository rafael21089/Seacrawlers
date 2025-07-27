using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawn : MonoBehaviour
{
    public GameObject meteorPrefab;
    public int numMeteors = 5;
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = 0f;
    public float maxY = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;
    public float minDelay = 1f;
    public float maxDelay = 5f;

    private void Start()
    {
        /*for (int i = 0; i < numMeteors; i++)
        {
            float delay = Random.Range(minDelay, maxDelay);
            Invoke("SpawnMeteor", delay);
        }*/
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.L))
            SpawnMeteor();
    }

    private void SpawnMeteor()
    {
        /*Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
        GameObject meteor = Instantiate(meteorPrefab, position, Quaternion.identity);
        Rigidbody rb = meteor.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.down * Random.Range(10f, 20f), ForceMode.Impulse);*/

        Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
        GameObject meteor = Instantiate(meteorPrefab, position, Quaternion.identity);
        Rigidbody rb = meteor.GetComponent<Rigidbody>();
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.AddForce(randomDirection * Random.Range(10f, 20f), ForceMode.Impulse);
        rb.AddTorque(randomDirection * Random.Range(10f, 20f), ForceMode.Impulse);
    }
}
