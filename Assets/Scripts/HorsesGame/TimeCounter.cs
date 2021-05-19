using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    private string minutes;
    private string seconds;
    private string miliseconds;
    private float timeSpent;
    [SerializeField] private TextMeshProUGUI timeText;
    private bool timerActivated;
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActivated)
        {
            timeSpent += Time.deltaTime;
            seconds = (Mathf.Floor(timeSpent) % 60).ToString("00");
            minutes = Mathf.Floor(timeSpent / 60).ToString("00");
            //miliseconds = Mathf.Floor((timeSpent*100) % 100).ToString("00");
            timeText.text = minutes + " : " + seconds;

        }
    }

    public void Init()
    {
        timerActivated = false;
        timeSpent = 0.0f;
    }

    public void DeactivateTimer()
    {
        timerActivated = false;
    }

    public void ActivateTimer()
    {
        timerActivated = true;
    }

    public float GetTimeSpent()
    {
        return timeSpent;
    }

    public bool GetActivatedTimer()
    {
        return timerActivated;
    }
}
