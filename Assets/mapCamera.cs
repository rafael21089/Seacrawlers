using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapCamera : MonoBehaviour
{
    void LateUpdate()
    {
        RenderTexture renderTexture = GetComponent<Camera>().targetTexture;

        // Set the render texture as the active render target
        RenderTexture.active = renderTexture;

        // Clear the render texture to black
        GL.Clear(true, true, Color.clear);

        // Render the scene
        GetComponent<Camera>().Render();
    }

}
