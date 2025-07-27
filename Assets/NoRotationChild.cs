using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRotationChild : MonoBehaviour
{
    public Transform childToExclude;
    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = childToExclude.localRotation;
    }

    private void LateUpdate()
    {
        // Aplica a rotação inversa ao filho desejado
        childToExclude.rotation = Quaternion.Inverse(transform.rotation) * initialRotation;
    }
}
