using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRewards : MonoBehaviour
{
    public int playerCurrentLevel = 1;
    int playerCurrentXp = 0;
    public int playerCurrentGold = 0;
    int xpEarned;
    //public int goldEarned = 1;

    public Text goldText;
    [SerializeField] Text xpText;
    [SerializeField] Text levelPlayerText;

    public int level = 1;
    public float experience { get; private set; }
    public Text lvlText;
    [SerializeField] Slider xpBar;

    float expValue = 4;

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            IncreaseGold(100);
        }
        /* if (Input.GetKey(KeyCode.Q))
         {
             //IncreasePlayerLevel();
         }



         if (Input.GetKey(KeyCode.E))
         {
             // IncreaseXp(100);
             Debug.Log("aaaaa" + xpBar.value);
             SetExperience(expValue);
         }*/
    }

    void IncreaseXp(int xpEarned)
    {
        playerCurrentXp += xpEarned;
        //xpText.text = playerCurrentXp.ToString();
    }

    public void IncreaseGold(int goldEarned)
    {
        playerCurrentGold += goldEarned;
        goldText.text = playerCurrentGold.ToString();
    }

    public void DecreaseGold(int goldEarned)
    {
        playerCurrentGold -= goldEarned;
        goldText.text = playerCurrentGold.ToString();
    }

    void IncreasePlayerLevel()
    {
        playerCurrentLevel++;
        levelPlayerText.text = playerCurrentLevel.ToString();
    }

    public static int ExpNeedToLvlUp(int currentLvl)
    {
        return (currentLvl * currentLvl + currentLvl) * 5;
    }

    public void SetExperience(float exp)
    {
        experience += exp;

        float expNeeded = ExpNeedToLvlUp(level);
        float previousExp = ExpNeedToLvlUp(level - 1);

        Debug.Log("aaaaa3 " + experience);
        Debug.Log("aaaaa4 " + expNeeded);
        Debug.Log("aaaaa5 " + previousExp);
        //Level up with xp
        if (experience >= expNeeded)
        {
            LevelUp();
            expNeeded = ExpNeedToLvlUp(level);
            previousExp = ExpNeedToLvlUp(level - 1);
        }

        //Fill xp bar with xp
        xpBar.value = (experience - previousExp) / (expNeeded - previousExp);
        Debug.Log("aaaaa6 " + xpBar.value);
        //Reset the fillbar
        if (xpBar.value >= 1)
        {
            xpBar.value = 0;
        }
    }

    public void LevelUp()
    {
        level++;
        lvlText.text = level.ToString();
    }
}
