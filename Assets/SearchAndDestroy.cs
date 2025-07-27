using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAndDestroy : MonoBehaviour
{
    public float danoPorSegundo = 25f;

    public float life = 3f;
    
    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;

        if(life <= 0)
        {
            Destroy(gameObject);
        }
    }

    /*void OnTriggerStay(Collider other)
    {
        Debug.Log("inimigome");
        /*if (other.CompareTag("Enemy"))
        {
            Debug.Log("inimigo");
            Destroy(other.gameObject);
        }*/
       /* if (other.CompareTag("Enemy"))
        {
            other.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(danoPorSegundo * -1);

        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(danoPorSegundo * -1);
        }
    }
}
