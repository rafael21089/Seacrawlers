using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipPatrol : MonoBehaviour
{
    public float IdleTime = 5;
    public float speed = 50f; // Speed of the ship
    public float patrolDistance = 50f; // Distance the ship will patrol
    public Transform[] patrolPoints; // Array of patrol points
    private int currentPoint = 0; // Index of current patrol point
    private bool movingForward = true; // Flag indicating direction of patrol


    public float minDistance = 15f; // Minimum distance to detect enemies
    public float maxDistance = 20f; // Maximum distance to detect enemies
    public LayerMask enemyLayer; // Layer mask for enemy objects
    public bool isEnemyDetected; // Flag to indicate if an enemy is detected



    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = 10f;
        transform.position = newPosition;
        IdleTime -= Time.deltaTime;

        if(IdleTime <= 0)
        {
            anim.SetBool("isPatrolling", true);
        }

        if (anim.GetBool("isPatrolling"))
        {
            // Get the direction to the next patrol point
            Vector3 nextPatrolPoint = patrolPoints[currentPoint].position;
            Vector3 directionToNextPoint = nextPatrolPoint - transform.position;
            directionToNextPoint.y = 0; // ignore y direction
            directionToNextPoint.Normalize();

            // Rotate the ship towards the next patrol point
            if (directionToNextPoint != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToNextPoint, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 2f);
            }

            // Move the ship towards the next patrol point
            Vector3 forwardDirection = transform.forward;
            forwardDirection.y = 0; // ignore y direction
            transform.position += forwardDirection * speed * Time.deltaTime;

            // Check if we've reached the next patrol point
            if (Vector3.Distance(transform.position, nextPatrolPoint) < 1f)
            {
                currentPoint++;
                if (currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }
            }
        }

        // Check for enemies within the specified distance range
        Collider[] hits = Physics.OverlapSphere(transform.position, maxDistance, enemyLayer);
        for (int i = 0; i < hits.Length; i++)
        {
            // Calculate the distance between the player and the enemy
            float distance = Vector3.Distance(transform.position, hits[i].transform.position);

            // If the enemy is within the distance range, set the detection flag to true
            if (distance >= minDistance && distance <= maxDistance)
            {
                isEnemyDetected = true;
                break;
            }
        }

        if (isEnemyDetected)
        {
            anim.SetBool("isPatrolling", false);
            anim.SetBool("isChasing", true);
        }

        if (anim.GetBool("isChasing"))
        {
            speed = speed * 2;
        }


    }

    // Draw the detection radius in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistance);
    }
}