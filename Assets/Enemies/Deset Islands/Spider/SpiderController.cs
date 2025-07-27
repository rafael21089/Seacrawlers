using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderController : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public float chaseRadius = 20f;
    public float chaseRadiusAttack = 20f;

    public Transform target;
    private Transform pl;
    public NavMeshAgent agent;
    private Animator anim;
    private float timer;
    private bool isChasing;
    private Quaternion targetRotation;
    public float rotationSpeed = 5f;


    private Transform targetTemporary;

    private HealthSystemForDummies spiderHealth;
    public bool onetime = false;

    public Collider col;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        target = null;
        pl = GameObject.FindGameObjectWithTag("Player").transform;
        spiderHealth = GetComponent<HealthSystemForDummies>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (!pl.gameObject.activeInHierarchy)
        {
            target = null;
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }


        

        if (spiderHealth.CurrentHealth <= 0 && onetime == false)
        {
            anim.SetTrigger("dead");
            this.gameObject.tag = "Untagged";
            GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>()?.Counter(this.gameObject);

            agent.isStopped = true;

            onetime = true;
        }
        else
        {
            if (target != null)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (distanceToTarget <= 50)
                {
                    if (distanceToTarget <= chaseRadiusAttack && chaseRadius >= distanceToTarget)
                    {
                        anim.SetBool("isChasing", false);
                        anim.SetBool("isAttacking", true);

                        if (targetTemporary == null)
                        {
                            targetTemporary = target;
                        }
                        else
                        {
                            targetRotation = Quaternion.LookRotation(targetTemporary.position - transform.position);
                            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                        }

                    }
                    else if (distanceToTarget <= chaseRadius && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Far"))
                    {
                        anim.SetBool("isChasing", true);
                        anim.SetBool("isAttacking", false);

                        agent.SetDestination(target.position);
                        targetRotation = Quaternion.LookRotation(target.position - transform.position);
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                    }
                 
                }
          


                if (agent.isOnNavMesh != false)
                {
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Far"))
                    {
                        agent.isStopped = true;
                    }

                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
                    {
                        agent.isStopped = false;
                    }
                }

            }
                

            

        }

        if (agent.isOnNavMesh != false)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                agent.isStopped = true;
            }
        }
        


    }


    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        NavMeshHit navHit;
        Vector3 randDirection = Vector3.zero;
        int attempts = 0;

        while (attempts < 50)
        {
            randDirection = Random.onUnitSphere * dist;
            randDirection += origin;

            if (NavMesh.SamplePosition(randDirection, out navHit, dist, layermask))
            {
                return navHit.position;
            }

            attempts++;
        }

        return origin;
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
                    transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-target.GetComponent<AbilitiesNovo2>().attackDmg);
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
                    transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-target.GetComponent<AbilitiesNovo2>().attackDmg);
                }
            }


        }

        if (collision.gameObject.name == "swordpirate Variant")
        {
            transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-target.GetComponent<AbilitiesNovo2>().attackDmg);
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


    public void attackSpiderAnim()
    {
        anim.SetBool("isAttacking", false);

        targetTemporary = null;
    }


    public void dies()
    {
        Destroy(this.gameObject);
    }


}
