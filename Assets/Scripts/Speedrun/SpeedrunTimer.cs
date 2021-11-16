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
    public AudioSource audioSource;
    public AudioClip goodSound, badSound;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private GameObject _pb, _pb100, _cn, _tb, _pa, _cb, _8e, _12, _ce, _ct, _ze;
    [SerializeField] private TextMeshProUGUI _pbText, _pbText100, _cnText, _tbText, _paText, _cbText, _8eText, _12Text, _ceText, _ctText, _zeText;
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
            SetCategory(PlayerPrefs.GetInt("Category", 0) == 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_finished) return;
        TimeSpan elapsedTime = (_started ? DateTime.Now - _startTime : TimeSpan.Zero);
        TimeSpan prevRecord = new TimeSpan(long.Parse(PlayerPrefs.GetString("PB" + PlayerPrefs.GetInt("Category", 0), long.MaxValue.ToString())));
        if (prevRecord.Ticks < long.MaxValue)
        {
            _pbText.text = TimeSpanToString(prevRecord);
            _pbText100.text = _pbText.text;
            
        }
        else
        {
            _pbText.text = TimeSpanToString(TimeSpan.Zero);
            _pbText100.text = _pbText.text;
        }
        
        if (elapsedTime > prevRecord) _timerText.color = badColor;
        _timerText.text = TimeSpanToString(elapsedTime);
    }

    private string TimeSpanToString(TimeSpan timeSpan)
    {
        if (timeSpan.Ticks == long.MaxValue) timeSpan = TimeSpan.Zero;
        string text = "<mspace=0.5em>";
        if (timeSpan.Hours > 0) text += timeSpan.Hours + ":";
        text += timeSpan.Minutes.ToString("D2") + ":";
        text += timeSpan.Seconds.ToString("D2") + "<size=60%><mspace=0.5em>.";
        text += timeSpan.Milliseconds.ToString("D2").Substring(0,2);
        return text;
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

        _cnText.text = TimeSpanToString(new TimeSpan(long.Parse(PlayerPrefs.GetString("PBCHECK0", long.MaxValue.ToString()))));
        _cnText.color = Color.white;
        _tbText.text = TimeSpanToString(new TimeSpan(long.Parse(PlayerPrefs.GetString("PBCHECK1", long.MaxValue.ToString()))));
        _tbText.color = Color.white;
        _paText.text = TimeSpanToString(new TimeSpan(long.Parse(PlayerPrefs.GetString("PBCHECK2", long.MaxValue.ToString()))));
        _paText.color = Color.white;
        _cbText.text = TimeSpanToString(new TimeSpan(long.Parse(PlayerPrefs.GetString("PBCHECK3", long.MaxValue.ToString()))));
        _cbText.color = Color.white;
        _8eText.text = TimeSpanToString(new TimeSpan(long.Parse(PlayerPrefs.GetString("PBCHECK4", long.MaxValue.ToString()))));
        _8eText.color = Color.white;
        _12Text.text = TimeSpanToString(new TimeSpan(long.Parse(PlayerPrefs.GetString("PBCHECK5", long.MaxValue.ToString()))));
        _12Text.color = Color.white;
        _ceText.text = TimeSpanToString(new TimeSpan(long.Parse(PlayerPrefs.GetString("PBCHECK6", long.MaxValue.ToString()))));
        _ceText.color = Color.white;
        _ctText.text = TimeSpanToString(new TimeSpan(long.Parse(PlayerPrefs.GetString("PBCHECK7", long.MaxValue.ToString()))));
        _ctText.color = Color.white;
        _zeText.text = TimeSpanToString(new TimeSpan(long.Parse(PlayerPrefs.GetString("PBCHECK8", long.MaxValue.ToString()))));
        _zeText.color = Color.white;
    }
    public static bool StopTimer()
    {
        return instance.Finish();
    }
    public static void ResetTimer()
    {
        instance._started = false;
        instance._finished = false;
        instance._timerText.color = Color.white;
        
        Color color = Color.white;
        instance._cnText.text = instance.TimeSpanToString(new TimeSpan(long.Parse(PlayerPrefs.GetString("PBCHECK0", long.MaxValue.ToString()))));
        instance._cnText.color = color;
        instance._tbText.text = instance.TimeSpanToString(new TimeSpan(long.Parse(PlayerPrefs.GetString("PBCHECK1", long.MaxValue.ToString()))));
        instance._tbText.color = color;
        instance._paText.text = instance.TimeSpanToString(new TimeSpan(long.Parse(PlayerPrefs.GetString("PBCHECK2", long.MaxValue.ToString()))));
        instance._paText.color = color;
        instance._cbText.text = instance.TimeSpanToString(new TimeSpan(long.Parse(PlayerPrefs.GetString("PBCHECK3", long.MaxValue.ToString()))));
        instance._cbText.color = color;
        instance._8eText.text = instance.TimeSpanToString(new TimeSpan(long.Parse(PlayerPrefs.GetString("PBCHECK4", long.MaxValue.ToString()))));
        instance._8eText.color = color;
        instance._12Text.text = instance.TimeSpanToString(new TimeSpan(long.Parse(PlayerPrefs.GetString("PBCHECK5", long.MaxValue.ToString()))));
        instance._12Text.color = color;
        instance._ceText.text = instance.TimeSpanToString(new TimeSpan(long.Parse(PlayerPrefs.GetString("PBCHECK6", long.MaxValue.ToString()))));
        instance._ceText.color = color;
        instance._ctText.text = instance.TimeSpanToString(new TimeSpan(long.Parse(PlayerPrefs.GetString("PBCHECK7", long.MaxValue.ToString()))));
        instance._ctText.color = color;
        instance._zeText.text = instance.TimeSpanToString(new TimeSpan(long.Parse(PlayerPrefs.GetString("PBCHECK8", long.MaxValue.ToString()))));
        instance._zeText.color = color;
    }
    private bool Finish()
    {
        _finished = true;
        TimeSpan elapsedTime = (_started ? DateTime.Now - _startTime : TimeSpan.Zero);
        TimeSpan prevRecord = new TimeSpan(long.Parse(PlayerPrefs.GetString("PB" + PlayerPrefs.GetInt("Category", 0), long.MaxValue.ToString())));
        bool newRecord = elapsedTime < prevRecord;
        if (newRecord) 
        {
            if ((PlayerPrefs.GetInt("Category", 0) == 0) || ((PlayerPrefs.GetInt("Category", 0) == 1) && (PlayerPrefs.GetInt("AltFinal1", 0) == 1) && (PlayerPrefs.GetInt("AltFinal2", 0) == 1) && (PlayerPrefs.GetInt("UsedTokens", 0) == 4)))
            {
                _timerText.color = goodColor;

                PlayerPrefs.SetString("PB" + PlayerPrefs.GetInt("Category", 0), elapsedTime.Ticks.ToString());
                audioSource.clip = goodSound;
            }
            else
            {
                _timerText.color = badColor;
                audioSource.clip = badSound;
            }
        }
        else
        {
            audioSource.clip = badSound;
        }

        audioSource.Play();
        _finalTime = elapsedTime;

        return newRecord;
    }

    public static void SetCategory(bool is100)
    {
        instance._12.SetActive(is100);
        instance._ce.SetActive(is100);
        instance._ct.SetActive(is100);
        instance._ze.SetActive(is100);
        instance._pb.SetActive(!is100);
        instance._pb100.SetActive(is100);
        PlayerPrefs.SetInt("Category", is100 ? 1 : 0);
    }
/// <summary>
/// 0 = "canicas";
/// 1 = "tiro";
/// 2 = "patos";
/// 3 = "caballos";
/// 4 = "8 estrellas";
///<br/> 5 = "12 estrellas";
/// 6 = "espejos";
/// 7 = "terror";
/// 8 = "zenobia"; </summary>
    public static void CompleteCheck(int objective)
    {
        TimeSpan elapsedTime = (instance._started ? DateTime.Now - instance._startTime : TimeSpan.Zero);
        TimeSpan prevRecord = new TimeSpan(long.Parse(PlayerPrefs.GetString("PBCHECK" + objective, long.MaxValue.ToString())));
        string text = instance.TimeSpanToString(elapsedTime);
        Color color = instance.badColor;
        print("For record " + ("PBCHECK" + objective) + ":");
        print("ELAPSED TIME: " + elapsedTime.Minutes + ":" + elapsedTime.Seconds);
        print("PREV RECORD: " + prevRecord.Minutes + ":" + prevRecord.Seconds);
        if (elapsedTime < prevRecord)
        {
            color = instance.goodColor;
            PlayerPrefs.SetString("PBCHECK" + objective, elapsedTime.Ticks.ToString());
            print("Set record " + ("PBCHECK" + objective) + " to: " + elapsedTime.Ticks);
        }
        
        switch (objective)
        {
            case 0:
                instance._cnText.text = text;
                instance._cnText.color = color;
                break;
            case 1:
                instance._tbText.text = text;
                instance._tbText.color = color;
                break;
            case 2:
                instance._paText.text = text;
                instance._paText.color = color;
                break;
            case 3:
                instance._cbText.text = text;
                instance._cbText.color = color;
                break;
            case 4:
                instance._8eText.text = text;
                instance._8eText.color = color;
                break;
            case 5:
                instance._12Text.text = text;
                instance._12Text.color = color;
                break;
            case 6:
                instance._ceText.text = text;
                instance._ceText.color = color;
                break;
            case 7:
                instance._ctText.text = text;
                instance._ctText.color = color;
                break;
            case 8:
                instance._zeText.text = text;
                instance._zeText.color = color;
                break;
        }
    }
}
