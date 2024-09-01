using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClockHandTouchController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool isRotating = false;
    public float rotationSpeed = 100f; // �������� �������� �������

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
            Debug.Log("�����: " + (int)(currentTime));
            angle = 0;
        }

         if(m)
         {
            float angle = -(RotateObject.transform.rotation.eulerAngles.z);
            int minutes = (int)((angle % 30) / 0.5);
            Debug.Log($"������� �����: {minutes} �����");
            angle = 0;
         }

         if(s)
         {
            float angle = -(RotateObject.transform.rotation.eulerAngles.z);
            int seconds = (int)((angle % 1) * 60);
             Debug.Log($"������� �����: {seconds} ������");
            angle = 0;
        }

    }
}
