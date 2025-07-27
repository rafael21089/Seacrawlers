using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MantaNovo : MonoBehaviour
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
    float targetY = -5f;
    Vector3 offset = new Vector3(0f, 0.0025f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag("boat").transform;
    }

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

            if(anim.GetBool("isAttacking"))
            {
                Attack();
            }

            if (anim.GetBool("isHide") && isGoingLower)
            {
                //Lower();
                //transform.position.y--/* -= new Vector3(0, transform.position.y -1f, 0);*/
                    
                

                while (transform.position.y > targetY)
                {
                    transform.position -= Vector3.Lerp(offset, offset, 0.5f);
                }

                
                /*if (transform.position.y < targetY)
                {
                    arrive = true;
                    isGoingLower = false;
                }*/
                Debug.Log("Aqui 2");
            }

            /*if (arrive)
            {
                Raise();
                anim.SetBool("isHide", false);
                Debug.Log("Aqui 3");
            }*/
        }
    }

    void Attack()
    {
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
                WaitAndInstantiate();
            }
        }
        else if (randomNumberAttacks == 1)
        {
            Debug.Log("Aqui 0.3");
            // Reset the randomNumberAttacks variable
            randomNumberAttacks = 0;
        }

        Debug.Log("Aqui 1");
        anim.SetBool("isHide", true);
        anim.SetBool("isAttacking", false);
        isGoingLower = true;
    }

    void WaitAndInstantiate()
    {
        // Wait for 0.5 seconds
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
        }

        // Instantiate another prefabMantaShoot object
        Rigidbody rb = Instantiate(prefabMantaShoot, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 7f, ForceMode.Impulse);

        if (Random.Range(1, 4) == 3)
        {
            // Wait for another 0.5 seconds
            elapsedTime = 0f;
            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime;
            }

            // Instantiate a third prefabMantaShoot object
            Rigidbody rb2 = Instantiate(prefabMantaShoot, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb2.AddForce(transform.forward * 7f, ForceMode.Impulse);
        }
    }

    void Lower()
    {
        float targetY = -5f;
        float moveSpeed = 0.00001f; // adjust this value to control how fast the character moves down
        float step = moveSpeed;//* Time.deltaTime;

        while (transform.position.y > targetY)
        {
            transform.position -= new Vector3(0, step, 0);
        }

        arrive = true;
        isGoingLower = false;
    }

    void Raise()
    {
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
        }

        Vector3 finalPos = transform.position;
        finalPos.y = targetY;
        transform.position = finalPos;

        arrive = false;
    }
}
