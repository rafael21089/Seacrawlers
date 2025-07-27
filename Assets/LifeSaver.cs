using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LifeSaver : MonoBehaviour
{
    public GameObject player;
    public GameObject boat;

    public bool onetime = false;

    // Update is called once per frame
    void Update()
    {

        if (onetime == false)
        {
            onetime = true;

            player = GameObject.FindGameObjectWithTag("Player");
            boat = GameObject.FindGameObjectWithTag("boat");
        }
    }


    public void buy()
    {

        int money = player.GetComponent<PlayerRewards>().playerCurrentGold - 100;

        if (money >= 0)
        {
            player.GetComponent<PlayerRewards>().DecreaseGold(100);

            player.transform.gameObject.GetComponent<NavMeshAgent>().enabled = false;

            Vector3 targetPosition = new Vector3(30.2999992f, 1.54999995f, -158.5f);
            player.transform.position = targetPosition;
            player.GetComponent<movement>().isMoving = false;

            player.transform.gameObject.GetComponent<NavMeshAgent>().enabled = true;

            //-------------

            boat.transform.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            boat.transform.gameObject.GetComponent<NavMeshAgent>().enabled = false;

            Vector3 targetPosition2 = new Vector3(38.5999985f, 0.319999933f, -149.729996f);
            boat.transform.position = targetPosition2;

            boat.transform.gameObject.GetComponent<NavMeshAgent>().enabled = true;

        }

    }

    public void nobuy()
    {
        this.gameObject.SetActive(false);
    }
}
