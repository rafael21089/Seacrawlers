using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoloDmg : MonoBehaviour
{
    public float danoPorSegundo = 5f;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("molotov");
            other.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(danoPorSegundo *  -1);
      
        }
    }
    
}
