using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.AI;

public class TimeController : MonoBehaviour
{
    public Text dayText;
    public TextMeshProUGUI timeText;
    public int daysToFinish = 30;
    public DateTime currentTime;
    [SerializeField] float timeMultiplier;
    public float startHr;
    
    [SerializeField] float sunRiseHour;
    [SerializeField] float sunsetHour;
    TimeSpan sunriseTime;
    TimeSpan sunsetTime;
    [SerializeField] Color dayAmbientLight;
    [SerializeField] Color nightAmbientLight;
    [SerializeField] AnimationCurve lightChangeCurve;
    [SerializeField] float maxSunLightIntensity;
    public Light moonLight;
    public Light sunLight;
    [SerializeField] float maxMoonLightIntensity;
    public Vector3 initialLightRotation = new Vector3(50, 330, 0);


    [SerializeField] private Transform playerTransform;  // Reference to the player's transform
    [SerializeField] private GameObject savePlayer;  // Key to switch camera view

    [SerializeField] AudioSource audioColiseu;

    public GameObject pos;

    public GameObject[] LastEnemies;

    public bool onetime = false;
    bool makeOne = false;

    // Start is called before the first frame update
    void Start()
    {
        
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHr);

        sunriseTime = TimeSpan.FromHours(sunRiseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);

    }

    // Update is called once per frame
    void Update()
    {
        if (daysToFinish <= 0 && onetime == false)
        {


            playerTransform.gameObject.GetComponent<NavMeshAgent>().enabled = false;

            //Vector3 targetPosition = new Vector3(-87.1100006f, 8.19999981f, -481f);
            Vector3 targetPosition = new Vector3(-24.2f, 8.1f, -154.9f);
            savePlayer.transform.position = targetPosition;
            savePlayer.GetComponent<movement>().isMoving = false;
            SwitchToogle.LoadSoundState();
            if (SwitchToogle.muted == false)
            {
                audioColiseu.Play();
            }
            
            playerTransform.gameObject.GetComponent<NavMeshAgent>().enabled = true;

            //// Call the CompleteOffMeshLink method to ensure correct transition to the new position
            //agent.CompleteOffMeshLink();

            currentTime = DateTime.Now.Date + TimeSpan.FromHours(12);

            UpdateTimeOfDay();
            RotateSun();
            UpdateLightSettings();


            for (int i = 0; i < LastEnemies.Length; i++)
            {
                LastEnemies[i].SetActive(true);
            }

            GameObject[] objects = GameObject.FindGameObjectsWithTag("obj");

            foreach (GameObject obj in objects)
            {
                Destroy(obj);
            }


            onetime = true;
        }
        else
        {
            if(daysToFinish > 0)
            {
                UpdateTimeOfDay();
                RotateSun();
                UpdateLightSettings();
            }
        }
      
    }

    void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

 
        if(timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");

            if (timeText.text == "00:00")
            {
                RemoveDays();
            }
            else
            {
                if(makeOne)
                {
                    makeOne = false;
                }
            }
        }
    }

    void RotateSun()
    {
        float sunLightRotation;

        if(currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);
            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);
            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }
        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan timeDifference = toTime - fromTime;

        if(timeDifference.TotalSeconds < 0)
        {
            timeDifference += TimeSpan.FromHours(24);
        }

        return timeDifference;
    }

    void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0.3f, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0.3f, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }

    void RemoveDays()
    {
        if(makeOne == false)
        {
            daysToFinish--;
            dayText.text = "Dia " + daysToFinish.ToString();
            makeOne = true;
        }
    }
}
