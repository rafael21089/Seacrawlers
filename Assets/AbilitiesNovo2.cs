using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AbilitiesNovo2 : MonoBehaviour
{
    public Animator anim;
    public Camera camera;
    public float tempoDeExpansao = 10f;
    public float danoPorSegundo = 5f;
    private bool _ativada = false;

    [Header("Ability 1")]
    public Image abilityImage1;
    public Image abilityImage2;
    public Image abilityImage3;


    public Image BuffabilityImage1;
    public Image BuffabilityImage2;
    public Image BuffabilityImage3;

    public static float generalCooldown = 2;
    public float reducedCooldown = 2.5f;

    public float cooldown1 = generalCooldown, cooldownImg = generalCooldown;
    bool isCooldown = false;

    public KeyCode ability1;
    public KeyCode ability2;
    public KeyCode ability3;

    public KeyCode Buffability1;
    public KeyCode Buffability2;
    public KeyCode Buffability3;

    GameObject chamas;
    Transform firePointCanon;
    Transform firePointMiniCanon1;
    Transform firePointMiniCanon2;

    public Transform firePointPlayer1;
    public Transform firePointPlayer2;
    public Transform firePoint;
    public GameObject player;
    public GameObject bulletPrefab;
    public GameObject bulletPrefabDoubleShoot;
    public GameObject bulletCanon;
    public GameObject bulletBigShoot;
    public GameObject bulletDonuts;
    public GameObject canon;


    public GameObject meteorGunnerSkill;

    public float bulletForce = 20f;
    float originalDmg, originalSpeed;

    public float bulletSpeed = 1f;

    bool firstShootDone = false, firstRotateDone = false;
    float waitTimeNextShoot = 0.5f, waitTime = 0.5f;

    bool isCanonActive = false;
    float canonActiveTime = 1, rotateActiveTime = 5;

    bool miniCanonsActive = false;
    float miniCanonsActiveTime = 5;
    public float velocidadeRotacao = 30f;
    bool firstShootDoneMiniCanon = false;
    bool abilityRotateAndShootActive = false;
    float rotateTime = 0.2f;

    GameObject canhao1, canhao2;
    GameObject bubbleE, bubbleE2, bubbleTesteCirc;
    public GameObject moloEffect;

    public GameObject purpleCirclePrefab;
    public GameObject purpleCirclePrefab2;
    public GameObject purpleBubble;

    float spawnDistance = 5f;
    public static bool isReadyToExplode = false, bubbleSpawned = false;

    bool flameTActive = false, rotateRight = true, rotateLeft = false;

    GameObject flame;
    public GameObject flamePrefab;

    public GameObject cagePrefab;
    GameObject cage;

    GameObject miniFlames;

    public GameObject CircleOfFireSPrefab;
    GameObject CircleOfFireS;

    public GameObject FlameWallPrefab;
    GameObject flameWalls, flameWalls2, flameWalls3, flameWalls4;

    bool finishRotating = false;
    bool isFirst = true;
    bool MageDoubleShoot = false;
    float MageDoubleShootTime = 2f;

    bool backToNormal = false;
    int clicked = -1;
    int clickedBuff = -1;
    public bool isBlockActive = false;
    bool isReducedCooldownActive = false, isReducedFirstTime = false;

    public float attackDmg = 100;


    Ray ray;
    void Start()
    {
        anim = player.GetComponent<Animator>();
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;

        BuffabilityImage1.fillAmount = 0;
        BuffabilityImage2.fillAmount = 0;
        BuffabilityImage3.fillAmount = 0;
        originalDmg = player.GetComponent<Attack>().attackDmg;
        originalSpeed = player.GetComponent<NavMeshAgent>().speed;
    }

    public void CheckAbilityUsed()
    {
       
            if (Ability1Display.equipedAbilities[clicked].functionName == "DoubleShoot")
            {
                DoubleShoot();
            }
            else if (Ability1Display.equipedAbilities[clicked].functionName == "ActivateCanon")
            {

                ActivateCanon();
            }
            else if (Ability1Display.equipedAbilities[clicked].functionName == "BigShoot")
            {
                BigShoot();
            }
            else if (Ability1Display.equipedAbilities[clicked].functionName == "JumpOver")
            {
                JumpOver();
            }
            else if (Ability1Display.equipedAbilities[clicked].functionName == "Flamethrower")
            {
                Flamethrower();
            }
            else if (Ability1Display.equipedAbilities[clicked].functionName == "Cage")
            {
                Cage();
            }
            else if (Ability1Display.equipedAbilities[clicked].functionName == "MiniFlames")
            {
                MiniFlames();
            }
            else if (Ability1Display.equipedAbilities[clicked].functionName == "CircleOfFire")
            {
                CircleOfFire();
            }
            else if (Ability1Display.equipedAbilities[clicked].functionName == "FlameWall")
            {
                FlameWall();
            }
            else if (Ability1Display.equipedAbilities[clicked].functionName == "ExplosiveShoot")
            {
                ExplosiveShoot();
            }

        

       
        



        isCooldown = true;


    }

    void CheckBuffUsed()
    {

        if (Ability1Display.equipedAbilitiesBuff[clickedBuff].functionName == "AbilityMoreDmg")
        {
            AbilityMoreDmg();
        }
        else if (Ability1Display.equipedAbilitiesBuff[clickedBuff].functionName == "AbilityMoreSpeed")
        {
            AbilityMoreSpeed();
        }
        else if (Ability1Display.equipedAbilitiesBuff[clickedBuff].functionName == "AbilityBlockDmg")
        {
            AbilityBlockDmg();
        }
        else if (Ability1Display.equipedAbilitiesBuff[clickedBuff].functionName == "AbilityReduceCooldown")
        {
            AbilityReduceCooldown();
        }

        isCooldown = true;

    }


    void Update()
    {

        isAnimationPlaying();

        if (Input.GetKey(ability1) && abilityImage1.fillAmount == 0)
        {
           

            if (player.GetComponent<movement>().classeEscolhida == movement.Class.Gunner)
            {

                clicked = 0;

                if (Ability1Display.equipedAbilities[clicked] != null)
                {
                    abilityImage1.fillAmount = Ability1Display.equipedAbilities[clicked].abilityCooldown;

                    if (Ability1Display.equipedAbilities[clicked].functionName == "DoubleShoot" || Ability1Display.equipedAbilities[clicked].functionName == "BigShoot")
                    {
                        anim.SetBool("isUsingAbilityGunner", true);
                        anim.SetBool("isMoving", false);

                        ray = camera.ScreenPointToRay(Input.mousePosition);

                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            Vector3 direction = hit.point - transform.position;
                            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
                        }

                        this.gameObject.GetComponent<movement>().isMoving = false;
                    }
                    else if (Ability1Display.equipedAbilities[clicked].functionName == "ExplosiveShoot")
                    {
                        ray = camera.ScreenPointToRay(Input.mousePosition);

                        anim.SetBool("MeteorSkill", true);

                    }
                    else
                    {
                        CheckAbilityUsed();
                    }
                }

            }


            // Mage

            if (player.GetComponent<movement>().classeEscolhida == movement.Class.Mage)
            {

                clicked = 0;

                if (Ability1Display.equipedAbilities[clicked] != null)
                {
                    abilityImage1.fillAmount = Ability1Display.equipedAbilities[clicked].abilityCooldown;

                    if (Ability1Display.equipedAbilities[clicked].functionName == "MiniFlames" || Ability1Display.equipedAbilities[clicked].functionName == "CircleOfFire" || Ability1Display.equipedAbilities[clicked].functionName == "FlameWall")
                    {
                        ray = camera.ScreenPointToRay(Input.mousePosition);

                        anim.SetBool("CastSkillMageRare", true);

                    }
                    else if (Ability1Display.equipedAbilities[clicked].functionName == "Flamethrower")
                    {
                        ray = camera.ScreenPointToRay(Input.mousePosition);

                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            Vector3 direction = hit.point - transform.position;
                            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
                        }
                        this.gameObject.GetComponent<movement>().isMoving = false;
                        
                        CheckAbilityUsed();

                        anim.SetBool("CastSkillFlametrower", true);
                        anim.SetBool("isMoving", false);

                    }
                    else
                    {
                        CheckAbilityUsed();
                    }
                }

            }


        }
        else if (Input.GetKey(ability2) && abilityImage2.fillAmount == 0)
        {

            if (player.GetComponent<movement>().classeEscolhida == movement.Class.Gunner)
            {

                clicked = 1;

                if (Ability1Display.equipedAbilities[clicked] != null)
                {
                    abilityImage2.fillAmount = Ability1Display.equipedAbilities[clicked].abilityCooldown;

                    if (Ability1Display.equipedAbilities[clicked].functionName == "DoubleShoot" || Ability1Display.equipedAbilities[clicked].functionName == "BigShoot")
                    {
                        anim.SetBool("isUsingAbilityGunner", true);
                        ray = camera.ScreenPointToRay(Input.mousePosition);
                        anim.SetBool("isMoving", false);

                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            Vector3 direction = hit.point - transform.position;
                            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
                        }

                        this.gameObject.GetComponent<movement>().isMoving = false;
                    }
                    else if (Ability1Display.equipedAbilities[clicked].functionName == "ExplosiveShoot")
                    {
                        ray = camera.ScreenPointToRay(Input.mousePosition);
                        anim.SetBool("MeteorSkill", true);

                    }
                    else
                    {
                        CheckAbilityUsed();
                    }
                }

                

            }


            // Mage

            if (player.GetComponent<movement>().classeEscolhida == movement.Class.Mage)
            {

                clicked = 1;

                if (Ability1Display.equipedAbilities[clicked] != null)
                {
                    abilityImage2.fillAmount = Ability1Display.equipedAbilities[clicked].abilityCooldown;

                    if (Ability1Display.equipedAbilities[clicked].functionName == "MiniFlames" || Ability1Display.equipedAbilities[clicked].functionName == "CircleOfFire" || Ability1Display.equipedAbilities[clicked].functionName == "FlameWall")
                    {
                        ray = camera.ScreenPointToRay(Input.mousePosition);

                        anim.SetBool("CastSkillMageRare", true);

                    }
                    else if (Ability1Display.equipedAbilities[clicked].functionName == "Flamethrower")
                    {
                        ray = camera.ScreenPointToRay(Input.mousePosition);

                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            Vector3 direction = hit.point - transform.position;
                            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
                        }
                        this.gameObject.GetComponent<movement>().isMoving = false;

                        CheckAbilityUsed();

                        anim.SetBool("CastSkillFlametrower", true);
                        anim.SetBool("isMoving", false);

                    }
                    else
                    {
                        CheckAbilityUsed();
                    }
                }

            }


        }
        else if (Input.GetKey(ability3) && abilityImage3.fillAmount == 0)
        {
            
            if (player.GetComponent<movement>().classeEscolhida == movement.Class.Gunner)
            {

                clicked = 2;

                if (Ability1Display.equipedAbilities[clicked] != null)
                {
                    abilityImage3.fillAmount = Ability1Display.equipedAbilities[clicked].abilityCooldown;

                    if (Ability1Display.equipedAbilities[clicked].functionName == "DoubleShoot" || Ability1Display.equipedAbilities[clicked].functionName == "BigShoot")
                    {
                        anim.SetBool("isUsingAbilityGunner", true);
                        ray = camera.ScreenPointToRay(Input.mousePosition);
                        anim.SetBool("isMoving", false);

                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            Vector3 direction = hit.point - transform.position;
                            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
                        }

                        this.gameObject.GetComponent<movement>().isMoving = false;
                    }
                    else if (Ability1Display.equipedAbilities[clicked].functionName == "ExplosiveShoot")
                    {
                        ray = camera.ScreenPointToRay(Input.mousePosition);
                        anim.SetBool("MeteorSkill", true);

                    }
                    else
                    {
                        CheckAbilityUsed();
                    }
                }

                    
                

            }

            // Mage

            if (player.GetComponent<movement>().classeEscolhida == movement.Class.Mage)
            {

                clicked = 2;

                if (Ability1Display.equipedAbilities[clicked] != null)
                {
                    abilityImage3.fillAmount = Ability1Display.equipedAbilities[clicked].abilityCooldown;

                    if (Ability1Display.equipedAbilities[clicked].functionName == "MiniFlames" || Ability1Display.equipedAbilities[clicked].functionName == "CircleOfFire" || Ability1Display.equipedAbilities[clicked].functionName == "FlameWall")
                    {
                        ray = camera.ScreenPointToRay(Input.mousePosition);

                        anim.SetBool("CastSkillMageRare", true);

                    }else if (Ability1Display.equipedAbilities[clicked].functionName == "Flamethrower")
                    {
                        ray = camera.ScreenPointToRay(Input.mousePosition);

                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            Vector3 direction = hit.point - transform.position;
                            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
                        }

                        this.gameObject.GetComponent<movement>().isMoving = false;

                        CheckAbilityUsed();

                        anim.SetBool("CastSkillFlametrower", true);
                        anim.SetBool("isMoving", false);

                    }
                    else
                    {
                        CheckAbilityUsed();
                    }
                }

            }

        }


        if (Input.GetKey(Buffability1) && BuffabilityImage1.fillAmount == 0)
        {
            clickedBuff = 0;
            if (Ability1Display.equipedAbilitiesBuff[clickedBuff] != null)
            {
                BuffabilityImage1.fillAmount = Ability1Display.equipedAbilitiesBuff[clickedBuff].abilityCooldown;

                CheckBuffUsed();
            }

        }
        else if (Input.GetKey(Buffability2) && BuffabilityImage2.fillAmount == 0)
        {
            clickedBuff = 1;

            if (Ability1Display.equipedAbilitiesBuff[clickedBuff] != null)
            {
                BuffabilityImage2.fillAmount = Ability1Display.equipedAbilitiesBuff[clickedBuff].abilityCooldown;

                CheckBuffUsed();

            }
        }
        else if (Input.GetKey(Buffability3) && BuffabilityImage3.fillAmount == 0)
        {
            clickedBuff = 2;

            if (Ability1Display.equipedAbilitiesBuff[clickedBuff] != null)
            {
                BuffabilityImage3.fillAmount = Ability1Display.equipedAbilitiesBuff[clickedBuff].abilityCooldown;

                CheckBuffUsed();
            }
        }


        RefreshCooldown();

    }


    void isAnimationPlaying()
    {

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("AbilityGunner"))
        {
            player.GetComponent<NavMeshAgent>().isStopped = true;
            player.GetComponent<movement>().isMoving = false;
            player.GetComponent<movement>().targetPosition = new Vector3();
            player.GetComponent<movement>().enabled = false;

            anim.SetBool("isShooting", false);
            //anim.SetBool("isMoving", false);


        }
        else if (anim.GetCurrentAnimatorStateInfo(1).IsName("MeteorGun"))
        {
            anim.SetBool("MeteorSkill", false);
        }
        else if (anim.GetCurrentAnimatorStateInfo(1).IsName("Cast"))
        {
            anim.SetBool("CastSkillMageRare", false);
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("AbilityMage"))
        {
            anim.SetBool("CastSkillFlametrower", false);

            player.GetComponent<NavMeshAgent>().isStopped = true;
            player.GetComponent<movement>().enabled = false;

            anim.SetBool("isMoving", false);
            anim.SetBool("isShooting", false);
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            anim.SetBool("isJumping", false);

        }
        else
        {
            player.GetComponent<movement>().enabled = true;
        }

    }

    void UnableToUse()
    {
        cooldown1 -= Time.deltaTime;

        if (cooldown1 <= 0)
        {
            player.GetComponent<Attack>().attackDmg = originalDmg;
            player.GetComponent<NavMeshAgent>().speed = originalSpeed;
            isBlockActive = false;
            isReducedCooldownActive = false;
            attackDmg = 100;
            canonActiveTime = 1;

            transform.GetChild(37).gameObject.SetActive(false);
            transform.GetChild(38).gameObject.SetActive(false);
            transform.GetChild(39).gameObject.SetActive(false);
            player.GetComponent<movement>().speed = 8f;


            if (player.GetComponent<movement>().enabled == false)
                player.GetComponent<movement>().enabled = true;

            if (cage != null)
                Destroy(cage);

            

        }
        RefreshCooldown();
        
    }

    void Refresh()
    {
        if (abilityImage1.fillAmount <= 0)
        {
            abilityImage1.fillAmount = 0;
            isCooldown = false;

        }
        else if (abilityImage2.fillAmount <= 0)
        {
            abilityImage2.fillAmount = 0;
            isCooldown = false;
        }
        else if (abilityImage3.fillAmount <= 0)
        {
            abilityImage3.fillAmount = 0;
            isCooldown = false;
        }

    }

    void RefreshCooldown()
    {
        if (Ability1Display.equipedAbilities[0] != null)
        {
            if (abilityImage1.fillAmount <= 1)
            {
                abilityImage1.fillAmount -= 1 / Ability1Display.equipedAbilities[0].abilityCooldown * Time.deltaTime;

                if (abilityImage1.fillAmount <= 0)
                {
                    abilityImage1.fillAmount = 0;
                    isCooldown = false;
                }
            }
        }
        if (Ability1Display.equipedAbilities[1] != null)
        {
            if (abilityImage2.fillAmount <= 1)
            {
                abilityImage2.fillAmount -= 1 / Ability1Display.equipedAbilities[1].abilityCooldown * Time.deltaTime;

                if (abilityImage2.fillAmount <= 0)
                {
                    abilityImage2.fillAmount = 0;
                    isCooldown = false;
                }
            }
        }
        if (Ability1Display.equipedAbilities[2] != null)
        {

            if (abilityImage3.fillAmount <= 1)
            {
                abilityImage3.fillAmount -= 1 / Ability1Display.equipedAbilities[2].abilityCooldown * Time.deltaTime;

                if (abilityImage3.fillAmount <= 0)
                {
                    abilityImage3.fillAmount = 0;
                    isCooldown = false;
                }
            }
        }


        //Buffs

        if (Ability1Display.equipedAbilitiesBuff[0] != null)
        {
            if (BuffabilityImage1.fillAmount <= 1)
            {
                BuffabilityImage1.fillAmount -= 1 / Ability1Display.equipedAbilitiesBuff[0].abilityCooldown * Time.deltaTime;

                if (BuffabilityImage1.fillAmount <= 0)
                {
                    BuffabilityImage1.fillAmount = 0;
                    isCooldown = false;
                }
            }
        }

        if (Ability1Display.equipedAbilitiesBuff[1] != null)
        {
            if (BuffabilityImage2.fillAmount <= 1)
            {
                BuffabilityImage2.fillAmount -= 1 / Ability1Display.equipedAbilitiesBuff[1].abilityCooldown * Time.deltaTime;

                if (BuffabilityImage2.fillAmount <= 0)
                {
                    BuffabilityImage2.fillAmount = 0;
                    isCooldown = false;
                }
            }
        }

        if (Ability1Display.equipedAbilitiesBuff[2] != null)
        {
            if (BuffabilityImage3.fillAmount <= 1)
            {
                BuffabilityImage3.fillAmount -= 1 / Ability1Display.equipedAbilitiesBuff[2].abilityCooldown * Time.deltaTime;

                if (BuffabilityImage3.fillAmount <= 0)
                {
                    BuffabilityImage3.fillAmount = 0;
                    isCooldown = false;
                }
            }
        }

        
       
        

        if (clicked == -1)
        {
            cooldown1 = generalCooldown;
        }
    }

    void ExplosiveShoot()
    {

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 groundPosition = hit.point;
            groundPosition.y = transform.position.y + 3;
            GameObject g = Instantiate(meteorGunnerSkill, groundPosition, Quaternion.identity);
        }

    }

    public void AbilityMoreDmg()
    {
        transform.GetChild(37).gameObject.SetActive(true);

        transform.GetChild(37).GetComponent<ParticleSystem>().Play();
        attackDmg *= 2f;
    }

    void AbilityMoreSpeed()
    {
        transform.GetChild(38).gameObject.SetActive(true);

        transform.GetChild(38).GetComponent<ParticleSystem>().Play();
        player.GetComponent<movement>().speed *= 1.5f;
    }

    void AbilityBlockDmg()
    {
        transform.GetChild(39).gameObject.SetActive(true);

        transform.GetChild(39).GetComponent<ParticleSystem>().Play();

        isBlockActive = true;
    }

    void AbilityReduceCooldown()
    {
        isReducedCooldownActive = true;
        isReducedFirstTime = true;

        if (isReducedCooldownActive && isReducedFirstTime)
        {
            cooldown1 = reducedCooldown;
            isReducedFirstTime = false;
        }
    }

    void DoubleShoot()
    {
        anim.SetBool("isUsingAbilityGunner", false);
        StartCoroutine(DelayedDoubleShoot());
        
    }

    IEnumerator DelayedDoubleShoot( )
    {
        // Obter a direção atual do jogador

        GameObject rb = Instantiate(bulletPrefabDoubleShoot, firePoint.position, firePoint.rotation * bulletPrefabDoubleShoot.transform.rotation);

        // Esperar por 0.5 segundos
        yield return new WaitForSeconds(0.2f);

        GameObject rb2 = Instantiate(bulletPrefabDoubleShoot, firePoint.position, firePoint.rotation * bulletPrefabDoubleShoot.transform.rotation);

    }

    void ActivateCanon()
    {
        Vector3 posicaoCanhao = new Vector3(player.transform.position.x + 2f, player.transform.position.y, player.transform.position.z);
        Vector3 euler = player.transform.rotation.eulerAngles;
        Quaternion direcao = Quaternion.Euler(euler.x, euler.y - 90f, euler.z);
        GameObject canhao = Instantiate(canon, posicaoCanhao , canon.transform.rotation);

    }

    void BigShoot()
    {
        // Obter a direção atual do jogador
        //Vector3 direction = player.GetComponent<movement>().weaponActiveNow.transform.GetChild(0).transform.forward;//player.transform.forward;
      
        // Instanciar a bala na posição da arma com a rotação atual do jogador
        GameObject g = Instantiate(bulletBigShoot, firePoint.position, /*rotation*/firePoint.rotation * bulletBigShoot.transform.rotation);

        //Rigidbody rb = g.GetComponent<Rigidbody>();
        //// Adicionar uma força para impulsionar a bala na direção do jogador
        //rb.AddForce(direction * 7f, ForceMode.Impulse);

        anim.SetBool("isUsingAbilityGunner", false);
    }

    void JumpOver()
    {
        player.GetComponentInChildren<Animator>().SetBool("isJumping", true);
    }

    /*IEnumerator FlamePlayer()
    {
        // Obter a direção atual do jogador
        Vector3 direction = player.transform.forward;

        // Instanciar a bala na posição do firePoint com a rotação atual do jogador
        Rigidbody rb = Instantiate(bulletBigShoot, firePoint.position, Quaternion.LookRotation(direction)).GetComponent<Rigidbody>();

        // Adicionar uma força para impulsionar a bala na direção do jogador
        rb.AddForce(direction * 7f, ForceMode.Impulse);
    }*/
    void Flamethrower()
    {
        Vector3 direction = player.transform.forward;
        Vector3 positionForShooting = player.GetComponent<movement>().weaponActiveNow.transform.GetChild(0).position;
        flame = Instantiate(flamePrefab, firePoint.position, Quaternion.LookRotation(direction));
        player.GetComponent<movement>().enabled = false;

    }
    
    void Cage()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 groundPosition = hit.point;
            groundPosition.y = 3f;
            cage = Instantiate(cagePrefab, groundPosition, Quaternion.identity);
            cage.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        }
    }

    void MiniFlames()
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 groundPosition = hit.point;
            groundPosition.y = transform.position.y;
            miniFlames = Instantiate(purpleCirclePrefab, groundPosition, Quaternion.identity);
            miniFlames.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
    }

    void CircleOfFire()
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 groundPosition = hit.point;
            groundPosition.y = transform.position.y;
            CircleOfFireS = Instantiate(CircleOfFireSPrefab, groundPosition, Quaternion.identity);
        }
        
    }

    void FlameWall()
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 groundPosition = hit.point;
            groundPosition.y = transform.position.y;
            flameWalls = Instantiate(FlameWallPrefab, groundPosition, Quaternion.Euler(0f, 90f, 0f));
            flameWalls2 = Instantiate(FlameWallPrefab, groundPosition, Quaternion.Euler(0f, 00f, 0f));
            flameWalls3 = Instantiate(FlameWallPrefab, groundPosition, Quaternion.Euler(0f, -90f, 0f));
            flameWalls4 = Instantiate(FlameWallPrefab, groundPosition, Quaternion.Euler(0f, 180f, 0f));
        }

    }


    IEnumerator RotatePlayerRight()
    {
        float rotationTime = 1f; // tempo de duração da rotação (em segundos)
        float elapsedTime = 0f;
        Quaternion startRotation = player.transform.rotation;
        Quaternion targetRotation;

        // Gira 360 graus para a direita
        targetRotation = Quaternion.Euler(0f, startRotation.eulerAngles.y + 180f, 0f);
        while (elapsedTime < rotationTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / rotationTime;
            player.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            flame.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z);
            flame.transform.rotation = player.transform.rotation;
            yield return null;
        }
    }

    IEnumerator RotatePlayerExtraRight()
    {
        float rotationTime = 1f; // tempo de duração da rotação (em segundos)
        float elapsedTime = 0f;
        Quaternion startRotation = player.transform.rotation;
        Quaternion targetRotation;

        // Gira mais 90 graus para a direita a partir da rotação final do RotatePlayerRight
        targetRotation = Quaternion.Euler(0f, startRotation.eulerAngles.y + 90f, 0f);
        while (elapsedTime < rotationTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / rotationTime;
            player.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            flame.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z);
            flame.transform.rotation = player.transform.rotation;
            yield return null;
        }
    }

    IEnumerator RotatePlayerExtraRight2()
    {
        float rotationTime = 1f; // tempo de duração da rotação (em segundos)
        float elapsedTime = 0f;
        Quaternion startRotation = player.transform.rotation;
        Quaternion targetRotation;

        // Gira mais 90 graus para a direita a partir da rotação final do RotatePlayerRight
        targetRotation = Quaternion.Euler(0f, startRotation.eulerAngles.y + 90f, 0f);
        while (elapsedTime < rotationTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / rotationTime;
            player.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            flame.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z);
            flame.transform.rotation = player.transform.rotation;
            yield return null;
        }
    }

    IEnumerator RotatePlayerSequence()
    {
        yield return StartCoroutine(RotatePlayerRight());
        yield return StartCoroutine(RotatePlayerExtraRight());
        yield return StartCoroutine(RotatePlayerExtraRight2());

        rotateRight = false;
        rotateLeft = true;
        // outras ações aqui
    }

    IEnumerator RotatePlayerLeft()
    {
        float rotationTime = 1f; // tempo de duração da rotação (em segundos)
        float elapsedTime = 0f;
        Quaternion startRotation = player.transform.rotation;
        Quaternion targetRotation;

        // Gira 90 graus para a esquerda
        targetRotation = Quaternion.Euler(0f, startRotation.eulerAngles.y - 90f, 0f);
        while (elapsedTime < rotationTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / rotationTime;
            player.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            flame.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z);
            flame.transform.rotation = player.transform.rotation;
            yield return null;
        }
    }


    IEnumerator RotatePlayer(bool rotateRight, float velocidadeRotacao)
    {
        float rotationTime = 1f; // tempo de duração da rotação (em segundos)
        float elapsedTime = 0f;
        Quaternion startRotation = player.transform.rotation;
        Quaternion targetRotation;
        float targetRotationY;

        // Gira 360 graus em uma direção
        targetRotationY = player.transform.rotation.eulerAngles.y + 360f;
        if (!rotateRight) targetRotationY -= 720f;
        targetRotation = Quaternion.Euler(0f, targetRotationY, 0f);
        while (elapsedTime < rotationTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / rotationTime;
            player.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            flame.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z);
            flame.transform.rotation = player.transform.rotation;
            yield return null;
        }

        // Espera um segundo
        yield return new WaitForSeconds(1f);

        // Gira 360 graus na direção oposta
        elapsedTime = 0f;
        startRotation = player.transform.rotation;
        targetRotationY = player.transform.rotation.eulerAngles.y - 360f;
        if (!rotateRight) targetRotationY += 720f;
        targetRotation = Quaternion.Euler(0f, targetRotationY, 0f);
        while (elapsedTime < rotationTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / rotationTime;
            player.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            flame.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z);
            flame.transform.rotation = player.transform.rotation;
            yield return null;
        }
    }
}
