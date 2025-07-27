using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class iceBirdController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public GameObject player;
    public Animator animator;

    public float chaseRadius = 2f;
    public float shootRadius = 2f;

    Vector3 lookingStartPosition;


    public float shootCooldown = 2f;
    private float shootTimer = 0f;

    private HealthSystemForDummies icebirdHealth;
    public bool onetime = false;
    public Collider col;

    ObjectiveManager objectiveManager;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        icebirdHealth = GetComponent<HealthSystemForDummies>();
        objectiveManager = FindObjectOfType<ObjectiveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (navMeshAgent.isOnNavMesh)
        {
            if (icebirdHealth.CurrentHealth <= 0 && onetime == false)
            {
                animator.SetTrigger("dead");
                this.gameObject.tag = "Untagged";
                objectiveManager.Kill15FrostEnemies();
                GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>()?.Counter(this.gameObject);

                onetime = true;
                navMeshAgent.isStopped = true;

            }
            else
            {
                if (distance <= shootRadius)
                {
                    navMeshAgent.isStopped = true;
                    animator.SetBool("isWalking", false);

                    if (shootTimer == 0)
                    {

                        if (animator.GetBool("isAttacking") == false)
                        {
                            animator.SetBool("isAttacking", true);
                        }

                        shootTimer = 1;
                    }
                    else
                    {
                        shootTimer += Time.deltaTime;

                        if (shootTimer >= shootCooldown)
                        {
                            shootTimer = 0;
                        }
                    }

                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {

                        Debug.Log("d");
                        // Store the current position as the jump start position
                        lookingStartPosition = player.transform.position;

                        // Calculate the direction vector from the enemy to the player
                        Vector3 direction = (lookingStartPosition - transform.position).normalized;

                        // Rotate the enemy towards the player using Quaternion.LookRotation()
                        Quaternion lookRotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

                    }

                }
                else
                {
                    shootTimer = 0;
                }

                if (distance <= chaseRadius && distance > shootRadius)
                {
                    navMeshAgent.isStopped = false;

                    navMeshAgent.SetDestination(player.transform.position);
                    animator.SetBool("isWalking", true);

                    // Calculate the direction vector from the enemy to the player
                    Vector3 direction = (player.transform.position - transform.position).normalized;

                    // Rotate the enemy towards the player using Quaternion.LookRotation()
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

                }
                else
                {
                    animator.SetBool("isWalking", false);
                    navMeshAgent.isStopped = true;
                }


                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
                {
                    navMeshAgent.isStopped = true;
                }
            }
        }
        

        



    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "GunBullet")
        {
            if (collision.gameObject.GetComponent<BulletFollow>() != null && collision.gameObject.GetComponent<BulletFollow>().target != null)
            {
                if (collision.gameObject.GetComponent<BulletFollow>().target.name == this.gameObject.name)
                {
                    if (collision.gameObject != null)
                    {
                        Destroy(collision.gameObject);
                    }
                    transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-player.GetComponent<AbilitiesNovo2>().attackDmg);
                }
            }


        }

        if (collision.gameObject.tag == "MagicBullet")
        {
            if (collision.gameObject.GetComponent<MagicFollow>() != null && collision.gameObject.GetComponent<MagicFollow>().target != null)
            {
                if (collision.gameObject.GetComponent<MagicFollow>().target.name == this.gameObject.name)
                {
                    if (collision.gameObject != null)
                    {
                        Destroy(collision.gameObject);
                    }
                    transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-player.GetComponent<AbilitiesNovo2>().attackDmg);
                }
            }


        }

        if (collision.gameObject.name == "swordpirate Variant")
        {
            transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-player.GetComponent<AbilitiesNovo2>().attackDmg);
        }


    }

    void OnParticleCollision(GameObject other)
    {

        if (other.name == "ParticleSystem_Fire_Wall Variant(Clone)")
        {
            transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-1);
        }

        if (other.name == "CircleOfFire Variant(Clone)")
        {
            transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-1);
        }

        if (other.name == "Image(Clone)")
        {
            transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-1);
        }

        if (other.name == "FlameThrower Variant(Clone)")
        {
            transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-10);
        }

        if (other.name == "FireBall Variant")
        {
            transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-150);
            Destroy(other.gameObject.transform.parent.gameObject);
        }

        if (other.name == "LigthningArrow Variant")
        {
            transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-300);
            Destroy(other.gameObject.transform.parent.gameObject);
        }

        if (other.name == "MeteorGunnerSkill Variant")
        {
            transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-10);
        }
    }

    public void attacking()
    {
        animator.SetBool("isAttacking", false);

    }
    public void ApplyDmgg()
    {
        col.enabled = true;

    }

    public void ApplyDmggOff()
    {
        col.enabled = false;
    }


    public void dies()
    {
        Destroy(this.gameObject);
    }
}
