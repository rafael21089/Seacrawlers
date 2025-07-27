using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    public Transform player;    // the player to follow
    public float zoomSpeed;    // the speed at which to zoom in/out
    public float minZoom;  // the minimum zoom level
    public float maxZoom; // the maximum zoom level

    Camera cam;


    public float cameraHeight = 40f; // Height of the camera above the player
    public float cameraDistance = 40f; // Distance of the camera from the player

    private float cameraYAngle = 45f; // Starting Y angle of the camera
    public float followDelay = 1f; // Delay between the player's movement and the camera's follow position


    public float rotationDelay = 0.5f; // Delay between the player's movement and the camera's rotation towards the player

    private Vector3 targetFollowPosition; // Target position the camera is following
    private Vector3 currentFollowPosition; // Current position the camera is following
    private Quaternion targetRotation; // Target rotation of the camera
    private Quaternion currentRotation; // Current rotation of the camera

    public float rotationSpeed = 90f; // Speed of rotation when pressing the "1" key

    private void Awake()
    {
        // Calculate the camera's target follow position based on the player's position, distance, and Y angle
        targetFollowPosition = player.position - Quaternion.Euler(0f, cameraYAngle, 0f) * Vector3.forward * cameraDistance;

        // Interpolate the camera's follow position with the target position based on the follow delay
        currentFollowPosition = Vector3.Lerp(currentFollowPosition, targetFollowPosition, followDelay * Time.deltaTime);


        // Calculate the camera's position based on the follow position and height
        Vector3 cameraPosition = currentFollowPosition;
        cameraPosition.y = cameraHeight;

        // Set the camera's position and rotation
        transform.position = cameraPosition;
    }

    void Start()
    {
        // calculate the initial offset between the camera and the player
        cam = GetComponent<Camera>();

        // Set the initial follow position to the player's position
        currentFollowPosition = player.position;
        targetFollowPosition = currentFollowPosition;

        transform.position = currentFollowPosition;

        transform.LookAt(player);

        // Set the initial rotation to face the player
        currentRotation = transform.rotation;
        targetRotation = Quaternion.LookRotation(player.position - transform.position);
    }

    void LateUpdate()
    {

        // Calculate the camera's target follow position based on the player's position, distance, and Y angle
        targetFollowPosition = player.position - Quaternion.Euler(0f, cameraYAngle, 0f) * Vector3.forward * cameraDistance;

        // Interpolate the camera's follow position with the target position based on the follow delay
        currentFollowPosition = Vector3.Lerp(currentFollowPosition, targetFollowPosition, followDelay * Time.deltaTime);

        // Calculate the camera's target rotation based on the player's position
        targetRotation = Quaternion.LookRotation(player.position - transform.position);

        // Interpolate the camera's rotation with the target rotation based on the rotation delay
        currentRotation = Quaternion.Lerp(currentRotation, targetRotation, rotationDelay * Time.deltaTime);

        // Calculate the camera's position based on the follow position and height
        Vector3 cameraPosition = currentFollowPosition;
        cameraPosition.y = cameraHeight;

        // Set the camera's position and rotation
        transform.position = cameraPosition;
        //transform.rotation = currentRotation;


        // handle zooming in/out with the mouse wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {

            // calculate the new zoom level
            float newSize = Mathf.Clamp(cam.orthographicSize - (scroll * zoomSpeed), minZoom, maxZoom);

            // update the camera's orthographic size with the new zoom level
            cam.orthographicSize = newSize;
        }
    }
}
