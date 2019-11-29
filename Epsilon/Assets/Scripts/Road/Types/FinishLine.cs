using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class FinishLine : MonoBehaviour
{
    public float CurrentLapTime = 0.00f;
    public float LastLap = 0.00f;
    public float BestLap = float.MaxValue;
    public TimeSpan TimeCalculated;
    public DateTime StartTime;
    public Text LastLapText, BestLapText, CurrentLapText;
    // Start is called before the first frame update
    public static FinishLine finishLine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            TimeCalculated = DateTime.Now - StartTime;
            LastLap = (float)TimeCalculated.TotalSeconds;
            if (LastLap < BestLap)
                BestLap = LastLap;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartTime = DateTime.Now;
        }
    }
    private void Update ()
    {
        finishLine = this;
        TimeCalculated = DateTime.Now - StartTime;
        CurrentLapTime = (float)TimeCalculated.TotalSeconds;
        LastLapText.text = "Last Lap Time : " + LastLap;
        CurrentLapText.text = "Current Lap Time : " + CurrentLapTime;
        BestLapText.text = "Best Lap Time : " + BestLap;
    }
}
