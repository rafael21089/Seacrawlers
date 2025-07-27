using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoatMovement : MonoBehaviour
{
    public Camera cam;
    NavMeshAgent agent;
    RaycastHit rHit;
    float distToArrive;


    public GameObject[] cannon;

    private bool isCoolingDownKraken = false;
    private bool isCoolingDownManta = false;
    private bool isCoolingDownMarinha = false;


    [SerializeField] public float damageOfBoat = 100;


    // Start is called before the first frame update
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        damageOfBoat = 100;
    }

    // Update is called once per frame

    void Update()
    {

        if (this.GetComponent<HealthSystemForDummies>().CurrentHealth <= 0)
        {
            agent.isStopped = true;
            agent.enabled = false;
            transform.position = new Vector3(38.5999985f, 0.319999933f, -149.729996f);
          
            agent.enabled = true;

            this.GetComponent<HealthSystemForDummies>().ReviveWithMaximumHealth();
        }
        else
        {
            if (Input.GetMouseButton(1))
            {
                int layerMask = 1 << LayerMask.NameToLayer("Water");

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out rHit, Mathf.Infinity, layerMask))
                {
                    if (rHit.collider.CompareTag("sea"))
                    {
                        agent.SetDestination(rHit.point);
                        // Rest of your code...
                    }
                    else
                    {
                        // Rest of your code...
                    }
                }

            }

            // Check if we've reached the destination
            if (agent.hasPath)
            {
                Vector3 relativePos = rHit.point - transform.position;
                Quaternion toRotation = Quaternion.LookRotation(relativePos);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1 * Time.deltaTime);


            }


            for (int i = 0; i < cannon.Length; i++)
            {
                if (cannon[i].activeInHierarchy)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        cannon[i].GetComponent<Animator>().SetTrigger("shoot");
                    }
                }
            }

            
        }
        
        

        

    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.name == "kraken" && isCoolingDownKraken == false)
        {

            transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-100);

            isCoolingDownKraken = true;
            StartCoroutine(CoolDownKraken());
        }

        if (other.name == "MantaBullet(Clone)" && isCoolingDownManta == false)
        {

            transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-50);
            Destroy(other.gameObject);

            isCoolingDownManta = true;
            StartCoroutine(CoolDownManta());
        }

        if (other.name == "BoatCanonShoot(Clone)" && isCoolingDownMarinha == false)
        {

            transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-100);
            Destroy(other.gameObject);

            isCoolingDownMarinha = true;
            StartCoroutine(CoolDownMarinha());
        }

    }
    IEnumerator CoolDownKraken()
    {
        yield return new WaitForSeconds(0.5f);
        isCoolingDownKraken = false;
    }
    IEnumerator CoolDownManta()
    {
        yield return new WaitForSeconds(0.1f);
        isCoolingDownManta = false;
    }

    IEnumerator CoolDownMarinha()
    {
        yield return new WaitForSeconds(0.1f);
        isCoolingDownMarinha = false;
    }
}
