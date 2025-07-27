using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireCanon : MonoBehaviour
{

    public GameObject cannonballPrefab;
    public GameObject cannonballPrefabpos;

    public float fireForce = 2f;

    public void Fire()
    {
        GameObject cannonball = Instantiate(cannonballPrefab, cannonballPrefabpos.transform.position, cannonballPrefabpos.transform.rotation);
        Rigidbody rb = cannonball.GetComponent<Rigidbody>();
        rb.AddForce(transform.right * fireForce);
    }
}
