using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonSkill : MonoBehaviour
{
    public float searchRadius = 10f;  // distance to search for enemies
    public string enemyTag = "Enemy"; // tag to search for

    public Transform target;  // current enemy target
    bool isCanonActive = true;
    public float canonActiveTime = 5;

    bool firstShootDone = false;
    public GameObject bulletCanon;
    public Transform firePointCanon;
    float waitTimeNextShoot = 1f;

    private void Start()
    {
        firePointCanon = transform.GetChild(0).gameObject.transform;

        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
        float minDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(enemyTag))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < minDistance)
                {
                    // found a closer enemy, set as new target
                    minDistance = distance;
                    target = collider.transform;

                    Vector3 direction = target.position - transform.position;
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    targetRotation *= Quaternion.Euler(0f, -90f, 0f);

                    this.gameObject.GetComponent<Rigidbody>().MoveRotation(targetRotation);
                }
            }
        }
    }

    void Update()
    {
        if (target == null)
        {

            // no target, search for enemies
            Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
            float minDistance = Mathf.Infinity;

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag(enemyTag))
                {
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    if (distance < minDistance)
                    {
                        // found a closer enemy, set as new target
                        minDistance = distance;
                        target = collider.transform;

                        Vector3 direction = target.position - transform.position;
                        Quaternion targetRotation = Quaternion.LookRotation(direction);
                        targetRotation *= Quaternion.Euler(0f, -90f, 0f);

                        this.gameObject.GetComponent<Rigidbody>().MoveRotation(targetRotation);
                    }
                }
            }
        }

        if (target != null)
        {
            // rotate towards target
            //Vector3 direction = target.position - transform.position;
            //Quaternion lookRotation = Quaternion.LookRotation(direction);
            //this.gameObject.transform.rotation = lookRotation;

            Vector3 direction = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation *= Quaternion.Euler(0f, -90f, 0f);

            this.gameObject.GetComponent<Rigidbody>().MoveRotation(targetRotation);



            if (waitTimeNextShoot == 1f)
            {

                GameObject bullet = Instantiate(bulletCanon, firePointCanon.position, Quaternion.identity);
                bullet.transform.rotation = firePointCanon.transform.rotation;
                Vector3 direcaoDisparo = firePointCanon.transform.forward;
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(direcaoDisparo * 10f, ForceMode.Impulse);

                waitTimeNextShoot -= Time.deltaTime;

            }
            else
            {
                waitTimeNextShoot -= Time.deltaTime;

                if (waitTimeNextShoot <= 0)
                {
                    waitTimeNextShoot = 1;
                }
            }

            // check if target is still in range
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget > searchRadius)
            {
                // target is out of range, reset
                target = null;
            }
        }


        canonActiveTime -= Time.deltaTime;

        if (canonActiveTime <= 0)
        {
            Destroy(this.gameObject);

        }
    }

   


}
