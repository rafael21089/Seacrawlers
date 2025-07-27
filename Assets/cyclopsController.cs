using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class cyclopsController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public GameObject player;
    public Animator animator;

    public float chaseRadius = 2f;
    public float shootRadius = 2f;

    Vector3 lookingStartPosition;

    public GameObject prefabCircle;
    public GameObject prefabExplosion;
    public float shootCooldown = 2f;
    private float shootTimer = 0f;

    public float skillCooldown = 2f;
    private float skillTimer = 0f;

    public float skillRadius = 5f;

    private HealthSystemForDummies cyclopsHealth;
    public bool onetime = false;
    public Collider col;
    public Collider colExplosion;

    ObjectiveManager objectiveManager;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        prefabCircle.GetComponent<ParticleSystem>().Stop();
        prefabExplosion.GetComponent<ParticleSystem>().Stop();
        cyclopsHealth = GetComponent<HealthSystemForDummies>();

        objectiveManager = FindObjectOfType<ObjectiveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (navMeshAgent.isOnNavMesh)
        {
            if (cyclopsHealth.CurrentHealth <= 0 && onetime == false)
            {
                animator.SetTrigger("dead");
                this.gameObject.tag = "Untagged";
                objectiveManager.KillVulcanicBoss();
                objectiveManager.KillCyclopes();
                GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>()?.Counter(this.gameObject);

                onetime = true;
            }
            else
            {
                if (distance <= skillRadius)
                {

                    if (skillTimer == 0)
                    {

                        if (animator.GetBool("Skill") == false)
                        {
                            animator.SetBool("Skill", true);
                        }

                        skillTimer = 1;
                    }
                    else
                    {
                        skillTimer += Time.deltaTime;

                        if (skillTimer >= skillCooldown)
                        {
                            skillTimer = 0;
                        }
                    }

                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SmashSkill"))
                    {
                        lookingStartPosition = player.transform.position;
                    }


                }
                if (distance <= shootRadius && animator.GetBool("Skill") == false)
                {
                    navMeshAgent.isStopped = true;
                    animator.SetBool("isWalking", false);

                    if (shootTimer == 0)
                    {
                        if (animator.GetBool("slash") == false)
                        {
                            animator.SetBool("slash", true);
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


                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
                    {
                        lookingStartPosition = player.transform.position;
                    }

                }
                else
                {
                    shootTimer = 0;
                }

                if (distance <= chaseRadius && distance > shootRadius && !animator.GetCurrentAnimatorStateInfo(0).IsName("SmashSkill"))
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



                if (animator.GetCurrentAnimatorStateInfo(0).IsName("SmashSkill"))
                {
                    navMeshAgent.isStopped = true;
                    animator.SetBool("isWalking", false);
                    animator.SetBool("slash", false);

                    //// Calculate the direction vector from the enemy to the player
                    Vector3 direction = (lookingStartPosition - transform.position).normalized;

                    // Rotate the enemy towards the player using Quaternion.LookRotation()
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

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
    public void slash()
    {
        animator.SetBool("slash", false);

    }

    public void circle()
    {
        prefabCircle.GetComponent<ParticleSystem>().Play();
    }

    public void circleStop()
    {
        prefabCircle.GetComponent<ParticleSystem>().Clear();
        prefabCircle.GetComponent<ParticleSystem>().Stop();
    }
    public void Explosion()
    {
        animator.SetBool("Skill", false);

        prefabExplosion.GetComponent<ParticleSystem>().Play();

        colExplosion.enabled = true;

    }

    public void stopExplosion()
    {
        colExplosion.enabled = false;

        prefabExplosion.GetComponent<ParticleSystem>().Stop();
    }
}
