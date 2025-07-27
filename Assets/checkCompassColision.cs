using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkCompassColision : MonoBehaviour
{
    GameObject boat;

    GameObject a1, a2, a3, a4, a5, a6, a7, a8;

    /*void Start()
    {
        boat = GameObject.Find("sailship");

        a1 = GameObject.Find("i1");
        a2 = GameObject.Find("i2");
        a3 = GameObject.Find("i3");
        a4 = GameObject.Find("i4");
        a5 = GameObject.Find("i5");
        a6 = GameObject.Find("i6");
        a7 = GameObject.Find("i7");
        a8 = GameObject.Find("i8");
        //a8 = GameObject.FindGameObjectWithTag("i8").GetComponent<Image>();

    }*/

    private void Update()
    {
        if(a1 == null)
        {
            a1 = GameObject.FindGameObjectWithTag("i1");
            a2 = GameObject.FindGameObjectWithTag("i2");
            a3 = GameObject.FindGameObjectWithTag("i3");
            a4 = GameObject.FindGameObjectWithTag("i4");
            a5 = GameObject.FindGameObjectWithTag("i5");
            a6 = GameObject.FindGameObjectWithTag("i6");
            a7 = GameObject.FindGameObjectWithTag("i7");
            a8 = GameObject.FindGameObjectWithTag("i8");
            boat = GameObject.FindGameObjectWithTag("boat");
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("boat"))
        {
            Debug.Log("FDS2");
            if (a1 != null)
            {
                Debug.Log("FDS3");
                if (a1.GetComponent<Image>().color == Color.red)
                {
                    Debug.Log("FDS18");
                    a1.SetActive(false);
                }
            }

            if (a2 != null)
            {
                Debug.Log("FDS4");
                if (a2.GetComponent<Image>().color == Color.red)
                {
                    Debug.Log("FDS17");
                    a2.SetActive(false);
                }
            }

            if (a3 != null)
            {
                Debug.Log("FDS5");
                if (a3.GetComponent<Image>().color == Color.red)
                {
                    Debug.Log("FDS16");
                    a3.SetActive(false);
                }
            }

            if (a4 != null)
            {
                Debug.Log("FDS6");
                if (a4.GetComponent<Image>().color == Color.red)
                {
                    Debug.Log("FDS15");
                    a4.SetActive(false);
                }
            }

            if (a5 != null)
            {
                Debug.Log("FDS7");
                if (a5.GetComponent<Image>().color == Color.red)
                {
                    Debug.Log("FDS14");
                    a5.SetActive(false);
                }
            }

            if (a6 != null)
            {
                Debug.Log("FDS8");
                if (a6.GetComponent<Image>().color == Color.red)
                {
                    Debug.Log("FDS13");
                    a6.SetActive(false);
                }
            }

            if (a7 != null)
            {
                Debug.Log("FDS9");
                if (a7.GetComponent<Image>().color == Color.red)
                {
                    Debug.Log("FDS12");
                    a7.SetActive(false);
                }
            }

            if (a8 != null)
            {
                Debug.Log("FDS10");
                if (a8.GetComponent<Image>().color == Color.red)
                {
                    Debug.Log("FDS11");
                    a8.SetActive(false);
                }
            }

        }
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("FDS");
        if (collision.transform.CompareTag("boat") || collision.transform.CompareTag("Player"))
        {
            Debug.Log("FDS2");
            if (a1.activeSelf)
            {
                Debug.Log("FDS3");
                if (a1.GetComponent<Image>().color == Color.red)
                {
                    Debug.Log("FDS18");
                    a1.SetActive(false);
                }
            }

            if (a2.activeSelf)
            {
                Debug.Log("FDS4");
                if (a2.GetComponent<Image>().color == Color.red)
                {
                    Debug.Log("FDS17");
                    a2.SetActive(false);
                }
            }

            if (a3.activeSelf)
            {
                Debug.Log("FDS5");
                if (a3.GetComponent<Image>().color == Color.red)
                {
                    Debug.Log("FDS16");
                    a3.SetActive(false);
                }
            }

            if (a4.activeSelf)
            {
                Debug.Log("FDS6");
                if (a4.GetComponent<Image>().color == Color.red)
                {
                    Debug.Log("FDS15");
                    a4.SetActive(false);
                }
            }

            if (a5.activeSelf)
            {
                Debug.Log("FDS7");
                if (a5.GetComponent<Image>().color == Color.red)
                {
                    Debug.Log("FDS14");
                    a5.SetActive(false);
                }
            }

            if (a6.activeSelf)
            {
                Debug.Log("FDS8");
                if (a6.GetComponent<Image>().color == Color.red)
                {
                    Debug.Log("FDS13");
                    a6.SetActive(false);
                }
            }

            if (a7.activeSelf)
            {
                Debug.Log("FDS9");
                if (a7.GetComponent<Image>().color == Color.red)
                {
                    Debug.Log("FDS12");
                    a7.SetActive(false);
                }
            }

            if (a8.activeSelf)
            {
                Debug.Log("FDS10");
                if (a8.GetComponent<Image>().color == Color.red)
                {
                    Debug.Log("FDS11");
                    a8.SetActive(false);
                }
            }

        }
    }*/


   
}
