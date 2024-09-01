using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformWatch : MonoBehaviour
{
    [SerializeField] private TineClock tineClock;

    [SerializeField] private Transform hourHand;
    [SerializeField] private Transform minuteHand;
    [SerializeField] private Transform secondHand;

    private void Update()
    {
        float hours = tineClock.h;
        float minutes = tineClock.m;
        float seconds = tineClock.s;


        hourHand.rotation = Quaternion.Euler(0, 0, (-hours * 15) * 2); // 360°/24 = 15° за час
        minuteHand.rotation = Quaternion.Euler(0, 0, -minutes * 6); // 360°/60 = 6° за минуту
        secondHand.rotation = Quaternion.Euler(0, 0, -seconds * 6); // 360°/60 = 6° за секунду
    }
}
