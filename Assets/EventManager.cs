using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] TimeController timeC;
    [SerializeField] GameObject meteoro;
    [SerializeField] GameObject tornado;

    private void Update()
    {
        if(timeC.daysToFinish == 29)
        {
            //meteoro.SetActive(true);
            tornado.SetActive(true);
        }
        else
        {
            //meteoro.SetActive(false);
            tornado.SetActive(false);
        }
    }
}
