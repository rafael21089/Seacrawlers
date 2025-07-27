using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MantaBehaviour : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.gameObject.activeInHierarchy)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= radiusAttack)
            {
                anim.SetBool("isAttacking", true);
            }

            if(anim.GetBool("isAttacking"))
            {
                if (randomNumberAttacks == 0)
                {
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
                    // Reset the randomNumberAttacks variable
                    randomNumberAttacks = 0;
                    timeToHide = true;
                    anim.SetBool("isAttacking", false);
                }

                
            }

            if(timeToHide)
            {
                // Lower the object's y position to 5 over a duration of 3 seconds
                StartCoroutine(LowerAndRaise());
                timeToHide = false;
            }
        }
    }

    IEnumerator LowerAndRaise()
    {
        // Move the object downwards to a y position of 5 over a duration of 3 seconds
        float duration = 3f;
        float elapsed = 0f;
        float initialY = transform.position.y;
        float targetY = 5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            Vector3 newPos = transform.position;
            newPos.y = Mathf.Lerp(initialY, targetY, t);
            transform.position = newPos;
            yield return null;
        }

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

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
        }
    }

    /*IEnumerator LowerAndRaise()
    {
        while (transform.position.y > 5f)
        {
            transform.position -= new Vector3(0, descendSpeed, 0);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(descendTime);

        while (transform.position.y < startingY)
        {
            transform.position += new Vector3(0, descendSpeed, 0);
            yield return new WaitForEndOfFrame();
        }

    }*/

    IEnumerator WaitAndInstantiate()
    {
        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Instantiate another prefabMantaShoot object
        Rigidbody rb = Instantiate(prefabMantaShoot, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 7f, ForceMode.Impulse);

        if (randomNumberAttacks == 3)
        {
            // Wait for another 0.5 seconds
            yield return new WaitForSeconds(0.5f);

            // Instantiate a third prefabMantaShoot object
            Rigidbody rb2 = Instantiate(prefabMantaShoot, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb2.AddForce(transform.forward * 7f, ForceMode.Impulse);
        }

    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}
