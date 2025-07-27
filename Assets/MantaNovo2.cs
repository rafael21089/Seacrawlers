using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MantaNovo2 : MonoBehaviour
{
    public float attackRadius = 20f;
    public GameObject ballPrefab;
    public Transform ballSpawnPoint;
    public float ballSpawnDelay = 0.5f;
    public float hideDelay = 2f;
    public float hidePosition = -5f;
    public float rotateSpeed = 5f;

    private enum CharacterState { Idle, Attack, Hide }
    private CharacterState currentState = CharacterState.Idle;

    private GameObject player;
    private Coroutine attackCoroutine;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    public Transform boat;
    public Transform target;

    public Animator anim;

    public NavMeshAgent agent;
    private Quaternion targetRotation;
    private float timer;
    private bool isChasing;
    public float wanderTimer = 5f;
    public float chaseRadius = 40f;
    public float wanderRadius = 10f;
    public float rotationSpeed = 5f;

    public bool onetime = false;
    public GameObject Explosion;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update()
    {

        if (boat == null && GameObject.FindGameObjectWithTag("boat") != null)
        {
            boat = GameObject.FindGameObjectWithTag("boat").transform;
        }


        if (Vector3.Distance(transform.position, boat.transform.position) <= 100)
        {
            anim.enabled = true;

            if (this.GetComponent<HealthSystemForDummies>().CurrentHealth <= 0 && onetime == false)
            {
                anim.SetBool("isChasing", false);

                anim.SetTrigger("dead");

                agent.isStopped = true;

                onetime = true;
            }
            else
            {
                switch (currentState)
                {
                    case CharacterState.Idle:
                        if (Vector3.Distance(transform.position, boat.transform.position) <= attackRadius)
                        {
                            StartAttack();
                        }
                        break;

                    case CharacterState.Attack:
                        // do nothing
                        break;

                    case CharacterState.Hide:
                        // do nothing
                        break;

                    default:
                        break;
                }

                if (Vector3.Distance(transform.position, boat.transform.position) <= chaseRadius)
                {
                    isChasing = true;

                    if (Vector3.Distance(transform.position, boat.transform.position) <= attackRadius)
                    {
                        anim.SetBool("isChasing", false);
                    }
                    else
                    {
                        anim.SetBool("isChasing", true);
                    }

                    initialPosition = transform.position;
                    initialRotation = transform.rotation;

                    agent.SetDestination(boat.position);
                    targetRotation = Quaternion.LookRotation(boat.position - transform.position);
                }

                // rotate towards player while within attack radius
                if (Vector3.Distance(transform.position, boat.transform.position) <= attackRadius)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(boat.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
                }
                else
                {
                    // Smoothly rotate towards target rotation

                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }

        }
        else
        {
            anim.enabled = false;

        }

       

        

    }

    void StartAttack()
    {
        currentState = CharacterState.Attack;
        attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        int numberOfBalls = Random.Range(1, 4);

        for (int i = 0; i < numberOfBalls; i++)
        {
            //Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);
            Rigidbody rb = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            Vector3 directionToPlayer = (boat.transform.position - ballSpawnPoint.position).normalized;
            rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
            yield return new WaitForSeconds(ballSpawnDelay);
        }

        yield return StartCoroutine(HideCoroutine());
    }

    IEnumerator HideCoroutine()
    {
        currentState = CharacterState.Hide;

        Vector3 hidePositionVector = new Vector3(initialPosition.x, hidePosition, initialPosition.z);
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(initialPosition, hidePositionVector, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //yield return StartCoroutine(AppearCoroutine(transform.position));
        //yield return new WaitForSeconds(hideDelay);

        elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(hidePositionVector, initialPosition, elapsedTime);
            //transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentState = CharacterState.Idle;
    }

    IEnumerator AppearCoroutine(Vector3 pos)
    {
        Vector3 appearPositionVector = new Vector3(pos.x, initialPosition.y, pos.z);
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(appearPositionVector, initialPosition, elapsedTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentState = CharacterState.Idle;
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    public void dies()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.name == "CanonBall(Clone)")
        {
            //hit
            this.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-target.gameObject.GetComponent<BoatMovement>().damageOfBoat);
            GameObject newobj = Instantiate(Explosion, this.transform.position, Explosion.transform.rotation);

            Destroy(other.gameObject);
        }


    }
}
