using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCombat : MonoBehaviour
{
    public enum HeroAttackType { Melee, Ranged}
    public HeroAttackType heroAttackType;
    //public GameObject targetedEnemy;
    //public float attackRange;
    //public float rotateSpeedForAttack;

    private movement moveScript;
    //public bool basicAtkIdle = false;
    //public bool isHeroAlive;
    //public bool performMeleeAttack = true;

    Attack attackScript;
    public Animator animator;

    public float range = 5.0f; // the radius of the circle range
    public Transform playerTransform; // the Transform component of the player GameObject

    public bool targetting = false;

    public GameObject target; // the current target enemy, if any


    private void Start()
    {
        moveScript = GetComponent<movement>();
    }

    void Update()
    {
        


        //if(targetedEnemy != null)
        //{
        //    if(Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) > attackRange)
        //    {
        //        moveScript.agent.SetDestination(targetedEnemy.transform.position);
        //        moveScript.agent.stoppingDistance = attackRange;


        //        //rotation
        //        Quaternion rotationToLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
        //        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref moveScript.rotateVelocity, rotateSpeedForAttack * (Time.deltaTime * 5));
        //        transform.eulerAngles = new Vector3(0, rotationY, 0);
        //    }
        //    else
        //    {
        //        if(heroAttackType == HeroAttackType.Melee)
        //        {
        //            if(performMeleeAttack)
        //            {
        //               StartCoroutine(MeleeAttackInterval());
        //            }
        //        }
        //    }
        //}
    }
    //IEnumerator MeleeAttackInterval()
    //{
    //    performMeleeAttack = false;
    //    anim.SetBool("isAttacking", true);

    //    yield return new WaitForSeconds(attackScript.attackTime / ((100 + attackScript.attackTime) * 0.01f));
    
    //    if(targetedEnemy == null)
    //    {
    //        anim.SetBool("isAttacking", false);
    //        performMeleeAttack = true;
    //    }
    //}

    //public void MeleeAttack()
    //{
    //    if (targetedEnemy != null)
    //    {
    //        if (targetedEnemy.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Cube)
    //        {
    //            targetedEnemy.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(attackScript.attackDmg * (-1));
    //        }
    //    }

    //    performMeleeAttack = true;
    //}
}
