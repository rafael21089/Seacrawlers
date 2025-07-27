using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class marksmanController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public GameObject player;
    public Animator animator;

    public float chaseRadius = 2f;
    public float shootRadius = 2f;

    Vector3 lookingStartPosition;

    public GameObject prefabShooting;
    public GameObject ShootingPosition1;
    public float shootCooldown = 2f;
    private float shootTimer = 0f;

    public float skillCooldown = 2f;
    private float skillTimer = 0f;

    public float skillRadius = 5f;

    private HealthSystemForDummies marksmanHealth;

    public bool onetime = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        marksmanHealth = GetComponent<HealthSystemForDummies>();

        SetEnemyTransparency(1f);
        prefabShooting.GetComponent<ParticleSystem>().Clear();
        prefabShooting.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);


        if (navMeshAgent.isOnNavMesh)
        {
            if (marksmanHealth.CurrentHealth <= 0 && onetime == false)
            {
                animator.SetTrigger("dead");
                this.gameObject.tag = "Untagged";
                GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>()?.Counter(this.gameObject);

                onetime = true;
            }
            else
            {
                if (distance < skillRadius)
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


                }

                else if (distance <= shootRadius)
                {
                    navMeshAgent.isStopped = true;
                    animator.SetBool("isWalking", false);

                    if (shootTimer == 0)
                    {
                        if (animator.GetBool("shoot") == false)
                        {
                            animator.SetBool("shoot", true);
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


                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
                    {
                        lookingStartPosition = player.transform.position;
                    }

                }
                else
                {
                    shootTimer = 0;
                }

                if (distance <= chaseRadius && distance > shootRadius && !animator.GetCurrentAnimatorStateInfo(0).IsName("InvisibleSkill") && !animator.GetCurrentAnimatorStateInfo(0).IsName("WalkingBackwards"))
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


                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("InvisibleSkill"))
                {
                    SetEnemyTransparency(1f);
                }


                if (animator.GetCurrentAnimatorStateInfo(0).IsName("InvisibleSkill") || animator.GetCurrentAnimatorStateInfo(0).IsName("WalkingBackwards"))
                {
                    navMeshAgent.isStopped = false;
                    animator.SetBool("isWalking", false);
                    animator.SetBool("shoot", false);

                    Vector3 directionToPlayer = player.transform.position - transform.position;
                    float distance2 = 10f; // set the distance you want the character to move away from the player
                    Vector3 oppositeDirection = -directionToPlayer.normalized * distance2;
                    Vector3 newPosition = transform.position + oppositeDirection;
                    navMeshAgent.SetDestination(newPosition);

                    // Calculate the direction vector from the enemy to the player
                    Vector3 direction = (lookingStartPosition - transform.position).normalized;

                    // Rotate the enemy towards the player using Quaternion.LookRotation()
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

                    SetEnemyTransparency(0.1f);

                }


                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
                {
                    // Calculate the direction vector from the enemy to the player
                    Vector3 direction = (lookingStartPosition - transform.position).normalized;

                    // Rotate the enemy towards the player using Quaternion.LookRotation()
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);

                }
            }
        }

        



       

    }

    


    public void shooting()
    {
        animator.SetBool("shoot", false);

        prefabShooting.GetComponent<ParticleSystem>().Play();


    }

    public void stop_shooting()
    {

        prefabShooting.GetComponent<ParticleSystem>().Clear();
        prefabShooting.GetComponent<ParticleSystem>().Stop();


    }


    public void invisibleSkill()
    {
        animator.SetBool("Skill", false);
    }


    public void SetEnemyTransparency(float transparency)
    {
        

        Renderer enemyRenderer = transform.GetChild(0).GetComponent<Renderer>();
        Material enemyMaterial = enemyRenderer.material;
        Color enemyColor = enemyMaterial.GetColor("_BaseColor");
        enemyColor.a = transparency;
        enemyMaterial.SetColor("_BaseColor", enemyColor);

        Renderer enemyRenderer2 = transform.GetChild(1).GetComponent<Renderer>();
        Material enemyMaterial2 = enemyRenderer2.material;
        Color enemyColor2 = enemyMaterial2.GetColor("_BaseColor");
        enemyColor2.a = transparency;
        enemyMaterial2.SetColor("_BaseColor", enemyColor2);



        Renderer enemyRenderer3 = prefabShooting.transform.parent.GetComponent<Renderer>();
        Material enemyMaterial3 = enemyRenderer3.material;
        Color enemyColor3 = enemyMaterial3.GetColor("_BaseColor");
        enemyColor3.a = transparency;
        enemyMaterial3.SetColor("_BaseColor", enemyColor2);


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


    public void dies()
    {
        Destroy(this.gameObject);
    }
}
