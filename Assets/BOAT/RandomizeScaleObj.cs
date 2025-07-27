using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeScaleObj : MonoBehaviour
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

    public Vector3 randomRotationRange;

    private void Awake()
    {
        RandomizeScale();
    }

    public void RandomizeScale()
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

        RandomizeByAxis(randomRotationRange);
    }


    public void RandomizeMyRotation()
    {
        transform.rotation = Random.rotation;
    }

    public void RandomizeByAxis(Vector3 randomRotationConstraints)
    {
        Quaternion randomConstrainedRotation = Quaternion.Euler(transform.rotation.eulerAngles.x + Random.Range(-randomRotationConstraints.x, randomRotationConstraints.x),
            transform.rotation.eulerAngles.y + Random.Range(-randomRotationConstraints.y, randomRotationConstraints.y),
            transform.rotation.eulerAngles.z + Random.Range(-randomRotationConstraints.z, randomRotationConstraints.z));

        transform.rotation = randomConstrainedRotation;
    }
}
