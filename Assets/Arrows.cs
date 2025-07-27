using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrows : MonoBehaviour
{
    int currentNQuests = 1, previousNQuests = 0;
    public Image[] arrows;
   //public List<ScriptableObject> quests = new List<ScriptableObject>();
    public GameObject player, boat, cubeDirection;
    public ScriptableObject[] quests2;
    public GameObject zone1, zone2, zone3, zone4;
    public GameObject objtNE, objtNW, objtSE, objtSW;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
         currentNQuests = transform.childCount;
        Debug.Log("aawww2" + currentNQuests);
        if (previousNQuests != currentNQuests)
         {
            for (int i = 0; i < quests2.Length; i++)
            {
                foreach (Transform child in transform)
                {
                    if (quests2[i] != null && quests2[i].name == child.name)
                    {
                        string questPlace = child.gameObject.transform.GetChild(3).name;
                        Debug.Log("mmm" + questPlace);

                        if (child.gameObject.transform.GetChild(3).name == "Lava")
                        {
                            if (zone1.name == "LavaZone")
                            {
                                Dir3();/*
                                Vector3 playerToZone1 = zone1.transform.position - cubeDirection.transform.position;
                                float angle = Vector3.Angle(playerToZone1, cubeDirection.transform.forward);
                                Debug.Log("aaww" + angle);
                                GetDirection(angle, Color.red);*/
                                
                            }
                            else if (zone2.name == "LavaZone")
                            {
                                Dir3();/*
                                Vector3 playerToZone2 = zone2.transform.position - cubeDirection.transform.position;
                                float angle = Vector3.Angle(playerToZone2, cubeDirection.transform.forward);
                                Debug.Log("aaww" + angle);
                                GetDirection(angle, Color.red);*/
                            }
                            else if (zone3.name == "LavaZone")
                            {
                                Dir3();/*
                                Vector3 playerToZone3 = zone3.transform.position - cubeDirection.transform.position;
                                float angle = Vector3.Angle(playerToZone3, cubeDirection.transform.forward);
                                Debug.Log("aaww" + angle);
                                GetDirection(angle, Color.red);**/
                            }
                            else if (zone4.name == "LavaZone")
                            {
                                Dir3();/*
                                Vector3 playerToZone4 = zone4.transform.position - cubeDirection.transform.position;
                                float angle = Vector3.Angle(playerToZone4, cubeDirection.transform.forward);
                                Debug.Log("aaww" + angle);
                                GetDirection(angle, Color.red);*/

                            }
                        }else if(child.gameObject.transform.GetChild(3).name == "Ice")
                        {
                            if (zone1.name == "IceZone")
                            {
                                Dir2();
                                /*Vector3 playerToZone1 = zone1.transform.position - cubeDirection.transform.position;
                                float angle = Vector3.Angle(playerToZone1, cubeDirection.transform.forward);
                                Debug.Log("aaww" + angle);
                                GetDirection(angle, Color.blue);*/

                            }
                            else if (zone2.name == "IceZone")
                            {
                                Dir2();
                                /* Vector3 playerToZone2 = zone2.transform.position - cubeDirection.transform.position;
                                 float angle = Vector3.Angle(playerToZone2, cubeDirection.transform.forward);
                                 Debug.Log("aaww" + angle);
                                 GetDirection(angle, Color.blue);*/
                            }
                            else if (zone3.name == "IceZone")
                            {
                                Dir2();
                                /*Vector3 playerToZone3 = zone3.transform.position - cubeDirection.transform.position;
                                float angle = Vector3.Angle(playerToZone3, cubeDirection.transform.forward);
                                Debug.Log("aaww" + angle);
                                GetDirection(angle, Color.blue);*/
                            }
                            else if (zone4.name == "IceZone")
                            {
                                Dir2();
                                /*Vector3 playerToZone4 = zone4.transform.position - cubeDirection.transform.position;
                                float angle = Vector3.Angle(playerToZone4, cubeDirection.transform.forward);
                                Debug.Log("aaww" + angle);
                                GetDirection(angle, Color.blue);*/

                            }
                        }
                        else if (child.gameObject.transform.GetChild(3).name == "Desert")
                        {
                            if (zone1.name == "SandZone")
                            {
                                Dir1();
                               /* Vector3 playerToZone1 = zone1.transform.position - cubeDirection.transform.position;
                                float angle = Vector3.Angle(playerToZone1, cubeDirection.transform.forward);
                                Debug.Log("aaww++" + angle);
                                GetDirection(angle, Color.yellow);*/

                            }
                            else if (zone2.name == "SandZone")
                            {
                                Dir1();
                                /*Vector3 playerToZone2 = zone2.transform.position - cubeDirection.transform.position;
                                float angle = Vector3.Angle(playerToZone2, cubeDirection.transform.forward);
                                Debug.Log("aaww++" + angle);
                                GetDirection(angle, Color.yellow);*/
                            }
                            else if (zone3.name == "SandZone")
                            {
                                Dir1();
                                /*Vector3 playerToZone3 = zone3.transform.position - cubeDirection.transform.position;
                                float angle = Vector3.Angle(playerToZone3, cubeDirection.transform.forward);
                                Debug.Log("aaww++" + angle);
                                GetDirection(angle, Color.yellow);*/
                            }
                            else if (zone4.name == "SandZone")
                            {
                                Dir1();
                                /*Vector3 playerToZone4 = zone4.transform.position - cubeDirection.transform.position;
                                float angle = Vector3.Angle(playerToZone4, cubeDirection.transform.forward);
                                Debug.Log("aaww++" + angle);
                                GetDirection(angle, Color.yellow);*/

                            }
                        }
                        else if (child.gameObject.transform.GetChild(3).name == "Forest")
                        {
                            if (zone1.name == "ForestZone")
                            {
                                Dir();

                            }
                            else if (zone2.name == "ForestZone")
                            {
                            
                                Dir();
                            }
                            else if (zone3.name == "ForestZone")
                            {
                              
                                Dir();
                            }
                            else if (zone4.name == "ForestZone")
                            {
                                Dir();

                            }
                        }

                        break; // Se quiser parar de procurar ap�s encontrar o primeiro filho correspondente
                    }
                }
            }
            previousNQuests = currentNQuests;
            Debug.Log("aawww" + currentNQuests);
        }   
    }

    void Dir()
    {
        if (objtNE.GetComponent<checkPositions>().mainObjt.name == "ForestZone")
        {
            arrows[7].gameObject.SetActive(true);
            arrows[7].color = Color.green;
        }
        else if(objtNW.GetComponent<checkPositions>().mainObjt.name == "ForestZone")
        {
            arrows[6].gameObject.SetActive(true);
            arrows[6].color = Color.green;
        }
        else if(objtSE.GetComponent<checkPositions>().mainObjt.name == "ForestZone")
        {
            arrows[4].gameObject.SetActive(true);
            arrows[4].color = Color.green;
        }
        else if (objtSW.GetComponent<checkPositions>().mainObjt.name == "ForestZone")
        {
            arrows[5].gameObject.SetActive(true);
            arrows[5].color = Color.green;
        }
        /*if(objtNE.GetComponent<checkPositions>().mainObjt.name == "ForestZone")
        {
            arrows[7].gameObject.SetActive(true);
            arrows[7].color = Color.green;
        }
        else if(objtNE.GetComponent<checkPositions>().mainObjt.name == "SandZone")
        {
            arrows[7].gameObject.SetActive(true);
            arrows[7].color = Color.yellow;
        }
        else if (objtNE.GetComponent<checkPositions>().mainObjt.name == "IceZone")
        {
            arrows[7].gameObject.SetActive(true);
            arrows[7].color = Color.blue;
        }
        else if (objtNE.GetComponent<checkPositions>().mainObjt.name == "LavaZone")
        {
            arrows[7].gameObject.SetActive(true);
            arrows[7].color = Color.red;
        }*/
    }

    void Dir1()
    {
        if (objtNE.GetComponent<checkPositions>().mainObjt.name == "SandZone")
        {
            arrows[7].gameObject.SetActive(true);
            arrows[7].color = Color.yellow;
        }
        else if (objtNW.GetComponent<checkPositions>().mainObjt.name == "SandZone")
        {
            arrows[6].gameObject.SetActive(true);
            arrows[6].color = Color.yellow;
        }
        else if (objtSE.GetComponent<checkPositions>().mainObjt.name == "SandZone")
        {
            arrows[4].gameObject.SetActive(true);
            arrows[4].color = Color.yellow;
        }
        else if (objtSW.GetComponent<checkPositions>().mainObjt.name == "SandZone")
        {
            arrows[5].gameObject.SetActive(true);
            arrows[5].color = Color.yellow;
        }
        /*
        if (objtNW.GetComponent<checkPositions>().mainObjt.name == "ForestZone")
        {
            arrows[6].gameObject.SetActive(true);
            arrows[6].color = Color.green;
        }
        else if (objtNW.GetComponent<checkPositions>().mainObjt.name == "SandZone")
        {
            arrows[6].gameObject.SetActive(true);
            arrows[6].color = Color.yellow;
        }
        else if (objtNW.GetComponent<checkPositions>().mainObjt.name == "IceZone")
        {
            arrows[6].gameObject.SetActive(true);
            arrows[6].color = Color.blue;
        }
        else if (objtNW.GetComponent<checkPositions>().mainObjt.name == "LavaZone")
        {
            arrows[6].gameObject.SetActive(true);
            arrows[6].color = Color.red;
        }*/
    }

    void Dir2()
    {
        if (objtNE.GetComponent<checkPositions>().mainObjt.name == "IceZone")
        {
            arrows[7].gameObject.SetActive(true);
            arrows[7].color = Color.blue;
        }
        else if (objtNW.GetComponent<checkPositions>().mainObjt.name == "IceZone")
        {
            arrows[6].gameObject.SetActive(true);
            arrows[6].color = Color.blue;
        }
        else if (objtSE.GetComponent<checkPositions>().mainObjt.name == "IceZone")
        {
            arrows[4].gameObject.SetActive(true);
            arrows[4].color = Color.blue;
        }
        else if (objtSW.GetComponent<checkPositions>().mainObjt.name == "IceZone")
        {
            arrows[5].gameObject.SetActive(true);
            arrows[5].color = Color.blue;
        }
        /*
        if (objtSE.GetComponent<checkPositions>().mainObjt.name == "ForestZone")
        {
            arrows[4].gameObject.SetActive(true);
            arrows[4].color = Color.green;
        }
        else if (objtSE.GetComponent<checkPositions>().mainObjt.name == "SandZone")
        {
            arrows[4].gameObject.SetActive(true);
            arrows[4].color = Color.yellow;
        }
        else if (objtSE.GetComponent<checkPositions>().mainObjt.name == "IceZone")
        {
            arrows[4].gameObject.SetActive(true);
            arrows[4].color = Color.blue;
        }
        else if (objtSE.GetComponent<checkPositions>().mainObjt.name == "LavaZone")
        {
            arrows[4].gameObject.SetActive(true);
            arrows[4].color = Color.red;
        }*/
    }

    void Dir3()
    {
        if (objtNE.GetComponent<checkPositions>().mainObjt.name == "LavaZone")
        {
            arrows[7].gameObject.SetActive(true);
            arrows[7].color = Color.red;
        }
        else if (objtNW.GetComponent<checkPositions>().mainObjt.name == "LavaZone")
        {
            arrows[6].gameObject.SetActive(true);
            arrows[6].color = Color.red;
        }
        else if (objtSE.GetComponent<checkPositions>().mainObjt.name == "LavaZone")
        {
            arrows[4].gameObject.SetActive(true);
            arrows[4].color = Color.red;
        }
        else if (objtSW.GetComponent<checkPositions>().mainObjt.name == "LavaZone")
        {
            arrows[5].gameObject.SetActive(true);
            arrows[5].color = Color.red;
        }

        /*
        if (objtSW.GetComponent<checkPositions>().mainObjt.name == "ForestZone")
        {
            arrows[5].gameObject.SetActive(true);
            arrows[5].color = Color.green;
        }
        else if (objtSW.GetComponent<checkPositions>().mainObjt.name == "SandZone")
        {
            arrows[5].gameObject.SetActive(true);
            arrows[5].color = Color.yellow;
        }
        else if (objtSW.GetComponent<checkPositions>().mainObjt.name == "IceZone")
        {
            arrows[5].gameObject.SetActive(true);
            arrows[5].color = Color.blue;
        }
        else if (objtSW.GetComponent<checkPositions>().mainObjt.name == "LavaZone")
        {
            arrows[5].gameObject.SetActive(true);
            arrows[5].color = Color.red;
        }*/
    }

    void GetDirection(float angle, Color color)
    {
        if (angle >= -22.5f && angle < 22.5f)
        {
            arrows[0].gameObject.SetActive(true);
            arrows[0].color = color;
            Debug.Log("aaww-" );
        }
        else if (angle >= 157.5f || angle < -157.5f)
        {
            arrows[1].gameObject.SetActive(true);
            arrows[1].color = color;
            Debug.Log("aaww-");
        }
        else if (angle >= -112.5f && angle < -67.5f)
        {
            arrows[2].gameObject.SetActive(true);
            arrows[2].color = color;
            Debug.Log("aaww-");
        }
        else if (angle >= 67.5f && angle < 112.5f)
        {
            arrows[3].gameObject.SetActive(true);
            arrows[3].color = color;
            Debug.Log("aaww-");
        }
        else if (angle >= 112.5f && angle < 157.5f)
        {
           
            arrows[4].gameObject.SetActive(true);
            arrows[4].color = color;
            Debug.Log("aaww--");
        }
        else if (angle >= -157.5f && angle < -112.5f)
        {
            arrows[5].gameObject.SetActive(true);
            arrows[5].color = color;
            Debug.Log("aaww--");
        }
        else if (angle >= -67.5f && angle < -22.5f)
        {
            arrows[6].gameObject.SetActive(true);
            arrows[6].color = color;
            Debug.Log("aaww---");
        }
        else if (angle >= 22.5f && angle < 67.5f)
        {
            arrows[7].gameObject.SetActive(true);
            arrows[7].color = color;
            Debug.Log("aaww---");
        }
    }
}
