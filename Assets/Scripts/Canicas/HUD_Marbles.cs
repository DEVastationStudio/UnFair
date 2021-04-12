using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class HUD_Marbles : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject preGameCanvas;
    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private GameObject postGameCanvas;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private TextMeshProUGUI preStarsObtainedText;
    [SerializeField] private TextMeshProUGUI postStarsObtainedText;
    [SerializeField] private Thrower thrower;
    [SerializeField] private DynamicDifficultyManager DDM;
    [SerializeField] private float MaxTimeHits = 6; // si se supera este tiempo, bajará la dificultad

    [Header("Control por mando")]
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _resetButton;
    private int score;
    private bool gameStarted;
    private string minutes;
    private string seconds;
    //private string miliseconds;
    private float timeSpent;
    private bool failedBall;
    private int stars;
    private int balls;
    private float velocityHits;

    void Start()
    {
        velocityHits = 0.0f;
        inGameCanvas.SetActive(false);
        postGameCanvas.SetActive(false);
        preGameCanvas.SetActive(true);
        _eventSystem.SetSelectedGameObject(_startButton);
        gameStarted = false;
        failedBall = false;
        score = 0;
        stars = 0;
        timeSpent = 0.0f;
        preStarsObtainedText.text = "Stars obtained: " + GameProgress.GetStars(4);
    }

    void Update()
    {
        if (gameStarted)
        {
            velocityHits += Time.deltaTime;
            timeSpent += Time.deltaTime;
            /*seconds = (Mathf.Floor(timeSpent) % 60).ToString("00");
            minutes = Mathf.Floor(timeSpent / 60).ToString("00");
            //miliseconds = Mathf.Floor((timeSpent*100) % 100).ToString("00");
            timeText.text = minutes + " : " + seconds;*/
            if (thrower.GetBallsLeft() < balls)
            {
                balls = thrower.GetBallsLeft();
                timeText.text = "Balls: " + balls;
            }
        }
    }

    public void StartGame()
    {
        preGameCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
        postGameCanvas.SetActive(false);
        gameStarted = true;
        balls = thrower.GetBallsLeft();
        timeText.text = "Balls: " + balls;
        thrower.SetGameStarted();
    }

    public void EndGame()
    {
        //Physics.autoSimulation = true;
        gameStarted = false;
        inGameCanvas.SetActive(false);
        postGameCanvas.SetActive(true);
        _eventSystem.SetSelectedGameObject(_resetButton);
        preGameCanvas.SetActive(false);
        thrower.SetGameFinished();
        finalScoreText.text = "Score: " + score;
        CalculateStars();
        if (stars > 0)
        {
            if (stars > GameProgress.GetStars(4))
            {
                GameProgress.SetStars(4, stars);
                float auxStarDDM = 0;
                switch (stars)
                {
                    case 0:
                        auxStarDDM = 0.0f;
                        break;

                    case 1:
                        auxStarDDM = 0.25f;
                        break;

                    case 2:
                        auxStarDDM = 0.5f;
                        break;

                    case 3:
                        auxStarDDM = 0.1f;
                        break;
                }

                DDM.SetValue(0, auxStarDDM);
            }
            postStarsObtainedText.text = "You got " + stars + " stars";

        }
        else
        {
            postStarsObtainedText.text = "Sorry you got no star ;(";
        }
        DDM.SaveParameters();
        finalTimeText.text = ((Mathf.Floor(timeSpent / 60).ToString("00")) + " : " + (Mathf.Floor(timeSpent) % 60).ToString("00"));
    }

    public void ResetGame()
    {
        FadeController.Fade("Canicas");
    }

    public void AddScore(int holeScore)
    {
        if (velocityHits >= MaxTimeHits)//bajas dificultad por hacerlo muy lento
        {
            DDM.SetValue(1, 0.1f);

        }
        else if (velocityHits<= 2.0f)
        {
            DDM.SetValue(1, 0.75f);
        }
        else
        {
            DDM.SetValue(1, 0.45f);
        }
        velocityHits = 0.0f;
        score += holeScore;
        scoreText.text = "Score: " + score.ToString();
    }

    public void SetFailBall()
    {
        failedBall = true;
    }

    private void CalculateStars()
    {
        if (!failedBall && score >= 50) { stars = 3; }
        else if (score >= 50) { stars = 2; }
        else if (score > 0) { stars = 1; }
        else { stars = 0; }
    }
}
