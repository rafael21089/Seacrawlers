using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackDmg;
    public float attackSpeed;
    public float attackTime;
    HeroCombat heroCombatScript;
    HealthSystemForDummies healthSystem;
    PlayerRewards playerRewardScript;
    

    //public Quest quest;

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = GetComponent<HealthSystemForDummies>();
        
        heroCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroCombat>();
        playerRewardScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRewards>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!healthSystem.IsAlive)
        {
            if(gameObject.CompareTag("Enemy"))
            {
                
                //playerRewardScript.goldEarned = 50;
                // playerRewardScript.IncreaseGold(50);
                //playerRewardScript.SetExperience(5);
                //Destroy(gameObject);
                //Debug.Log("Destroy");
                //heroCombatScript.targetedEnemy = null; 
                //heroCombatScript.performMeleeAttack = false;
                //quest.goal.EnemyKilled();
                //Debug.Log(quest.goal.currentAmount);
                //Debug.Log("fuwsqad00");
                /*if (quest.goal.isReached()) 
                {
                    Debug.Log("fuck00");
                    playerRewardScript.IncreaseGold(quest.goldReward);
                    playerRewardScript.SetExperience(quest.experienceReward);
                    quest.Completed();
                    quest.isActive = false;
                }
                */
            }
            
        }
    }
}
