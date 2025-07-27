using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    [SerializeField] private GameObject fullMap;
    private bool active;

    public void CloseFullMap()
    {
        active = false;
        fullMap.SetActive(false);
    }

    private void Start()
    {
        active = false;
        fullMap.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            active = !active;
            fullMap.SetActive(active);
        }
    }
}