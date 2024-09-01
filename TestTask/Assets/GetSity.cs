using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;


// �� ����� ������� ����� ��� ������� �� � ��������� ����� � ��� �����, �� ���� �� ���� 
// ��������� �� ����� Dictionary
public class GetSity : MonoBehaviour
{
    public Dictionary<int, int> timeZone = new Dictionary<int, int>
    {
        {1, 3 }, // ������, ���������, ������, �����-���������
        {2, 6 }, // ����
        {3, 4 } // ������
    };

    public int id;
    public int timeGMT;
    public GameObject SityPanel;
    public bool getSity = false;

    [SerializeField] private TineClock tineClock;
    public void Awake()
    {
        id = PlayerPrefs.GetInt("id");

        if (id == 0)
        {
            SityPanel.SetActive(true);
        }
        else
        {
            SityPanel.SetActive(false);
            _GetSity(id);
            tineClock.StartedCor(timeGMT);
        }
        
    }

    public void _GetSity(int id)
    {
        this.id = id;

        foreach (KeyValuePair<int, int> sity in timeZone)
        {
            if (timeZone.ContainsKey(id))
            {
                timeGMT = timeZone[id];
            }
        }
    }

    public void GetButtonResult()
    {
        if(id != 0)
        {
            PlayerPrefs.SetInt("id", id);
            SityPanel.SetActive(false);
            tineClock.StartedCor(timeGMT);
        }
    }
}
