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
    void Start()
    {
        timeSpent = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSpent += Time.deltaTime;
        seconds = (Mathf.Floor(timeSpent) % 60).ToString("00");
        minutes = Mathf.Floor(timeSpent / 60).ToString("00");
        //miliseconds = Mathf.Floor((timeSpent*100) % 100).ToString("00");
        timeText.text = minutes + " : " + seconds;
    }
}
