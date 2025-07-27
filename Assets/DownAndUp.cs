using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DownAndUp : MonoBehaviour
{
    public Animator anim;

    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public float radiusAttack = 20f;

    public Transform player;
    public Transform target;
    private NavMeshAgent agent;
    private float timer;
    private Quaternion targetRotation;
    public float rotationSpeed = 5f;

    public Transform firePoint;
    public GameObject prefabMantaShoot;

    int randomNumberAttacks = 0;
    bool timeToHide = false;
    float descendSpeed = 10f;
    private float startingY;
    private float descendTime = 3f;
    bool arrive = false, isGoingLower = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag("boat").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.gameObject.activeInHierarchy)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= radiusAttack)
            {
                Debug.Log("Aqui 0");
                anim.SetBool("isAttacking", true);
            }

            if (anim.GetBool("isAttacking"))
            {
                Debug.Log("Aqui 0.1");
                if (randomNumberAttacks == 0)
                {
                    Debug.Log("Aqui 0.2");
                    // Generate a random number between 1 and 3 (inclusive)
                    randomNumberAttacks = Random.Range(1, 4);

                    // Instantiate a prefabMantaShoot object
                    Rigidbody rb = Instantiate(prefabMantaShoot, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
                    rb.AddForce(transform.forward * 7f, ForceMode.Impulse);

                    if (randomNumberAttacks == 2 || randomNumberAttacks == 3)
                    {
                        // Wait for 0.5 seconds
                        StartCoroutine(WaitAndInstantiate());
                    }
                }
                else if (randomNumberAttacks == 1)
                {
                    Debug.Log("Aqui 0.3");
                    // Reset the randomNumberAttacks variable
                    randomNumberAttacks = 0;
                    //timeToHide = true;
                }
                Debug.Log("Aqui 1");
                anim.SetBool("isHide", true);
                anim.SetBool("isAttacking", false);
                isGoingLower = true;
            }

            if(anim.GetBool("isHide") && isGoingLower)
            {
                StartCoroutine(Lower());
                Debug.Log("Aqui 2");
            }
           
            if(arrive)
            {
                StartCoroutine(Raise());
                anim.SetBool("isHide", false);

                Debug.Log("Aqui 3");
            }
            /*if (timeToHide)
            {
                // Lower the object's y position to 5 over a duration of 3 seconds
                StartCoroutine(Lower());

                /*if(arrive)
                    StartCoroutine(Raise());

                timeToHide = false;
            }*/
        }
    }

    IEnumerator WaitAndInstantiate()
    {
        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Instantiate another prefabMantaShoot object
        Rigidbody rb = Instantiate(prefabMantaShoot, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 7f, ForceMode.Impulse);

        if (Random.Range(1, 4) == 3)
        {
            // Wait for another 0.5 seconds
            yield return new WaitForSeconds(0.5f);

            // Instantiate a third prefabMantaShoot object
            Rigidbody rb2 = Instantiate(prefabMantaShoot, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb2.AddForce(transform.forward * 7f, ForceMode.Impulse);
        }
    }

    IEnumerator Lower()
    {
        // Move the object downwards to a y position of 5 over a duration of 3 seconds
        float duration = 3f;
        float elapsed = 0f;
        float initialY = transform.position.y;
        float targetY = -5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            Vector3 newPos = transform.position;
            newPos.y = Mathf.Lerp(initialY, targetY, t);
            transform.position = newPos;
            yield return null;
        }
        arrive = true;
        isGoingLower = false;
        // Wait for 3 seconds
        /* yield return new WaitForSeconds(3f);

         // Move the object back up to its original y position over a duration of 3 seconds
         elapsed = 0f;
         while (elapsed < duration)
         {
             elapsed += Time.deltaTime;
             float t = Mathf.Clamp01(elapsed / duration);
             Vector3 newPos = transform.position;
             newPos.y = Mathf.Lerp(targetY, initialY, t);
             transform.position = newPos;
             yield return null;
         }*/
    }

    IEnumerator Raise()
    {
        // Move the object downwards to a y position of 5 over a duration of 3 seconds
        float duration = 3f;
        float elapsed = 0f;
        float initialY = -5f;
        float targetY = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            Vector3 newPos = transform.position;
            newPos.y = Mathf.Lerp(initialY, targetY, t);
            transform.position = newPos;
            yield return null;
        }

        arrive = false;
    }
}
