using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SpeedrunTimer : MonoBehaviour
{
    public static SpeedrunTimer instance; 
    private DateTime _startTime;
    private bool _started, _finished;
    private TimeSpan _finalTime;
    public Color goodColor, badColor;
    [SerializeField] private TextMeshProUGUI _timerText;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_finished) return;
        string tempTimeText = "";
        TimeSpan elapsedTime = (_started ? DateTime.Now - _startTime : TimeSpan.Zero);
        TimeSpan prevRecord = new TimeSpan(PlayerPrefs.GetInt("RecordDays", 9999999), PlayerPrefs.GetInt("RecordHours", -1),PlayerPrefs.GetInt("RecordMinutes", -1),PlayerPrefs.GetInt("RecordSeconds", -1),PlayerPrefs.GetInt("RecordMillis", -1));
        
        if (elapsedTime > prevRecord) _timerText.color = badColor;
        if (elapsedTime.Hours > 0)
        {
            tempTimeText += elapsedTime.Hours + ":";
        }
        tempTimeText += elapsedTime.Minutes.ToString("D2") + ":";
        tempTimeText += elapsedTime.Seconds.ToString("D2") + "<size=60%>.";
        tempTimeText += elapsedTime.Milliseconds.ToString("D2").Substring(0,2);
        _timerText.text = tempTimeText;
    }

    public static void InitTimer()
    {
        if (instance == null) return;
        instance.StartCoroutine(instance.StartTimer());
    }
    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(0.5f);
        _started = true;
        _finished = false;
        _startTime = DateTime.Now;
        _timerText.color = Color.white;
    }
    public static bool StopTimer()
    {
        return instance.Finish();
    }
    public static void ResetTimer()
    {instance.Finish();return;
        instance._started = false;
        instance._finished = false;
        instance._timerText.color = Color.white;
    }
    private bool Finish()
    {
        _finished = true;
        string tempTimeText = "";
        TimeSpan elapsedTime = (_started ? DateTime.Now - _startTime : TimeSpan.Zero);
        TimeSpan prevRecord = new TimeSpan(PlayerPrefs.GetInt("RecordDays", 9999999), PlayerPrefs.GetInt("RecordHours", -1),PlayerPrefs.GetInt("RecordMinutes", -1),PlayerPrefs.GetInt("RecordSeconds", -1),PlayerPrefs.GetInt("RecordMillis", -1));
        bool newRecord = elapsedTime < prevRecord;print(elapsedTime);print(prevRecord);print(newRecord);
        if (newRecord) 
        {
            _timerText.color = goodColor;

            PlayerPrefs.SetInt("RecordDays", elapsedTime.Days);
            PlayerPrefs.SetInt("RecordHours", elapsedTime.Hours);
            PlayerPrefs.SetInt("RecordMinutes", elapsedTime.Minutes);
            PlayerPrefs.SetInt("RecordSeconds", elapsedTime.Seconds);
            PlayerPrefs.SetInt("RecordMillis", elapsedTime.Milliseconds);
        }
        if (elapsedTime.Hours > 0)
        {
            tempTimeText += elapsedTime.Hours + ":";
        }
        tempTimeText += elapsedTime.Minutes.ToString("D2") + ":";
        tempTimeText += elapsedTime.Seconds.ToString("D2") + "<size=60%>.";
        tempTimeText += elapsedTime.Milliseconds.ToString("D2").Substring(0,2);
        _finalTime = elapsedTime;

        return newRecord;
    }
}
