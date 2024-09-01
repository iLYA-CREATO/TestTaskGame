using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmAndClock : MonoBehaviour
{
    public GameObject Clock;
    public GameObject ClockElectro;


    public GameObject Alarm;
    

    public void OpenAlarm()
    {
        Alarm.SetActive(true);

        ClockElectro.SetActive(false);
        Clock.SetActive(false);
    }

    public void OpenClock()
    {
        Alarm.SetActive(false);

        ClockElectro.SetActive(true);
        Clock.SetActive(true);
    }
}
