using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using TMPro;
using System.Threading;

public class TineClock : MonoBehaviour
{
    private string apiUrl = "https://worldtimeapi.org/api/timezone/Etc/GMT-";

    public float h;
    public float m;
    public float s;

    bool isGetTime = false;

    [SerializeField] private TextMeshProUGUI textTime;

    public void Update()
    {
        if(isGetTime)
        {
            s += Time.deltaTime;

            if (s > 60)
            {
                s = 0;
                m += 1;
            }

            if (m > 60)
            {
                m = 0;
                h += 1;
            }
        }  

        int hour = (int)h;
        int minutes = (int)m;
        int seconds = (int)Mathf.Round(s);
        textTime.text = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minutes, seconds);
    }
    int timeRegion;
    public void StartedCor(int timeRegion)
    {
        this.timeRegion = timeRegion;
        StartCoroutine(GetTime(timeRegion));
    }
    public IEnumerator GetTime(int timeRegion)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl + timeRegion))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(webRequest.error);
                ReloadCoroutine();
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                ProcessTimeResponse(jsonResponse);
            }
        }
    }

    private void ReloadCoroutine()
    {
        StopCoroutine(GetTime(this.timeRegion));
        StartCoroutine(GetTime(this.timeRegion));
    }
    private void ProcessTimeResponse(string jsonResponse)
    {
        TimeData timeData = JsonUtility.FromJson<TimeData>(jsonResponse);

        int startIndex = timeData.datetime.IndexOf("T");
        int endIndex = timeData.datetime.LastIndexOf(".");
        if (startIndex != -1 && endIndex !=  -1)
        {
            string result = timeData.datetime.Substring(startIndex + 1, endIndex - startIndex - 1);
            Debug.Log(result);
            timeData.datetime = result;
        }
        
        Debug.Log("Текущее время UTC: " + timeData.datetime);

        // После я разбил бы эти данные на несколько состовляющих
        int endIndexH = timeData.datetime.IndexOf(":");
        if (endIndexH != -1)
        {
            string hour = timeData.datetime.Substring(0, endIndexH);
            string minut = timeData.datetime.Substring(3, endIndexH);
            string second = timeData.datetime.Substring(6, endIndexH);
            Debug.Log(hour + " " + minut + " " + second);

            h = Convert.ToInt32(hour);
            m = Convert.ToInt32(minut);
            s = Convert.ToInt32(second);
        }
        isGetTime = true;
    }

    [System.Serializable]
    public class TimeData
    {
        public string datetime;
    }


    /*
     * "utc_offset": "+00:00",
      "timezone": "Etc/UTC",
      "day_of_week": 6,
      "day_of_year": 244,
      "datetime": "2024-08-31T09:45:44.293346+00:00",
      "utc_datetime": "2024-08-31T09:45:44.293346+00:00",
      "unixtime": 1725097544,
      "raw_offset": 0,
      "week_number": 35,
      "dst": false,
      "abbreviation": "UTC",
      "dst_offset": 0,
      "dst_from": null,
      "dst_until": null,
      "client_ip": "213.135.155.117"
     */
}
