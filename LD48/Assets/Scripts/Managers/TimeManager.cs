/*
 *FileName:      TimeManager.cs
 *Author:        MC
 *Date:          2021/04/24 15:41:54
 *UnityVersion:  2020.3.0f1c1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager>
{
    public float timer;

    public Text timerText;

    [HideInInspector]
    public bool isTimeouts;

    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (isTimeouts == false)
        {
            timer += Time.deltaTime;
            timerText.text = timer.ToString("F2");
        }
    }
}
