using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;
    //public GameObject hitEffect;

    private void Awake()
    {
        Destroy(gameObject, life);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            //if(Abilities.isBlockActive != true)
            //{
                collision.collider.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-50);
                Destroy(this.gameObject);
            //}
                
        }

        if (collision.collider.CompareTag("Player"))
        {
           
             Destroy(this.gameObject);
            

        }

    }
}
