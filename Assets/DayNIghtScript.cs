using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class DayNIghtScript : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay;
    public Volume postProcess;

    [SerializeField] private float tick;

    [SerializeField] private float seconds;
    [SerializeField] private int minutes;
    [SerializeField] private int hours;

    private void Clock()
    {
        seconds += Time.deltaTime * tick;

        if (seconds >= 60)
        {
            seconds = 0;
            minutes++;
        }

        if (minutes >= 60)
        {
            minutes = 0;
            hours++;
        }

        if (hours >= 24)
        {
            hours = 0;
        }
    }

    private void DisplayTime()
    {
        timeDisplay.text = string.Format("{0:00}:{1:00}", hours, minutes);
    }
    
    private void Start()
    {
        postProcess = GetComponent<Volume>();
    }
    
    private void FixedUpdate()
    {
        Clock();
        DisplayTime();
    }
}

