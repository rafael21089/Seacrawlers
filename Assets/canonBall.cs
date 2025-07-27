using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canonBall : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 10f;

    public Rigidbody rb;

    public GameObject Explosion;
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("boat").transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }

        rb.MovePosition(transform.position + transform.right * speed * Time.fixedDeltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("sea"))
        {
            GameObject newobj = Instantiate(Explosion,other.transform.position,Explosion.transform.rotation);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameObject newobj = Instantiate(Explosion, other.transform.position, Explosion.transform.rotation);
        }
    }
}
