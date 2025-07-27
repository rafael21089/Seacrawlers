using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class mantisController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public GameObject player;
    public Animator animator;

    public float chaseRadius = 2f;
    public float attackRadius = 2f;

    Vector3 lookingStartPosition;

    public float attackCooldown = 2f;
    private float attackTimer = 0f;

    private HealthSystemForDummies mantisHealth;
    public bool onetime = false;

    public Collider col;

    ObjectiveManager objectiveManager;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        mantisHealth = GetComponent<HealthSystemForDummies>();
        objectiveManager = FindObjectOfType<ObjectiveManager>();

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (navMeshAgent.isOnNavMesh)
        {
            if (mantisHealth.CurrentHealth <= 0 && onetime == false)
            {
                animator.SetTrigger("dead");
                this.gameObject.tag = "Untagged";
                objectiveManager.Kill2Mantis();
                GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>()?.Counter(this.gameObject);

                onetime = true;
                navMeshAgent.isStopped = true;

            }
            else
            {
                if (distance <= attackRadius)
                {
                    navMeshAgent.isStopped = true;
                    animator.SetBool("isWalking", false);

                    if (attackTimer == 0)
                    {

                        if (animator.GetBool("slash") == false)
                        {
                            animator.SetBool("slash", true);

                            // Store the current position as the jump start position
                            lookingStartPosition = player.transform.position;
                        }

                        attackTimer = 1;
                    }
                    else
                    {
                        attackTimer += Time.deltaTime;

                        if (attackTimer >= attackCooldown)
                        {
                            attackTimer = 0;
                        }
                    }

                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
                    {

                        // Calculate the direction vector from the enemy to the player
                        Vector3 direction = (lookingStartPosition - transform.position).normalized;

                        // Rotate the enemy towards the player using Quaternion.LookRotation()
                        Quaternion lookRotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

                    }

                }
                else
                {
                    attackTimer = 0;
                }

                if (distance <= chaseRadius && distance > attackRadius)
                {
                    navMeshAgent.isStopped = false;

                    navMeshAgent.SetDestination(player.transform.position);
                    animator.SetBool("isWalking", true);

                    // Calculate the direction vector from the enemy to the player
                    Vector3 direction = (player.transform.position - transform.position).normalized;

                    // Rotate the enemy towards the player using Quaternion.LookRotation()
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

                }


                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
                {

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

    public void slash()
    {
        animator.SetBool("slash", false);

    }

    public void dies()
    {
        Destroy(this.gameObject);
    }

    public void ApplyDmgg()
    {
        col.enabled = true;

    }

    public void ApplyDmggOff()
    {
        col.enabled = false;
    }

}
