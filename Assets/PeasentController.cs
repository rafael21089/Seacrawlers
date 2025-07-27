using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PeasentController : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;
    Vector3 target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();    
    }

    void Update() 
    {
        if (Vector3.Distance(transform.position, target) < 5)
        {
            IterateIndex();
            UpdateDestination();
        }
    }
    
      

    void UpdateDestination()
    {
        target = waypoints[currentWaypointIndex].position;
        agent.SetDestination(target);
    }

    void IterateIndex() 
    {
        currentWaypointIndex++;
        if (currentWaypointIndex == waypoints.Length)
        {
            currentWaypointIndex = 0;
        }
    }
}
