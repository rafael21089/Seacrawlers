using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{

    /*private void OnTriggerEnter(Collider other)
    {
        Debug.Log("bbbb");
    }*/

    public float danoPorSegundo = 5f;

    void OnTriggerStay(Collider other)
    {
       
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("purple" + other.tag);
            other.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(danoPorSegundo * -1);

        }
    }
}
