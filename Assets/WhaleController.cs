using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WhaleController : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public float chaseRadius = 20f;
    public float chaseRadiusAttack = 20f;

    public Transform target;
    private Transform pl;
    private NavMeshAgent agent;
    private Animator anim;
    private float timer;
    private bool isChasing;
    private Quaternion targetRotation;
    public float rotationSpeed = 5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        pl = GameObject.FindGameObjectWithTag("Player").transform;

        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (!pl.gameObject.activeInHierarchy)
        {
            target = GameObject.FindGameObjectWithTag("boat").transform;
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= chaseRadius)
        {
            isChasing = true;

            if (distanceToTarget <= chaseRadiusAttack)
            {
                anim.SetBool("isChasing", false);
                anim.SetBool("isIdle", false);
                anim.SetBool("isAttacking", true);
            }
            else
            {
                anim.SetBool("isChasing", true);
                anim.SetBool("isIdle", false);
                anim.SetBool("isAttacking", false);

            }

            agent.SetDestination(target.position);
            targetRotation = Quaternion.LookRotation(target.position - transform.position);
        }
        else if (isChasing)
        {
            isChasing = false;
            anim.SetBool("isChasing", false);
            anim.SetBool("isIdle", true);
            anim.SetBool("isAttacking", false);

            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            targetRotation = Quaternion.LookRotation(newPos - transform.position);
        }
        else if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            targetRotation = Quaternion.LookRotation(newPos - transform.position);
            timer = 0;
        }

        timer += Time.deltaTime;

        // Smoothly rotate towards target rotation

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }


    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}
