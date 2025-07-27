using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFixed : MonoBehaviour
{
    public Vector3 notrandomRotationRange;

    // Start is called before the first frame update
    void Awake()
    {
        RandomizeByAxis(notrandomRotationRange);
    }

    public void RandomizeByAxis(Vector3 randomRotationConstraints)
    {
        Quaternion randomConstrainedRotation = Quaternion.Euler(transform.rotation.eulerAngles.x + randomRotationConstraints.x,
            transform.rotation.eulerAngles.y + randomRotationConstraints.y,
            transform.rotation.eulerAngles.z + randomRotationConstraints.z);

        transform.rotation = randomConstrainedRotation;
    }
}
