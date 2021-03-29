using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD_Marbles : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject preGameCanvas;
    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private GameObject postGameCanvas;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    private int score;
    private bool gameStarted;
    private string minutes;
    private string seconds;
    //private string miliseconds;
    private float timeSpent;
    void Start()
    {
        inGameCanvas.SetActive(false);
        postGameCanvas.SetActive(false);
        preGameCanvas.SetActive(true);
        gameStarted = false;
        score = 0;
        timeSpent = 0.0f;
    }

    void Update()
    {
        if (gameStarted)
        {
            timeSpent += Time.deltaTime;
            seconds = (Mathf.Floor(timeSpent) % 60).ToString("00");
            minutes = Mathf.Floor(timeSpent / 60).ToString("00");
            //miliseconds = Mathf.Floor((timeSpent*100) % 100).ToString("00");
            timeText.text = minutes + " : " + seconds;
        }
    }

    public void StartGame()
    {
        preGameCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
        postGameCanvas.SetActive(false);
        gameStarted = true;
    }

    public void EndGame()
    {
        gameStarted = false;
        inGameCanvas.SetActive(false);
        postGameCanvas.SetActive(true);
        finalScoreText.text = "Score: " + score;
        finalTimeText.text = ((Mathf.Floor(timeSpent / 60).ToString("00")) + " : " + (Mathf.Floor(timeSpent) % 60).ToString("00"));
        preGameCanvas.SetActive(false);
    }

    public void ResetGame()
    {
        FadeController.Fade("Canicas");
    }

    public void AddScore(int holeScore)
    {
        score += holeScore;
        scoreText.text = "Score: " + score.ToString();
    }
}
