using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distanceLoading : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public GameObject boat;
    public GameObject p1;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boat = GameObject.FindGameObjectWithTag("boat");

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!player.activeInHierarchy)
        {
            p1 = GameObject.FindGameObjectWithTag("boat");
        }
        else
        {
            p1 = GameObject.FindGameObjectWithTag("Player");
        }

        float distance = Vector3.Distance(transform.position, p1.transform.position);

        if (distance >= 300)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeInHierarchy)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                if (!child.gameObject.activeInHierarchy)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }



    }
}
