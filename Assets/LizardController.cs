using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LizardController : MonoBehaviour
{
    public float chaseRadius = 10f;
    public float attackRadius = 2f;

    public float jumpRange = 3f;
    public float jumpCooldown = 5f;
    public float jumpDuration = 1f;
    public float jumpSpeed = 10f;
    public Animator animator;
    public GameObject player;

    private NavMeshAgent navMeshAgent;
    private bool canJump = true;
    private bool isJumping = false;
    private float jumpTimer = 0f;

    public bool onetime2 = false;

    Vector3 jumpStartPosition;

    private HealthSystemForDummies lizardHealth;

    public Collider col;

    private int onetime = 0;

    ObjectiveManager objectiveManager;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        lizardHealth = GetComponent<HealthSystemForDummies>();
        objectiveManager = FindObjectOfType<ObjectiveManager>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (navMeshAgent.isOnNavMesh)
        {
            if (lizardHealth.CurrentHealth <= 0 && onetime2 == false)
            {
                animator.SetTrigger("dead");
                this.gameObject.tag = "Untagged";
                objectiveManager.Kill10VulcanicEnemies();
                objectiveManager.Kill3Lizards();

                GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>()?.Counter(this.gameObject);
                onetime2 = true;
            }
            else
            {
               
                    if (distance <= attackRadius)
                    {

                        navMeshAgent.isStopped = true;

                        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Stab"))
                        {
                            // Store the current position as the jump start position
                            jumpStartPosition = player.transform.position;

                            // Calculate the direction vector from the enemy to the player
                            Vector3 direction = (jumpStartPosition - transform.position).normalized;

                            // Rotate the enemy towards the player using Quaternion.LookRotation()
                            Quaternion lookRotation = Quaternion.LookRotation(direction);
                            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
                        }



                        animator.SetTrigger("Stab");
                        StartCoroutine(StabCooldown());

                        // Stop the NavMeshAgent while jumping

                    }
                    else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Stab"))
                    {

                        if (!isJumping && distance <= chaseRadius)
                        {
                            jumpStartPosition = player.transform.position;

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
                            // Stop the NavMeshAgent while jumping
                            navMeshAgent.isStopped = true;

                        }

                        if (!isJumping && distance <= jumpRange && canJump)
                        {
                            // Calculate the direction vector from the enemy to the player
                            Vector3 direction = (player.transform.position - transform.position).normalized;

                            // Rotate the enemy towards the player using Quaternion.LookRotation()
                            Quaternion lookRotation = Quaternion.LookRotation(direction);
                            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

                            // Store the current position as the jump start position
                            jumpStartPosition = player.transform.position;

                            // Trigger the jump animation
                            animator.SetTrigger("Dash");

                            // Disable jumping temporarily
                            canJump = false;

                            // Reset the jump timer
                            jumpTimer = 0f;

                            // Stop the NavMeshAgent while jumping
                            navMeshAgent.isStopped = true;

                            StartCoroutine(JumpCooldown());

                        }

                        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
                        {
                            transform.position = Vector3.MoveTowards(transform.position, jumpStartPosition, jumpSpeed * Time.deltaTime);
                            onetime = 1;
                            isJumping = true;
                        }
                        else
                        {
                            if (onetime == 1)
                            {
                                navMeshAgent.isStopped = true;
                                StartCoroutine(ResumeChase());
                                onetime = 0; // Change onetime to 2 to indicate that the coroutine has started
                            }

                        }
                    }

                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
                    {
                        navMeshAgent.isStopped = true;
                    }
                
            }

         }
        
    }

    

    IEnumerator ResumeChase()
    {
        yield return new WaitForSeconds(1f);
        navMeshAgent.isStopped = false;
        isJumping = false;

    }


    IEnumerator StabCooldown()
    {
        yield return new WaitForSeconds(1f);
        navMeshAgent.isStopped = false;

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

    public void IsJumping()
    {
        isJumping = true;
    }

    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
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
