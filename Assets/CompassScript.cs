using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassScript : MonoBehaviour
{
    public Transform playerTransform;
    public Transform boatTransform;
    Vector3 dir;
    public Camera miniCamPlayer, miniCamBoat;
    

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.gameObject.activeSelf)
        {
            dir.z = playerTransform.eulerAngles.y;
            transform.localEulerAngles = dir;
            miniCamPlayer.gameObject.SetActive(true);
            miniCamBoat.gameObject.SetActive(false);
        }
        else
        {
            dir.z = boatTransform.eulerAngles.y;
            transform.localEulerAngles = dir;
            miniCamBoat.gameObject.SetActive(true);
            miniCamPlayer.gameObject.SetActive(false);
        }
        
    }
}
