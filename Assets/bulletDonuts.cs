using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDonuts : MonoBehaviour
{
    public float life = 0.05f;
    GameObject player;
    private void Awake()
    {
        Destroy(gameObject, life);
    }
    private void Start()
    {
         player = GameObject.FindGameObjectWithTag("Player");    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            //if (Abilities.isBlockActive != true)
                collision.collider.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-50);
        }
    }

  

    // Update is called once per frame
    void Update()
    {
        life-= Time.deltaTime;
        
        float dist = Vector3.Distance(gameObject.transform.position, player.transform.position);

        if (life <= 0 || dist > 5)
        {
            Destroy(gameObject);
        }
            
    }
}
