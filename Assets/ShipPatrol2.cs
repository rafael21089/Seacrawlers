using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipPatrol2 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;


    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public float chaseRadius = 20f;
    public float chaseRadiusAttack = 20f;

    public Transform target;
    private Transform boat;
    private NavMeshAgent agent;
    private Animator anim;
    private float timer;
    private bool isChasing;
    private Quaternion targetRotation;
    public float rotationSpeed = 5f;
    public bool onetime = false;


    public Collider col;
    public float shootAngleThreshold = 10f;
    public float rotationSpeedShoot = 90f; // Speed at which the boat rotates towards the player
    public float fireCooldown = 2f; // Cooldown between firing shots
    private bool canFire = true; // Whether the boat can currently fire a shot
    private Quaternion targetRotationShoot; // The target rotation for the boat to face the player

    public GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (boat == null && GameObject.FindGameObjectWithTag("boat") != null)
        {
            target = GameObject.FindGameObjectWithTag("boat").transform;
        }
        

        float distanceToTarget = Vector3.Distance(transform.position, target.position);


        if (distanceToTarget <= 100)
        {
            if (this.GetComponent<HealthSystemForDummies>().CurrentHealth <= 0 && onetime == false)
            {

                Destroy(this.gameObject);
                onetime = true;

            }
            else
            {
                if (distanceToTarget <= chaseRadius)
                {
                    isChasing = true;

                    if (distanceToTarget <= chaseRadiusAttack)
                    {

                        agent.isStopped = true;


                        // Calculate the direction to the target
                        Vector3 directionToTarget = (target.position - transform.position).normalized;

                        // Add 90 degrees to the direction (in radians)
                        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x);
                        angle += Mathf.Deg2Rad * 90f;

                        // Calculate the new direction vector
                        directionToTarget = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);

                        // Calculate the rotation to point towards the target
                        targetRotation = Quaternion.LookRotation(directionToTarget);


                        // Check if the object is already rotated towards the target
                        float angle2 = Quaternion.Angle(transform.rotation, targetRotation);

                        if (angle2 < shootAngleThreshold)
                        {
                            // Calculate the direction to the target
                            Vector3 directionToTarget2 = (target.position - transform.position).normalized;

                            // Calculate the rotation to point towards the target
                            targetRotation = Quaternion.LookRotation(directionToTarget2);

                            // Check if the boat is pointing towards the target
                            if (Quaternion.Angle(transform.rotation, targetRotation) < shootAngleThreshold)
                            {
                                // Check if the boat can fire a shot
                                if (canFire)
                                {
                                    // Fire the cannon
                                    FireCannon();
                                    // Start the cooldown timer
                                    StartCoroutine(FireCooldown());
                                }
                            }
                            else
                            {
                                // Rotate the boat towards the target over time
                                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                            }
                        }
                        else
                        {
                            // Rotate the object towards the target
                            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                        }


                    }
                    else
                    {
                        // Stop the boat and reset the target rotation
                        agent.isStopped = true;
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    }

                    if (distanceToTarget > chaseRadiusAttack)
                    {
                        agent.isStopped = false;

                        agent.SetDestination(target.position);
                        // Rotate the boat towards the target over time
                        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    }




                }
                //else if (isChasing)
                //{
                //    // Stop the boat and reset the target rotation
                //    agent.isStopped = false;
                //    //transform.rotation = targetRotation;

                //    isChasing = false;

                //    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                //    agent.SetDestination(newPos);
                //    targetRotation = Quaternion.LookRotation(newPos - transform.position);
                //}
                //else if (timer >= wanderTimer)
                //{
                //    // Stop the boat and reset the target rotation
                //    agent.isStopped = false;
                //    //transform.rotation = targetRotation;
                //    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                //    agent.SetDestination(newPos);
                //    targetRotation = Quaternion.LookRotation(newPos - transform.position);
                //    timer = 0;
                //}

                //timer += Time.deltaTime;

                //// Smoothly rotate towards target rotation
                //if (distanceToTarget <= chaseRadius)
                //{
                //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                //}

            }
        }


       


    }


    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(randDirection, out navHit, dist, layermask))
        {
            return navHit.position;
        }
        else
        {
            // If a valid position was not found, return the original position
            return origin;
        }
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.name == "CanonBall(Clone)")
        {
            //hit
            this.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-target.gameObject.GetComponent<BoatMovement>().damageOfBoat);
            GameObject newobj = Instantiate(Explosion, this.transform.position, Explosion.transform.rotation);

            Destroy(other.gameObject);
        }
    }

    void FireCannon()
    {
        GameObject bulletCanon = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bulletCanon.GetComponent<Rigidbody>();
        Vector3 direction = (target.transform.position - firePoint.position).normalized;
        rb.AddForce(direction * 10f, ForceMode.Impulse);
    }

    IEnumerator FireCooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(fireCooldown);
        canFire = true;
    }




}
