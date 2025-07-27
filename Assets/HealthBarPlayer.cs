using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FollowCameraRotation))]
public class HealthBarPlayer : MonoBehaviour
{
    [SerializeField] bool isBillboarded = true;
    [SerializeField] bool shouldShowHealthNumbers = true;
    [SerializeField] TimeController tC;
    [SerializeField] Transform boat;
    [SerializeField] AudioSource getHit;
    [SerializeField] AudioSource die;
    [SerializeField] AudioSource wakeUp;

    float finalValue;
    float animationSpeed = 0.1f;
    float leftoverAmount = 0f;

    // Caches
    HealthSystemForDummies healthSystem;
    public Image image;
    public Text text;
    FollowCameraRotation followCameraRotation;

    public Canvas black, black2;
    float timerBlack = 3f;
    Transform player;
    PlayerRewards playerRewardScript;
    public TimeController timeController;

    public KeyCode rest;
    bool isBlackActive = false, dieAlreadyPlay = false;
    public static bool dontReceiveDmg = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerRewardScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRewards>();
        healthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystemForDummies>();
        followCameraRotation = GetComponent<FollowCameraRotation>();
        healthSystem.OnCurrentHealthChanged.AddListener(ChangeHealthFill);
    }

    void Update()
    {
        animationSpeed = healthSystem.AnimationDuration;

        if (!healthSystem.HasAnimationWhenHealthChanges)
        {
            image.fillAmount = healthSystem.CurrentHealthPercentage / 100;
        }

        text.text = $"{healthSystem.CurrentHealth}/{healthSystem.MaximumHealth}";

        text.enabled = shouldShowHealthNumbers;

        followCameraRotation.enabled = isBillboarded;

       if(!healthSystem.IsAlive && tC.daysToFinish > 0)
        {
            player.GetComponent<movement>().isMoving = false;

            
            


            black2.gameObject.SetActive(true);
            dontReceiveDmg = true;
            //player.position = new Vector3(-81f, 2.8f, 29.5f);
            player.position = new Vector3(-46.2f, 1.6f, -208.3f);
            boat.position = new Vector3(38.6f, 0.32f, -149.7f);


            timerBlack -= Time.deltaTime;

            if(timerBlack <= 0)
            {
                timeController.daysToFinish -= 1;
                timeController.dayText.text = "Dia " + timeController.daysToFinish.ToString();
                timeController.currentTime = DateTime.Now.Date + TimeSpan.FromHours(timeController.startHr);
                Quaternion newRotation = Quaternion.Euler(timeController.initialLightRotation);
                timeController.sunLight.transform.rotation = newRotation;
                timeController.moonLight.transform.rotation = newRotation;
                black2.gameObject.SetActive(false);
                healthSystem.ReviveWithMaximumHealth();
                timerBlack = 3f;
                dontReceiveDmg = false;
                playerRewardScript.playerCurrentGold /= 2;
                playerRewardScript.goldText.text = playerRewardScript.playerCurrentGold.ToString();
                dieAlreadyPlay = false;
            }
        }
       
       if(Input.GetKey(rest) && healthSystem.IsAlive && tC.daysToFinish > 0)
        {
            black.gameObject.SetActive(true);
            SwitchToogle.LoadSoundState();
            if (SwitchToogle.muted == false)
            {
                wakeUp.Play();
            }
            
            dontReceiveDmg = true;
            isBlackActive = true;

            foreach (AnimatorControllerParameter parameter in player.GetComponent<Animator>().parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Bool)
                    player.GetComponent<Animator>().SetBool(parameter.name, false);
            }

            player.GetComponent<Animator>().SetTrigger("isSleeping");

        }

       if(isBlackActive)
        {
            player.GetComponent<movement>().isMoving = false;
            timerBlack -= Time.deltaTime;

            if (timerBlack <= 0)
            {
                timeController.daysToFinish -= 1;
                timeController.dayText.text = "Dia " + timeController.daysToFinish.ToString();
                timeController.currentTime = DateTime.Now.Date + TimeSpan.FromHours(timeController.startHr);
                Quaternion newRotation = Quaternion.Euler(timeController.initialLightRotation);
                timeController.sunLight.transform.rotation = newRotation;
                timeController.moonLight.transform.rotation = newRotation;
                black.gameObject.SetActive(false);
                healthSystem.ReviveWithMaximumHealth();
                timerBlack = 3f;
                isBlackActive = false;
                dontReceiveDmg = false;
            }
        }


        if (player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Sleep") || player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Getting Up"))
        {
            player.GetComponent<movement>().isMoving = false;
            player.GetComponent<movement>().target = null;

        }
    }

    private void ChangeHealthFill(CurrentHealth currentHealth)
    {
        if (!healthSystem.HasAnimationWhenHealthChanges) return;

        StopAllCoroutines();
        if (image.fillAmount > 0 && !dontReceiveDmg)
        {
            SwitchToogle.LoadSoundState();
            if (SwitchToogle.muted == false)
            {
                getHit.Play();
            }
        }
            
        
        if (image.fillAmount <= 0 && !dieAlreadyPlay)
        {
            SwitchToogle.LoadSoundState();
            if (SwitchToogle.muted == false)
            {
                die.Play();
            }
            
            dieAlreadyPlay = true;
        }


        StartCoroutine(ChangeFillAmount(currentHealth));
    }

    private IEnumerator ChangeFillAmount(CurrentHealth currentHealth)
    {
        finalValue = currentHealth.percentage / 100;

        float cacheLeftoverAmount = this.leftoverAmount;

        float timeElapsed = 0;

        while (timeElapsed < animationSpeed)
        {
            float leftoverAmount = Mathf.Lerp((currentHealth.previous / healthSystem.MaximumHealth) + cacheLeftoverAmount, finalValue, timeElapsed / animationSpeed);
            this.leftoverAmount = leftoverAmount - finalValue;
            image.fillAmount = leftoverAmount;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        this.leftoverAmount = 0;
        image.fillAmount = finalValue;

        if(image.fillAmount == 0)
        {
            
            healthSystem.Kill();
        }

    }
}