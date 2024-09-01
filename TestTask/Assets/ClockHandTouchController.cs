using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClockHandTouchController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool isRotating = false;
    public float rotationSpeed = 100f; // Скорость вращения стрелки

    public bool h;
    public bool m;
    public bool s;

    public GameObject RotateObject;
    void Start()
    {
        Debug.Log("StartDrop");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        float rotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        if (rotation != 0)
        {
            RotateObject.transform.Rotate(Vector3.forward, -rotation); 
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

         if (h)
         {
            float angle = -(RotateObject.transform.rotation.eulerAngles.z); 
            float currentTime = ((angle % 360) + 360) % 360 / 15; 
            currentTime = currentTime % 12;
            Debug.Log("Часов: " + (int)(currentTime));
            angle = 0;
        }

         if(m)
         {
            float angle = -(RotateObject.transform.rotation.eulerAngles.z);
            int minutes = (int)((angle % 30) / 0.5);
            Debug.Log($"Текущее время: {minutes} минут");
            angle = 0;
         }

         if(s)
         {
            float angle = -(RotateObject.transform.rotation.eulerAngles.z);
            int seconds = (int)((angle % 1) * 60);
             Debug.Log($"Текущее время: {seconds} секунд");
            angle = 0;
        }

    }
}
