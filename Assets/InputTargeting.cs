using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTargeting : MonoBehaviour
{
    public GameObject selectedHero;
    public bool heroPlayer;
    RaycastHit hit;
    public Camera cam;
    private void Start()
    {
        selectedHero = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //cube targeting
        if(Input.GetMouseButtonDown(1))
        {
           
            Ray r = cam.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(r, out hit, Mathf.Infinity))
            {
                //if cube is targetable
                if (hit.collider.gameObject.GetComponent<Targetable>() != null)
                    if (hit.collider.gameObject.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Cube)
                    {
                       
                        //selectedHero.GetComponent<HeroCombat>().targetedEnemy = hit.collider.gameObject;
                    }
            }
            else if(hit.collider.gameObject.GetComponent<Targetable>() == null)
            {
                
                //selectedHero.GetComponent<HeroCombat>().targetedEnemy = null;
            }
        }
    }
}
