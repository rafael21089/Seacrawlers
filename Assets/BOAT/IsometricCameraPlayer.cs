using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCameraPlayer : MonoBehaviour
{
    public Transform target;    // The player's transform
    public float height = 5f;    // Height above the player
    public float rotationX = 28f;    // Rotation around X axis
    public float rotationY = 32f;    // Rotation around Y axis
    public float maxDistance = 20f; // Maximum distance from the player when centered

    public float rotationSpeed = 10f; // Speed at which the camera rotates around the player

    private Vector3 offset;
    public float smoothSpeed = 0.1f; // Speed at which the camera moves to its target position

    void Start()
    {
        // Calculate the offset between the camera and the player
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
    // Calculate the position of the camera based on the player's position
    Vector3 targetPosition = target.position + Vector3.up * height;

    // Check for input from the left Alt key and left mouse button
    //if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0))
    //{
    //    // Rotate the camera around the player based on mouse movement
    //    rotationY += Input.GetAxis("Mouse X") * rotationSpeed;

    //    // Clamp the rotation angle to avoid over-rotation
    //    rotationY = Mathf.Clamp(rotationY, -180f, 180f);
    //}

    // Calculate the new camera position based on the updated rotation angle
    Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    Vector3 direction = rotation * Vector3.forward;
    float distance = Mathf.Clamp(offset.magnitude, 0f, maxDistance);
    Vector3 cameraPosition = targetPosition - direction * distance;

    //cameraPosition.y = Mathf.Round(cameraPosition.y);
    // Set the camera's position and rotation
    transform.position = cameraPosition;
    //transform.rotation = Quaternion.LookRotation(target.position - transform.position);
    }
}

