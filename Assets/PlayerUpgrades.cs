using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class PlayerUpgrades : MonoBehaviour
{

    public NavMeshAgent BoatAgent;


    public GameObject upgradeBoat;
    public GameObject upgradeBoatStats;
    public GameObject upgradeBoatAddons;
    public GameObject upgradeBoatChoose;


    public GameObject[] statSpeedImages;
    public GameObject[] statDamageImages;
    public GameObject[] statHealthImages;


    public int points = 0;
    public int Allpoints = 0;
    public int money;

    // Update is called once per frame


    public TextMeshProUGUI txtPoints;


    private float boatSpeed;
    private float boatDamage;
    private float boatHealth;

    public GameObject[] Cannon;
    public GameObject[] Button;





    private void Start()
    {
        money = GetComponent<PlayerRewards>().playerCurrentGold;
    }
    void Update()
    {
        if (BoatAgent == null)
        {
            BoatAgent = GameObject.FindGameObjectWithTag("boat").GetComponent<NavMeshAgent>();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == "kaspar")
            {
                OpenUpgradeBoat();
            }
        }

        money = GetComponent<PlayerRewards>().playerCurrentGold;
        txtPoints.text = "Buy Points: " + points;
    }


    public void BuyPoints()
    {
        int money2 = money;

        money2 = money2 - 500;

        if (Allpoints <= 8)
        {
            if (money2 >= 0)
            {
                money = money - 500;
                points = points + 1;
                Allpoints = Allpoints + 1;
                GetComponent<PlayerRewards>().DecreaseGold(500);
            }
        }
       

    }


    public void IncreaseSpeed()
    {
        //BoatAgent.speed += 1.5f;


        if (points > 0)
        {

            int c = 0;

            for (int i = 0; i < statSpeedImages.Length; i++)
            {
                if (statSpeedImages[i].GetComponent<Image>().color == Color.red)
                {
                    c = c + 1;
                }
            }

            if (c != 3)
            {
                for (int i = 0; i < statSpeedImages.Length; i++)
                {
                    if (statSpeedImages[i].GetComponent<Image>().color == Color.white)
                    {
                        statSpeedImages[i].GetComponent<Image>().color = Color.red;
                        break;
                    }
                }

                points = points - 1;

                BoatAgent.speed += 1.5f;
            }
            
        }

        
        
    }

    public void IncreaseDamage()
    {
        if (points > 0)
        {
            int c = 0;

            for (int i = 0; i < statDamageImages.Length; i++)
            {
                if (statDamageImages[i].GetComponent<Image>().color == Color.red)
                {
                    c = c + 1;
                }
            }

            if (c != 3)
            {
                for (int i = 0; i < statDamageImages.Length; i++)
                {
                    if (statDamageImages[i].GetComponent<Image>().color == Color.white)
                    {
                        statDamageImages[i].GetComponent<Image>().color = Color.red;
                        break;
                    }
                }

                points = points - 1;

                BoatAgent.gameObject.GetComponent<BoatMovement>().damageOfBoat += 100;
            }

            

        }

        

    }

    public void IncreaseHealth()
    {

        if (points > 0)
        {

            int c = 0;

            for (int i = 0; i < statHealthImages.Length; i++)
            {
                if (statHealthImages[i].GetComponent<Image>().color == Color.red)
                {
                    c = c + 1;
                }
            }

            if (c != 3)
            {
                for (int i = 0; i < statHealthImages.Length; i++)
                {
                    if (statHealthImages[i].GetComponent<Image>().color == Color.white)
                    {
                        statHealthImages[i].GetComponent<Image>().color = Color.red;
                        break;
                    }
                }


                points = points - 1;

                BoatAgent.gameObject.GetComponent<HealthSystemForDummies>().MaximumHealth += 200;
                BoatAgent.gameObject.GetComponent<HealthSystemForDummies>().ReviveWithMaximumHealth();
            }

        }

        

    }



    public void OpenUpgradeBoat()
    {
        upgradeBoat.SetActive(true);
    }

    public void CloseUpgradeBoat()
    {
        upgradeBoatStats.SetActive(false);
        upgradeBoatAddons.SetActive(false);
        upgradeBoatChoose.SetActive(true);
        upgradeBoat.SetActive(false);
    }

    public void OpenStatUpgradeBoat()
    {
        upgradeBoatStats.SetActive(true);
        upgradeBoatChoose.SetActive(false);
    }

    public void OpenAddonUpgradeBoat()
    {
        upgradeBoatAddons.SetActive(true);
        upgradeBoatChoose.SetActive(false);

    }

    public void BuyCannon()
    {
        int money2 = money;

        money2 = money2 - 1000;

        if (money2 >= 0)
        {
            for (int i = 0; i < Cannon.Length; i++)
            {
                if (!Cannon[i].activeInHierarchy && i == 0)
                {
                    Cannon[i].SetActive(true);
                    GetComponent<PlayerRewards>().DecreaseGold(1000);

                    Button[i].SetActive(false);
                    Button[i+1].SetActive(true);

                    break;
                }
                else if (!Cannon[i].activeInHierarchy && i == 1)
                {
                    Cannon[i].SetActive(true);
                    GetComponent<PlayerRewards>().DecreaseGold(1000);

                    Button[i].SetActive(false);
                    Button[i + 1].SetActive(true);

                    break;
                }
                else if (!Cannon[i].activeInHierarchy && i == 2)
                {
                    Cannon[i].SetActive(true);
                    Cannon[i+1].SetActive(true);
                    GetComponent<PlayerRewards>().DecreaseGold(1000);

                    Button[i].SetActive(false);

                    break;
                }
            }

           
        }


    }

   
}
