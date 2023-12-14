using UnityEngine;
using System;
using TMPro;

public class Timer : MonoBehaviour
{
    private float currentTime;
    [SerializeField] private TMP_Text currentTimeText;

    private bool stopTime = false;
    
    void Update()
    {
        if (stopTime == false)
        {
            currentTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            currentTimeText.text = String.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public float getCurrentTime()
    {
        return currentTime;
    }

    public void stopTimer()
    {
        stopTime = true;
    }

    public void saveTime()
    {
        float roundedTime = Mathf.Round(currentTime * 100) / 100;
        PlayerPrefs.SetFloat("CurrentTime", roundedTime);
    }
}
