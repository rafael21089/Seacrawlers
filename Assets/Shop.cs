using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int coin, ability;
    public Text coinText, abilityText;
    void Start()
    {
        
    }

    public void BuyItem()
    {
        if(coin >= 200)
        {
            coin -= 200;
            coinText.text = coin.ToString();

            ability += 1;
            abilityText.text = ability.ToString();
        }
        else
        {
            print("Not Enough money");
        }
    }
}
