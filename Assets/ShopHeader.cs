using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHeader : MonoBehaviour
{
    public Text currentGoldShop;
    public Text currentLevelShop;
    PlayerRewards pR;

    // Start is called before the first frame update
    void Start()
    {
        pR = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRewards>();

        currentGoldShop.text = pR.playerCurrentGold.ToString();
        currentLevelShop.text = pR.playerCurrentLevel.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        currentGoldShop.text = pR.playerCurrentGold.ToString();
        currentLevelShop.text = pR.playerCurrentLevel.ToString();
    }
}
