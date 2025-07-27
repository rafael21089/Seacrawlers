using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class oldPirateBossController : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;
    public GameObject player;
    public Animator animator;


    public float chaseRadius = 10f;
    public float attackRadius = 3f;
    float distance;

    Vector3 lookingStartPosition;



    private bool isCooldown = false;
    private float cooldownDuration = 10f;
    private float currentCooldownTime = 0f;


    public float thrustSkillRadius = 3f;
    public float slashSkillRadius = 5f;
    public float castSkillRadius = 7f;

    bool buffIsOn = false;
    float timer = 0;
    float timerDuration = 5;

    public bool healbuff = false;
    public bool damagebuff = false;
    public bool armorbuff = false;

    public GameObject sword;

    public bool onetime = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        StartCooldown();

    }

    // Update is called once per frame
    void Update()
    {

        bufftimer();

        distance = Vector3.Distance(transform.position, player.transform.position);

        if (navMeshAgent.isOnNavMesh)
        {
            if (transform.GetComponent<HealthSystemForDummies>().CurrentHealth <= 0 && onetime == false)
            {
                animator.SetTrigger("dead");
                this.gameObject.tag = "Untagged";
                onetime = true;
                transform.GetComponent<HealthSystemForDummies>().CurrentHealth = 0;
            }
            else
            {
                if (isCooldown)
                {
                    // Count down the cooldown time
                    currentCooldownTime -= Time.deltaTime;

                    if (currentCooldownTime <= 0f)
                    {
                        // Cooldown is over
                        isCooldown = false;
                    }

                    normalStates();

                }
                else
                {

                    int randomNumber = Random.Range(1, 4);

                    if (randomNumber == 1)
                    {
                        if (distance <= thrustSkillRadius)
                        {

                            animator.SetBool("isWalking", false);
                            animator.SetBool("isChasing", false);
                            animator.SetTrigger("ThrustSkill");

                            //// Calculate the direction vector from the enemy to the player
                            Vector3 direction = (lookingStartPosition - transform.position).normalized;

                            // Rotate the enemy towards the player using Quaternion.LookRotation()
                            Quaternion lookRotation = Quaternion.LookRotation(direction);
                            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

                            StartCooldown();

                        }
                        else
                        {
                            normalStates();
                        }
                    }
                    else if (randomNumber == 2)
                    {
                        if (distance <= thrustSkillRadius)
                        {
                            animator.SetBool("isWalking", false);
                            animator.SetBool("isChasing", false);
                            animator.SetTrigger("SlashSkill");

                            //// Calculate the direction vector from the enemy to the player
                            Vector3 direction = (lookingStartPosition - transform.position).normalized;

                            // Rotate the enemy towards the player using Quaternion.LookRotation()
                            Quaternion lookRotation = Quaternion.LookRotation(direction);
                            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

                            StartCooldown();

                        }
                        else
                        {
                            normalStates();
                        }
                    }
                    else if (randomNumber == 3)
                    {
                        if (distance <= castSkillRadius)
                        {
                            animator.SetBool("isWalking", false);
                            animator.SetBool("isChasing", false);
                            animator.SetTrigger("CastSkill");

                            StartCooldown();

                        }
                        else
                        {
                            normalStates();
                        }
                    }


                }
            }
            


           
          


        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Slash") || animator.GetCurrentAnimatorStateInfo(0).IsName("Cast") || animator.GetCurrentAnimatorStateInfo(0).IsName("ThrustSkill") || animator.GetCurrentAnimatorStateInfo(0).IsName("SlashSkill"))
        {
            navMeshAgent.isStopped = true;

        }
        else
        {
            lookingStartPosition = player.transform.position;
            navMeshAgent.isStopped = false;

        }



    }

    void bufftimer()
    {

        if (buffIsOn)
        {
            timer += Time.deltaTime;

            if (timer >= timerDuration)
            {
                timer = 0;
                buffIsOn = false;
                transform.GetChild(3).GetComponent<ParticleSystem>().Stop();
                transform.GetChild(4).GetComponent<ParticleSystem>().Stop();
                transform.GetChild(5).GetComponent<ParticleSystem>().Stop();

                healbuff = false;
                damagebuff = false;
                armorbuff = false;
            }


            if (healbuff == true)
            {
                transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(1f);
            }
           
        }


    }


    private void StartCooldown()
    {
        isCooldown = true;
        currentCooldownTime = cooldownDuration;
    }
    void normalStates()
    {
        if (distance <= attackRadius && !(animator.GetCurrentAnimatorStateInfo(0).IsName("Cast") || animator.GetCurrentAnimatorStateInfo(0).IsName("ThrustSkill") || animator.GetCurrentAnimatorStateInfo(0).IsName("SlashSkill")))
        {
            //attack

            animator.SetBool("isWalking", false);
            animator.SetBool("isChasing", false);
            animator.SetTrigger("isSlashing");

            //// Calculate the direction vector from the enemy to the player
            Vector3 direction = (lookingStartPosition - transform.position).normalized;

            // Rotate the enemy towards the player using Quaternion.LookRotation()
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);



        }
        else if (distance <= chaseRadius)
        {
            //chase
            navMeshAgent.speed = 5f;

            navMeshAgent.SetDestination(player.transform.position);
            animator.SetBool("isChasing", true);
            animator.SetBool("isWalking", false);

            // Calculate the direction vector from the enemy to the player
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // Rotate the enemy towards the player using Quaternion.LookRotation()
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        else
        {
            //walking

            navMeshAgent.speed = 2f;
            navMeshAgent.SetDestination(player.transform.position);
            animator.SetBool("isWalking", true);
            animator.SetBool("isChasing", false);

            // Calculate the direction vector from the enemy to the player
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // Rotate the enemy towards the player using Quaternion.LookRotation()
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
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

                    if (armorbuff == true)
                    {
                        player.transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-10);
                    }

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

                    if (armorbuff == true)
                    {
                        player.transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-10);
                    }
                }
            }


        }

        if (collision.gameObject.name == "swordpirate Variant")
        {
            transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-player.GetComponent<AbilitiesNovo2>().attackDmg);

            if (armorbuff == true)
            {
                player.transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-10);
            }
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



    public void swordOn()
    {
        sword.GetComponent<BoxCollider>().enabled = true;
    }

    public void swordOff()
    {
        sword.GetComponent<BoxCollider>().enabled = false;
    }

    public void castSkill()
    {
        int randomNumber = Random.Range(1, 4);

        if (randomNumber == 1)
        {
            transform.GetChild(3).GetComponent<ParticleSystem>().Play();
            buffIsOn = true;
            armorbuff = true;
        }
        else if (randomNumber == 2)
        {
            transform.GetChild(4).GetComponent<ParticleSystem>().Play();
            buffIsOn = true;
            healbuff = true;

        }
        else if (randomNumber == 3)
        {
            transform.GetChild(5).GetComponent<ParticleSystem>().Play();
            buffIsOn = true;
            damagebuff = true;

        }

    }


    public void dies()
    {
        Destroy(this.gameObject);
    }
}
