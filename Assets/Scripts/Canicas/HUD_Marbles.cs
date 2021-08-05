using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUD_Marbles : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject preGameCanvas;
    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private GameObject ballsCanvas;
    [SerializeField] private GameObject postGameCanvas;
    [SerializeField] private GameObject preGameButtonsCanvas;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject continuePauseButton;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject settingsMenuPregameButton;
    [SerializeField] private GameObject settingsMenuIngameButton;
    [SerializeField] private GameObject exitConfirmationPregame;
    [SerializeField] private GameObject exitConfirmationIngame;
    [SerializeField] private GameObject resetConfirmationIngame;
    [SerializeField] private GameObject exitConfirmationPostgame;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private TextMeshProUGUI preStarsObtainedText;
    [SerializeField] private TextMeshProUGUI postStarsObtainedText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private Thrower thrower;
    [SerializeField] private DynamicDifficultyManager DDM;
    [SerializeField] private float MaxTimeHits = 6; // si se supera este tiempo, bajará la dificultad
    [SerializeField] private ConversationHelper conversation;
    [SerializeField] private ConversationHelper conversationTutorial;
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private LineRenderer _lineRenderer;
    
    //[SerializeField] private PlayerInput playerInput;
    [Header("Estrellas Pregame")]
    [SerializeField] private Image _star1Pregame;
    [SerializeField] private Image _star2Pregame;
    [SerializeField] private Image _star3Pregame;
    [Header("Resumen Estrellas PostGame")]
    [SerializeField] private TextMeshProUGUI _textStar1;
    [SerializeField] private TextMeshProUGUI _textStar2;
    [SerializeField] private TextMeshProUGUI _textStar3;
    [SerializeField] private Image _star1;
    [SerializeField] private Image _star2;
    [SerializeField] private Image _star3;
    [SerializeField] private Color _starDoneColor;
    [SerializeField] private Color _starNotDoneColor;
    [SerializeField] private TextMeshProUGUI _textCurrentScore;
    [SerializeField] private TextMeshProUGUI _textBestScore;

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
    [HideInInspector] public static bool isPaused;
    private bool isReseting;
    public static bool startedPressed; //para impedir que los obstáculos se muevan
    private int starNum;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (isPaused || isReseting) { return; }
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
            if (balls <= 0)
            {
                _lineRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.0f));
                thrower.SetInvisibleSlider();

            }
            else
            {
                _lineRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 1.0f));
            }
        }
    }

    private void Init()
    {
        startedPressed = false;
        isPaused = false;
        isReseting = false;
        velocityHits = 0.0f;
        exitConfirmationPregame.SetActive(false);
        exitConfirmationIngame.SetActive(false);
        resetConfirmationIngame.SetActive(false);
        exitConfirmationPostgame.SetActive(false);
        inGameCanvas.SetActive(false);
        ballsCanvas.SetActive(false);
        postGameCanvas.SetActive(false);
        countdownText.gameObject.SetActive(false);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        preGameButtonsCanvas.SetActive(true);
        preGameCanvas.SetActive(true);        
        starNum = GameProgress.GetStars(4);
        _star1Pregame.color = starNum >= 1 ? _starDoneColor : _starNotDoneColor;
        _star2Pregame.color = starNum >= 2 ? _starDoneColor : _starNotDoneColor;
        _star3Pregame.color = starNum >= 3 ? _starDoneColor : _starNotDoneColor;
        _eventSystem.SetSelectedGameObject(_startButton);
        gameStarted = false;
        failedBall = false;
        score = 0;
        scoreText.text = "Score: " + score.ToString();
        stars = 0;
        timeSpent = 0.0f;
        preStarsObtainedText.text = "Stars obtained: " + GameProgress.GetStars(4);
    }

    public void OpenTutorial()
    {
        preGameButtonsCanvas.SetActive(false);
        conversationTutorial.StartConversation();

    }
    private void StartGame()
    {
        preGameCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
        ballsCanvas.SetActive(true);
        postGameCanvas.SetActive(false);
        gameStarted = true;
        balls = thrower.GetBallsLeft();
        timeText.text = "Balls: " + balls;
        thrower.SetGameStarted();
    }

    public void StartCountdown()
    {
        AudioManager.instance.FadeOut(7, 0.1f);
        StartCoroutine(Countdown());
    }
    public void PauseGame(bool _isPaused)
    {
        if (isReseting) { return; }
        isPaused = _isPaused;
        if (isPaused)
        {
            pauseMenu.SetActive(true);
            _eventSystem.SetSelectedGameObject(continuePauseButton);
            //pausar obstáculos

        }
        else
        {
            pauseMenu.SetActive(false);
            //despausar obstáculos

        }

    }

    public void OpenSettingsMenu()
    {
        thrower.SetInSettings(true);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        //_eventSystem.SetSelectedGameObject(firstSettingsButton);

    }
    public void OpenSettingsMenuPregame()
    {
        thrower.SetInSettings(true);
        preGameCanvas.SetActive(false);
        settingsMenu.SetActive(true);
        //_eventSystem.SetSelectedGameObject(firstSettingsButton);
    }

    public void CloseSettingsMenu()
    {
        if (gameStarted)
        {
            CloseSettingsMenuIngame();
        }
        else
        {
            CloseSettingsMenuPregame();
        }
    }

    public void CloseSettingsMenuIngame()
    {
        thrower.SetInSettings(false);
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
        _eventSystem.SetSelectedGameObject(settingsMenuIngameButton);
    }
    public void CloseSettingsMenuPregame()
    {
        thrower.SetInSettings(false);
        settingsMenu.SetActive(false);
        preGameCanvas.SetActive(true);
        _eventSystem.SetSelectedGameObject(settingsMenuPregameButton);

    }
    public void UnPauseGame()
    {
        if (isReseting) { return; }
        thrower.UnPauseGame();
    }
    public void EndGame()
    {
        //Physics.autoSimulation = true;
        startedPressed = false;
        gameStarted = false;
        inGameCanvas.SetActive(false);
        ballsCanvas.SetActive(false);
        postGameCanvas.SetActive(true);
        preGameCanvas.SetActive(false);
        //conversation.StartConversation();
        _eventSystem.SetSelectedGameObject(_resetButton);
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
        if (isReseting) { return; }
        AudioManager.instance.changeTheme(6);
        isReseting = true;
        Marble marbleInGame = FindObjectOfType<Marble>();
        if (marbleInGame != null) Destroy(marbleInGame.gameObject);
        //FadeController.Fade("Canicas");
        Init();
        obstacleSpawner.DestroyObstacles();
        obstacleSpawner.Init();
        thrower.Init();

    }

    public void AddScore(int holeScore)
    {
        if (velocityHits >= MaxTimeHits)//bajas dificultad por hacerlo muy lento
        {
            DDM.SetValue(1, 0.1f);

        }
        else if (velocityHits <= 2.0f)
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
        
        /* TO DO: Estrellas del minijuego, que no estén anidadas        
        _textStar1.text = "1. xxxxxxxxxxxxxxxxxxx: " + x + "/x";
        _textStar2.text = "2. xxxxxxxxxxxxxxxxxxx: " + x + "/x";
        _textStar3.text = "2. xxxxxxxxxxxxxxxxxxx: " + x + "/x";
        if (condition1)
        {            
            _star1.color = _starDoneColor;
        }
        else
        {
            _star1.color = _starNotDoneColor;
        }

        if (condition2)
        {            
            _star2.color = _starDoneColor;
        }
        else
        {
            _star2.color = _starNotDoneColor;
        }

        if (condition3)
        {            
            _star3.color = _starDoneColor;
        }
        else
        {
            _star3.color = _starNotDoneColor;
        }
        */
        
        if (PlayerPrefs.GetInt("BestScoreMarble", 0) == 0 || ((int)Mathf.Floor(score)) < PlayerPrefs.GetInt("BestScoreMarble", 0))
        {
            PlayerPrefs.SetInt("BestScoreMarble", ((int)Mathf.Floor(score)));
        }
        _textCurrentScore.text = "Tiempo" + "\n" + ((int)Mathf.Floor(score));
        _textBestScore.text = "Mejor Tiempo" + "\n" + PlayerPrefs.GetInt("BestScoreMarble");
    }


    IEnumerator Countdown()
    {
        countdownText.gameObject.SetActive(true);
        preGameButtonsCanvas.SetActive(false);
        startedPressed = true;
        int count = 3;
        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        countdownText.text = "";
        countdownText.gameObject.SetActive(false);
        AudioManager.instance.FadeIn(7, 0.1f);
        StartGame();
    }

    public void OpenExitConfirmationPregame()
    {
        exitConfirmationPregame.SetActive(true);
    }

    public void OpenExitConfirmationIngame()
    {
        exitConfirmationIngame.SetActive(true);
    }

    public void OpenResetConfirmationIngame()
    {
        resetConfirmationIngame.SetActive(true);
    }

    public void OpenExitConfirmationPostgame()
    {
        exitConfirmationPostgame.SetActive(true);
    }

    
    public void CloseExitConfirmationPregame()
    {
        exitConfirmationPregame.SetActive(false);
    }

    public void CloseExitConfirmationIngame()
    {
        exitConfirmationIngame.SetActive(false);
    }

    public void CloseResetConfirmationIngame()
    {
        resetConfirmationIngame.SetActive(false);
    }

    public void CloseExitConfirmationPostgame()
    {
        exitConfirmationPostgame.SetActive(false);
    }
}
