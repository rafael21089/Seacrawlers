using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FinishGame : MonoBehaviour
{
    [SerializeField] Canvas victoryCanvas, loseCanvas;
    [SerializeField] GameObject enemyCiclope, enemyPig, enemyDesert, enemyIce, player;
    [SerializeField] TimeController tC;
    private void Update()
    {
        if(tC.daysToFinish == 0)
        {
            if(enemyCiclope == null && enemyPig == null && enemyDesert == null && enemyIce == null)
            {
                victoryCanvas.gameObject.SetActive(true);
            }
            else if(player.GetComponent<HealthSystemForDummies>().CurrentHealth <= 0)
            {
                loseCanvas.gameObject.SetActive(true);
            }
        }
    }

}
