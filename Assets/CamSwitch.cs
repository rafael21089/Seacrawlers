using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public Camera cam;
    public CameraLockUnlock camLockUnlockScript;
    public IsometricCameraPlayer iCP;
    bool camViewChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        camLockUnlockScript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!camViewChanged)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                camViewChanged = true;
                camLockUnlockScript.enabled = true;
                iCP.enabled = false;
                //cam.enabled = false;
            }
        }
        else if(camViewChanged)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                camViewChanged = false;
                camLockUnlockScript.enabled = false;
                iCP.enabled = true;
                //cam.enabled = true;
            }
        }
    }
}
