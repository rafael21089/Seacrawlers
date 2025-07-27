using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomizeScale : MonoBehaviour
{
    public float globalScaleMultiplier = 1f;

    public bool scaleUniformly;

    public float uniformScaleMin = .1f;
    public float uniformScaleMax = 1f;

    public float xScaleMin = .1f;
    public float xScaleMax = 3f;
    public float yScaleMin = .1f;
    public float yScaleMax = 3f;
    public float zScaleMin = .1f;
    public float zScaleMax = 3f;

    public float RocksQuantity;
    public float EnemyQuantity;

    public GenerateIslandObjects g;

    bool spawnEnemies = false;
    bool _hasLoadedFirstFrame = false;

    public NavMeshSurface nv;

    private void Awake()
    {
        RandomizeObjectScale();

        //nv = GameObject.FindGameObjectWithTag("Navmesh").GetComponent<NavMeshSurface>();
        //nv.BuildNavMesh();

    }

    void RandomizeObjectScale()
    {
        Vector3 randomizedScale = Vector3.one;
        if (scaleUniformly)
        {
            float uniformScale = Random.Range(uniformScaleMin, uniformScaleMax);
            randomizedScale = new Vector3(uniformScale, uniformScale, uniformScale);
        }
        else
        {
           randomizedScale = new Vector3(Random.Range(xScaleMin, xScaleMax), Random.Range(yScaleMin, yScaleMax), Random.Range(zScaleMin, zScaleMax));
        }

        transform.localScale = randomizedScale * globalScaleMultiplier;

    }
}
