using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FollowCameraRotation))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] bool isBillboarded = true;
    [SerializeField] bool shouldShowHealthNumbers = true;

    float finalValue;
    float animationSpeed = 0.1f;
    float leftoverAmount = 0f;

    // Caches
    HealthSystemForDummies healthSystem;
    Image image;
    Text text;
    FollowCameraRotation followCameraRotation;

    bool isQDown = false;
    float wait = 5f;
    private void Start()
    {
        //healthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystemForDummies>();
        healthSystem = transform.GetComponentInParent<HealthSystemForDummies>();
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();
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

        if (text != null)
        {
            text.text = $"{healthSystem.CurrentHealth}/{healthSystem.MaximumHealth}";
            text.enabled = shouldShowHealthNumbers;

        }


        followCameraRotation.enabled = isBillboarded;

       /* if (Input.GetKey(KeyCode.Q))
        {
         
            if (!isQDown && Abilities.isBlockActive != true)
            {
                healthSystem.AddToCurrentHealth(-100);
                //healthSystem.OnCurrentHealthChanged.AddListener(ChangeHealthFill);
            }
            isQDown = true;
        }

        if(isQDown)
        {
            wait -= Time.deltaTime;

            if (wait <= 0)
                isQDown = false;
        }*/
    }

    private void ChangeHealthFill(CurrentHealth currentHealth)
    {
        if (!healthSystem.HasAnimationWhenHealthChanges) return;

        StopAllCoroutines();
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
    }
}