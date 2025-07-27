using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCleaner : MonoBehaviour
{
    // Call UnloadUnusedAssets every 30 seconds
    private const int CLEANUP_INTERVAL = 1800;

    // Target frame rate for the game
    private const int TARGET_FRAME_RATE = 60;

    public int i = 0;
    public bool onetime = false;

    void Awake()
    {
        // Set target frame rate
        Application.targetFrameRate = TARGET_FRAME_RATE;

        // Disable VSync
        QualitySettings.vSyncCount = 0;
    }


    

    void Update()
    {
        // Call UnloadUnusedAssets every CLEANUP_INTERVAL frames
        if (Time.frameCount % CLEANUP_INTERVAL == 0)
        {
            Resources.UnloadUnusedAssets();
            
        }


    }

    void OnDisable()
    {
        // Free unused memory
        System.GC.Collect();
    }
}
