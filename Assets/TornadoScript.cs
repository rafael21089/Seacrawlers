using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoScript : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 point1;
    public Vector2 point2;

    private Vector2 targetPoint;
    private Renderer renderer;

    bool isMovingFirstPoint = true;
    float dissolveAmount = 1f;
    private void Start()
    {
        // Get the renderer component from the game object
        renderer = GetComponent<Renderer>();

        // Pick random points within the bounds of the map
        float xMin = -10f;
        float xMax = 10f;
        float zMin = -10f;
        float zMax = 10f;
        point1 = new Vector2(Random.Range(xMin, xMax), Random.Range(zMin, zMax));
        point2 = new Vector2(Random.Range(xMin, xMax), Random.Range(zMin, zMax));

        // Start by moving towards point 1
        targetPoint = point1;

        // Set the initial dissolve value to 1
        renderer.material.SetFloat("_Dissolve", 1f);
    }
    float Increase(Vector3 newPosition)
    {
        /*float dissolveAmount = 1f - Mathf.Clamp01(Vector3.Distance(transform.position, newPosition) / Vector3.Distance(point1, point2));
        return dissolveAmount;*/

        dissolveAmount += 0.01f;
        return dissolveAmount;
    }

    float Decrease(Vector3 newPosition)
    {
        dissolveAmount -= 0.01f;
        return dissolveAmount;
    }

    private void Update()
    {
        // Move towards the current target point
        Vector3 newPosition = new Vector3(targetPoint.x, transform.position.y, targetPoint.y);
        transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        // Calculate the dissolve amount based on the distance from the current position to the target point
        //float dissolveAmount = 1f - Mathf.Clamp01(Vector3.Distance(transform.position, newPosition) / Vector3.Distance(point1, point2));

        if (isMovingFirstPoint)
            dissolveAmount = Decrease(newPosition);
         else
             dissolveAmount = Increase(newPosition);

         // Set the dissolve amount on the material
         renderer.material.SetFloat("_Dissolve", dissolveAmount);

        // If the tornado has reached the second point, stop moving
         if (targetPoint == point2 && transform.position == newPosition)
         {
            // You can add code here to do something when the tornado reaches its destination
            /*Debug.Log("Tornado has reached its destination!");
            renderer.material.SetFloat("_Dissolve", 1f); // Set dissolve to 1 when reaching point 2
            enabled = false; // Disable the script to stop moving the tornado
            return;*/
            Destroy(gameObject);
        }

        // If the tornado has reached the current target point, pick a new one
        if (Vector3.Distance(transform.position, newPosition) < 0.1f)
         {
             if (targetPoint == point1)
             {
                 renderer.material.SetFloat("_Dissolve", 0f); // Set dissolve to 1 when reaching point 1
                 isMovingFirstPoint = false;
                 targetPoint = point2;
             }
             /*else
             {
                 renderer.material.SetFloat("_Dissolve", 1f); // Set dissolve to 0 when reaching point 2
                 //targetPoint = point1;
             }*/
         }
    }
}

