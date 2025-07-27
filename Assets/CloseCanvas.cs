using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCanvas : MonoBehaviour
{
    public GameObject canvas;

    public void CloseCvs()
    {
        canvas.SetActive(false);
    }
}
