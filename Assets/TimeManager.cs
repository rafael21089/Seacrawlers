using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Canvas timerCanvas;
    int hr=0, min=0, day=30;
    int seg;
    Text dayText, timeText;

    // Start is called before the first frame update
    void Start()
    {
        dayText = timerCanvas.transform.GetChild(0).GetComponent<Text>();
        timeText = timerCanvas.transform.GetChild(1).GetComponent<Text>();
        seg = 0;
    }

    // Update is called once per frame
    void Update()
    {
        seg++; 

        if (seg >= 60)
        {
            seg = 0;
            min++;
        }
        if (min >= 60)
        {
            min = 0;
            hr++;
        }
        if (hr > 23)
        {
            hr = 0;
            day--;
        }

        if(hr > 9 && min > 9)
        {
            dayText.text = "Dia " + day.ToString();
            timeText.text = hr.ToString() + ":" + min.ToString();
        }
        else
        {
            if(hr < 10 || min < 10)
            {
                dayText.text = "Dia " + day.ToString();

                if(hr < 10 && min < 10)
                {
                    timeText.text = "0" + hr.ToString() + ":" + "0" + min.ToString();
                }
                else if (hr < 10 && min > 9)
                {
                    timeText.text = "0" + hr.ToString() + ":" + min.ToString();
                }
                else
                {
                    timeText.text = hr.ToString() + ":" + "0" + min.ToString();
                }
            }
        }
    }
}
