using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movement : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    RaycastHit rHit;
    [SerializeField] Animator anim;

    public float rotateSpeedMovement = 0.1f;
    public float rotateVelocity;


    public Vector3 targetPosition; // Target position to move towards
    public float speed = 5f; // Movement speed
    public bool isMoving; // Flag to indicate if the GameObject is currently moving
    public float smoothTime = 0.5f; // Smooth time for interpolation
    private Vector3 velocity; // Velocity for smooth interpolation

    private NavMeshPath path;
    private int currentCornerIndex = 0;



    // ---------------  Para attackar

    public float range; // the radius of the circle range
    public Transform playerTransform; // the Transform component of the player GameObject
    public bool targetting = false;
    public GameObject target; // the current target enemy, if any

    public bool inRange;


    public GameObject[] effects;


    // ----------------- Class Changing
    public enum Class
    {
        Gunner,
        Swordsman,
        Mage
    }

    public Class classeEscolhida;
    public GameObject[] weapons;
    public GameObject weaponActiveNow;
    [SerializeField] public LayerMask layerMask;
    public float snapDistance = 1.0f;


    // Start is called before the first frame update


    public float navmeshHeightOffset = 0.1f; // the height offset to use when projecting on the NavMesh
    public LayerMask navmeshLayer; // the layer to use for the NavMesh
    public GameObject cubefordebug;

    bool walkingtoIt = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();

        weapons = GameObject.FindGameObjectsWithTag("ClassWeapon");
        for (int i = 0; i < weapons.Length; i++)
        {
           weapons[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (DialogueManager.isActive == true)
            return;


        ClassChecker();

  


        
            if (anim.GetBool("isMoving") == false)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }
        
        


        if (Input.GetMouseButtonDown(1))
        {



            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layerMask = ~LayerMask.GetMask("ignore");

            if (Physics.Raycast(ray, out hit ,Mathf.Infinity , layerMask))
            {

                if (hit.collider.CompareTag("Enemy"))
                {
                    target = hit.collider.gameObject;
                    inRange = Vector3.Distance(transform.position, target.transform.position) <= range;

                    if (!inRange)
                    {

                        if (NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, path))
                        {

                            currentCornerIndex = 0;
                            targetPosition = path.corners[currentCornerIndex];
                            isMoving = true;

                            //heroCombatScript.targetedEnemy = null;
                            anim.SetBool("isMoving", true);
                            walkingtoIt = true;
                        }

                    }
                    else
                    {
                        isItOnRange();
                    }

                }
                else
                {
                    // player clicked on a non-enemy object, stop attacking and running
                    anim.SetBool("isShooting", false);
                    anim.SetBool("isSlashing", false);
                    anim.SetBool("isCasting", false);
                    target = null;


                    agent.isStopped = false;

                   


                        NavMeshHit navHit;
                        if (NavMesh.SamplePosition(hit.point, out navHit, Mathf.Infinity, NavMesh.AllAreas))
                        {

                            if (NavMesh.CalculatePath(transform.position, navHit.position, NavMesh.AllAreas, path))
                            {
                                currentCornerIndex = 0;
                                targetPosition = path.corners[currentCornerIndex];
                                isMoving = true;

                                //heroCombatScript.targetedEnemy = null;
                                anim.SetBool("isMoving", true);

                            }
                        }
                    
                }
            }
            
        }

        if (target != null || walkingtoIt== true)
        {
            if (target != null)
            {
                inRange = Vector3.Distance(transform.position, target.transform.position) <= range;
            }
        }
        else
        {
            inRange = false;
        }



        if (!inRange)
        {

            anim.SetBool("isShooting", false);
            anim.SetBool("isSlashing", false);
            anim.SetBool("isCasting", false);
            Moving();

            if (walkingtoIt == false)
            {
                target = null;
            }

        }
        else
        {  
            isItOnRange(); 
          
        }



        


    }

    public void isItOnRange()
    {
        if (target != null)
        {
            // check if the target is still within range
            bool inTargetRange = Vector3.Distance(transform.position, target.transform.position) <= range;

            if (inTargetRange)
            {
                // rotate the GameObject towards the target enemy
                Vector3 direction = target.transform.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = targetRotation;

                walkingtoIt = false;

                // play the shooting animation

                if (classeEscolhida == Class.Gunner)
                {
                    anim.SetBool("isShooting", true);
                }
                else if (classeEscolhida == Class.Swordsman)
                {
                    anim.SetBool("isSlashing", true);

                }
                else if (classeEscolhida == Class.Mage)
                {
                    anim.SetBool("isCasting", true);

                }

                anim.SetBool("isMoving", false);

                isMoving = false;

            }
            else
            {
                // target is out of range, so move towards it
                anim.SetBool("isShooting", false);
                anim.SetBool("isSlashing", false);
                anim.SetBool("isCasting", false);

                if (walkingtoIt == false)
                {
                    target = null;
                }

            }
        }
        else if (inRange)
        {
            // play the idle animation if no target and player is within range
            anim.SetBool("isShooting", false);
            anim.SetBool("isSlashing", false);
            anim.SetBool("isCasting", false);

        }

    }


    public void Moving()
    {
        if (isMoving) // Check if the GameObject is currently moving
        {
            // Calculate the direction to move towards
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0f;
            direction.Normalize();

            // Move towards the current target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, speed);

            // Rotate towards the direction of movement
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.2f);
            }

            // Check if the GameObject has arrived at the current target position
            if (Vector3.Distance(transform.position, targetPosition) <= 0.45f)
            {
                anim.SetBool("isMoving", false); // Change the "IsMoving" parameter of the Animator to false

                if (currentCornerIndex < path.corners.Length - 1)
                {
                    // Set the next target position to move towards
                    currentCornerIndex++;
                    targetPosition = path.corners[currentCornerIndex];
                    isMoving = true;

                    anim.SetBool("isMoving", true);
                }
                else
                {
                    anim.SetBool("isMoving", false); // Change the "IsMoving" parameter of the Animator to true
                }
            }

        }
    }


    void ClassChecker()
    {
        if (classeEscolhida == Class.Gunner)
        {

            range = 7f;

            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i].name == "Pirate_Pistol")
                {
                    anim.SetBool("hasSword", false);

                    if(!weapons[i].activeInHierarchy)
                    {
                        weapons[i].SetActive(true);
                    }
                    weaponActiveNow = weapons[i];
                }
                else
                {
                    weapons[i].SetActive(false);
                }
            }

        }
        else if (classeEscolhida == Class.Swordsman)
        {
            range = 3f;


            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i].name == "swordpirate Variant")
                {
                    anim.SetBool("hasSword" , true);
                  
                    if (!weapons[i].activeInHierarchy)
                    {
                        weapons[i].SetActive(true);
                    }
                    weaponActiveNow = weapons[i];
                }
                else
                {
                    weapons[i].SetActive(false);
                }
            }

        }
        else if (classeEscolhida == Class.Mage)
        {
            range = 7f;


            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i].name == "Cetro")
                {
                    anim.SetBool("hasSword", false);
                    if (!weapons[i].activeInHierarchy)
                    {
                        weapons[i].SetActive(true);
                    }
                    weaponActiveNow = weapons[i];
                }
                else
                {
                    weapons[i].SetActive(false);
                }
            }
        }

        if (weaponActiveNow != null)
        {
            if (weaponActiveNow.name == "swordpirate Variant")
            {
                anim.SetBool("hasSword", true);
            }
        }


    }


    public void FireGun()
    {

        GameObject positionForShooting = weaponActiveNow.transform.GetChild(0).gameObject;


        Instantiate(effects[0].gameObject, positionForShooting.transform.position, effects[0].transform.rotation);
        GameObject bull = Instantiate(effects[1].gameObject, positionForShooting.transform.position, effects[1].transform.rotation * positionForShooting.transform.rotation);

        bull.GetComponent<BulletFollow>().target = target;


    }


    public void FireMagic()
    {
        Transform d = weaponActiveNow.transform.GetChild(26);
        GameObject positionForShooting = d.transform.GetChild(0).gameObject;
        GameObject bull = Instantiate(effects[2].gameObject, positionForShooting.transform.position, effects[2].transform.rotation * positionForShooting.transform.rotation);

        bull.GetComponent<MagicFollow>().target = target;


    }



    public void colliderOnAndOff()
    {
        if (weaponActiveNow.gameObject.GetComponent<MeshCollider>().enabled == true)
        {
            weaponActiveNow.gameObject.GetComponent<MeshCollider>().enabled = false;
        }
        else
        {
            weaponActiveNow.gameObject.GetComponent<MeshCollider>().enabled = true;
        }
    }

}
