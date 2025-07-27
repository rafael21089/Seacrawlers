using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleExplosion : MonoBehaviour
{
    [Header("Stats")]
    public int damage;
    public bool destroyOnHit;

    //[Header("Effects")]
    //public GameObject muzzleEffect;
    //public GameObject hitEffect;

    [Header("Explosive Projectile")]
    public bool isExplosive;
    public float explosionRadius;
    public float explosionForce;
    public int explosionDamage;
    public GameObject explosionEffect;

    private Rigidbody rb;

    private bool hitTarget;
    public static bool vaiExplodir = false;

    // Start is called before the first frame update
    void Start()
    {
        // get rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Abilities.isReadyToExplode)
        //{
            vaiExplodir = true;
            Explode();
            
        //}

    }

    private void Explode()
    {
        // spawn explosion effect (if assigned)
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // find all the objects that are inside the explosion range
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, explosionRadius);

        // loop through all of the found objects and apply damage and explosion force
        for (int i = 0; i < objectsInRange.Length; i++)
        {
            if (objectsInRange[i].gameObject == gameObject)
            {
                // don't break or return please, thanks
            }
            else
            {
                //apply damage to miniBoss
                if (objectsInRange[i].GetComponent<HealthSystemForDummies>() != null)
                {
                    objectsInRange[i].GetComponent<HealthSystemForDummies>().AddToCurrentHealth(explosionDamage * (-1));
                }


                // check if object is enemy, if so deal explosionDamage
                ////if (objectsInRange[i].GetComponent<BasicEnemy>() != null)
                ////    objectsInRange[i].GetComponent<BasicEnemy>().TakeDamage(explosionDamage);

                // check if object has a rigidbody
                if (objectsInRange[i].GetComponent<Rigidbody>() != null)
                {

                    if (!objectsInRange[i].CompareTag("Player"))
                    {
                        // custom explosionForce
                        Vector3 objectPos = objectsInRange[i].transform.position;

                        // calculate force direction
                        Vector3 forceDirection = (objectPos - transform.position).normalized;

                        // apply force to object in range
                        objectsInRange[i].GetComponent<Rigidbody>().AddForceAtPosition(forceDirection * explosionForce + Vector3.up * explosionForce, transform.position + new Vector3(0, -0.5f, 0), ForceMode.Impulse);

                        Debug.Log("Kabooom " + objectsInRange[i].name);
                    }
                }
            }
        }

        // destroy projectile with 0.1 seconds delay
        Invoke(nameof(DestroyProjectile), 0.1f);
    }

    private void DestroyProjectile()
    {
        Debug.Log("efeito" + gameObject);
        Destroy(gameObject);
    }
}
