using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayNight : MonoBehaviour
{
    public int days;
    public int hours;
    public int minutes;

    public string amPM = "AM";
    public float counter;

    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CountMinutes();
        SetTime();
    }

    void SetTime()
    {
        int setHours = hours % 12;

        if(setHours == 0)
        {
            setHours = 12;
        }

        if(hours < 12)
        {
            amPM = "AM";
        }
        else
        {
            amPM = "PM";
        }

        int setMinutes = minutes;
        int setDays = days;

        string mins = ConvertToTwoDigit(setMinutes);
        string day = ConvertToTwoDigit(setDays);

        timeText.text = setHours.ToString() + ":" + mins + " "+ amPM;
        dayText.text = "Days " + day;
    }

    string ConvertToTwoDigit(int value)
    {
        string currentString = value.ToString();

        if (currentString.Length == 1)
        {
            currentString = "0" + currentString;
        }
        return currentString;
    }

    void CountMinutes()
    {
        if (counter == 60)
        {
            counter = 0;              
        }

        counter += Time.deltaTime;

        minutes = (int)counter;

        if (counter < 60)
        {
            return;
        }

        if (counter > 60)
        {
            counter = 60;
        }

        if(counter == 60)
        {
            CountHours();
        }
    }

    void CountHours()
    {
        hours++;
        if(hours == 24)
        {
            CountDay();
            hours = 0;
        }
    }

    void CountDay()
    {
        days++;
    }
}
