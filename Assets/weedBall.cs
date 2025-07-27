using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weedBall : MonoBehaviour
{
    public float speed = 5f;
    public float distance = 6f;

    GameObject targetPosition;

    Vector3 posIni;

    private void Start()
    {
        targetPosition = GameObject.FindGameObjectWithTag("Player");
        // Store the current position as the jump start position
        posIni = targetPosition.transform.position;

        posIni.y = 3f;

        // Calculate the direction vector from the enemy to the player
        Vector3 direction = (posIni - transform.position).normalized;

        // Rotate the enemy towards the player using Quaternion.LookRotation()
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

        // Rotate the fireball object by -90 degrees on the y-axis
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction vector towards the initial position
        Vector3 direction = (posIni - transform.position).normalized;

        // Move the fireball towards the initial position in world space
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Check if the fireball has traveled beyond the maximum distance
        if (Vector3.Distance(transform.position, posIni) < 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
