using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealthController : MonoBehaviour
{

    private HealthSystemForDummies playerHealth;


    //Desert
    private bool isCoolingDownMarksman = false;
    private bool isCoolingDownSpider = false;
    private bool isCoolingDownWorm = false;


    //Florest

    private bool isCoolingDownRose = false;
    private bool isCoolingDownMantis = false;
    private bool isCoolingDownPigMan = false;

    //Ice

    private bool isCoolingDownIceBird = false;
    private bool isCoolingDownIceMonster = false;
    private bool isCoolingDownIceSpider = false;

    //Lava

    private bool isCoolingDownLizard = false;
    private bool isCoolingDownZombie = false;
    private bool isCoolingDownCyclops = false;

    private bool isCoolingBoss = false;




    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<HealthSystemForDummies>();

    }



    void OnParticleCollision(GameObject other)
    {
        if (playerHealth.GetComponent<AbilitiesNovo2>().isBlockActive != true && HealthBarPlayer.dontReceiveDmg == false)
        {

            if (other.name == "FireShot Variant" && isCoolingDownMarksman == false)
            {

                other.GetComponent<ParticleSystem>().GetComponent<ParticleSystem>().Clear();
                other.GetComponent<ParticleSystem>().GetComponent<ParticleSystem>().Stop();


                transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-100);


                isCoolingDownMarksman = true;
                StartCoroutine(CoolDownMarksman());

            }


            if (other.name == "RockSpike" && isCoolingDownIceMonster == false)
            {

               
                transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-200);


                isCoolingDownIceMonster = true;
                StartCoroutine(CoolDownIceMonster());

            }


            if (other.name == "IceLance" && isCoolingDownIceSpider == false)
            {

                other.GetComponent<ParticleSystem>().GetComponent<ParticleSystem>().Clear();
                other.GetComponent<ParticleSystem>().GetComponent<ParticleSystem>().Stop();


                transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-100);


                isCoolingDownIceSpider = true;
                StartCoroutine(CoolDownIceSpider());

            }
        }

    }


    void OnTriggerEnter(Collider other)
    {

        if (playerHealth.GetComponent<AbilitiesNovo2>().isBlockActive != true && HealthBarPlayer.dontReceiveDmg == false)
        {

            if (other.name == "SpiderCol" && isCoolingDownSpider == false)
            {

                transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-100);

                isCoolingDownSpider = true;
                StartCoroutine(CoolDownSpider());
            }

            if (other.name == "RockSpike Variant(Clone)" && isCoolingDownWorm == false)
            {

                transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-100);

                isCoolingDownWorm = true;
                StartCoroutine(CoolDownWorm());
            }


            if (other.name == "Sphere(Clone)" && isCoolingDownRose == false)
            {

                transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-15);

                isCoolingDownRose = true;
                StartCoroutine(CoolDownRose());
            }


            if (other.name == "MantisCol" && isCoolingDownMantis == false)
            {

                transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-100);

                isCoolingDownMantis = true;
                StartCoroutine(CoolDownMantis());
            }


            if (other.name == "pigmanAxe" && isCoolingDownPigMan == false)
            {

                transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-100);

                isCoolingDownPigMan = true;
                StartCoroutine(CoolDownPigman());
            }


            if (other.name == "pigmanAxeSkill" && isCoolingDownPigMan == false)
            {

                transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-200);

                isCoolingDownPigMan = true;
                StartCoroutine(CoolDownPigman());
            }


            if (other.name == "iceBird" && isCoolingDownIceBird == false)
            {

                transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-100);

                isCoolingDownIceBird = true;
                StartCoroutine(CoolDownIceBird());
            }

            if (other.name == "IcemonsterSlash" && isCoolingDownIceMonster == false)
            {

                transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-100);

                isCoolingDownIceMonster = true;
                StartCoroutine(CoolDownIceMonster());
            }


            if (other.name == "LizardSpear" && isCoolingDownLizard == false)
            {

                transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-100);

                isCoolingDownLizard = true;
                StartCoroutine(CoolDownLizard());
            }

            if (other.name == "zombieSmash" && isCoolingDownZombie == false)
            {

                transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-100);

                isCoolingDownZombie = true;
                StartCoroutine(CoolDownZombie());
            }

            if (other.name == "cyclopsSlap" && isCoolingDownCyclops == false)
            {

                transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-100);

                isCoolingDownCyclops = true;
                StartCoroutine(CoolDownCyclops());
            }

            if (other.name == "cyclopsExplosion" && isCoolingDownCyclops == false)
            {

                transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-200);

                isCoolingDownCyclops = true;
                StartCoroutine(CoolDownCyclops());
            }


            if (other.name == "bosspiratesword" && isCoolingBoss == false)
            {
                if (other.gameObject.GetComponent<bosscollidersword>().cont.damagebuff == true)
                {
                    transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-200);
                }
                else
                {
                    transform.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-100);
                }


                isCoolingBoss = true;
                StartCoroutine(CoolDownBoss());
            }

        }
    }



    IEnumerator CoolDownMarksman()
    {
        yield return new WaitForSeconds(0.5f);
        isCoolingDownMarksman = false;
    }

    IEnumerator CoolDownSpider()
    {
        yield return new WaitForSeconds(0.5f);
        isCoolingDownSpider = false;
    }

    IEnumerator CoolDownWorm()
    {
        yield return new WaitForSeconds(0.5f);
        isCoolingDownWorm = false;
    }

    IEnumerator CoolDownRose()
    {
        yield return new WaitForSeconds(0.5f);
        isCoolingDownRose = false;
    }

    IEnumerator CoolDownMantis()
    {
        yield return new WaitForSeconds(0.5f);
        isCoolingDownMantis = false;
    }

    IEnumerator CoolDownPigman()
    {
        yield return new WaitForSeconds(0.5f);
        isCoolingDownPigMan = false;
    }

    IEnumerator CoolDownIceBird()
    {
        yield return new WaitForSeconds(0.5f);
        isCoolingDownIceBird = false;
    }

    IEnumerator CoolDownIceMonster()
    {
        yield return new WaitForSeconds(0.5f);
        isCoolingDownIceMonster = false;
    }

    IEnumerator CoolDownIceSpider()
    {
        yield return new WaitForSeconds(0.5f);
        isCoolingDownIceSpider = false;
    }

    IEnumerator CoolDownLizard()
    {
        yield return new WaitForSeconds(0.7f);
        isCoolingDownLizard = false;
    }

    IEnumerator CoolDownZombie()
    {
        yield return new WaitForSeconds(0.7f);
        isCoolingDownZombie = false;
    }

    IEnumerator CoolDownCyclops()
    {
        yield return new WaitForSeconds(0.5f);
        isCoolingDownCyclops = false;
    }


    IEnumerator CoolDownBoss()
    {
        yield return new WaitForSeconds(0.5f);
        isCoolingBoss = false;
    }
}
