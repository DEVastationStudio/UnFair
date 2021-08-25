using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class HUD_Manager : MonoBehaviour
{
    [SerializeField] private GameObject preGameCanvas;
    [SerializeField] private GameObject preGameButtonsCanvas;
    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private GameObject timeCanvas;
    [SerializeField] private GameObject postGameCanvas;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject exitConfirmationPregame;
    [SerializeField] private GameObject exitConfirmationPostGame;
    [SerializeField] private GameObject exitConfirmationMenu;
    [SerializeField] private GameObject resetConfirmationMenu;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject settingButtonPause;
    [SerializeField] private GameObject settingButtonPregame;
    //[SerializeField] private TextMeshProUGUI positionText;
    //[SerializeField] private TextMeshProUGUI timeSpent;
    //[SerializeField] private TextMeshProUGUI starsEndedGameText;
    [SerializeField] private TextMeshProUGUI starsObtained;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private ConversationHelper conversation;
    [SerializeField] private ConversationHelper conversationTutorial;
    [SerializeField] private HorsesLogSystem _logSystem;
    //[SerializeField] private PlayerInput playerInput;
    [SerializeField] private FirstPositionController _firstPosController;
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
    [SerializeField] private TextMeshProUGUI _textCurrentTime;
    [SerializeField] private TextMeshProUGUI _textBestTime;
    [SerializeField] private GameObject _youLoseText;

    [Header("Control por mando")]
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _startGame;
    [SerializeField] private GameObject _resetGame;

    private TimeCounter timeCounter;
    private MetaController metaController;
    private HorsesSpawner horsesSpawner;
    EnemyHorse[] enemyHorses;
    PlayerHorse playerHorse;
    private float raceTime;
    private int stars;
    private bool paused;
    private bool isReseting;
    private bool gameStarted;
    private int starNum;
    void Start()
    {
        playerHorse = FindObjectOfType<PlayerHorse>();
        enemyHorses = FindObjectsOfType<EnemyHorse>();
        timeCounter = this.GetComponent<TimeCounter>();
        metaController = FindObjectOfType<MetaController>();
        horsesSpawner = FindObjectOfType<HorsesSpawner>();
        Init();
    }

    void Update()
    {

    }

    private void Init()
    {
        //playerInput.SwitchCurrentActionMap("ActionMap");
        paused = false;
        gameStarted = false;
        isReseting = false;
        stars = -1;
        raceTime = 0.0f;
        countdownText.gameObject.SetActive(false);
        inGameCanvas.SetActive(false);
        timeCanvas.SetActive(false);
        postGameCanvas.SetActive(false);
        pauseMenu.SetActive(false);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        exitConfirmationMenu.SetActive(false);
        exitConfirmationPregame.SetActive(false);
        exitConfirmationPostGame.SetActive(false);
        _star1.gameObject.SetActive(true);
        _star2.gameObject.SetActive(true);
        _star3.gameObject.SetActive(true);
        _youLoseText.SetActive(false);
        resetConfirmationMenu.SetActive(false);
        preGameCanvas.SetActive(true);
        starNum = GameProgress.GetStars(2);
        _star1Pregame.color = starNum >= 1 ? _starDoneColor : _starNotDoneColor;
        _star2Pregame.color = starNum >= 2 ? _starDoneColor : _starNotDoneColor;
        _star3Pregame.color = starNum >= 3 ? _starDoneColor : _starNotDoneColor;
        preGameButtonsCanvas.SetActive(true);
        _eventSystem.SetSelectedGameObject(_startGame);
        starsObtained.text = "Estrellas obtenidas: " + GameProgress.GetStars(2);
        _firstPosController.Init();
    }

    private void StartGame()
    {
        preGameCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
        timeCanvas.SetActive(true);
        playerHorse.StartGame();
        /*foreach (var enemy in enemyHorses)
        {
            enemy.StartGame();
        }*/
        Invoke("UnPauseEnemys", 0.5f);
        gameStarted = true;
        timeCounter.ActivateTimer();
    }

    public void StartCountdown()
    {
        AudioManager.instance.FadeOut(17, 0.1f);
        StartCoroutine(Countdown());
    }

    public void OpenTutorial()
    {
        preGameButtonsCanvas.SetActive(false);
        conversationTutorial.StartConversation();

    }

    public void RaceFinished(int position)
    {
        raceTime = timeCounter.GetTimeSpent();
        _logSystem._T = raceTime;
        //timeSpent.text = FormatTime();
        playerHorse.EndGame();
        CalculateStars(position); //comprobar que no se llame al reset combo una vez se haya finalizado la carrera

        foreach (var enemy in enemyHorses)
        {
            enemy.EndGame();
        }

        inGameCanvas.SetActive(false);
        timeCanvas.SetActive(false);
        postGameCanvas.SetActive(true);
        //conversation.StartConversation();
        _eventSystem.SetSelectedGameObject(_resetGame);

        string ordinal = "";
        switch (position)
        {
            case 1:
                ordinal = "st";
                break;
            case 2:
                ordinal = "nd";
                break;
            case 3:
                ordinal = "rd";
                break;
            case 4:
                ordinal = "th";
                break;

        }
        if (position > 1)
        {
            //positionText.text = "Has perdido ;(";
        }
        else
        {
            //positionText.text = "Posición: " + position;
        }

        if (stars > 0)
        {
            //starsEndedGameText.text = "Estrellas: " + stars;
            if (stars > GameProgress.GetStars(2))
            {
                GameProgress.SetStars(2, stars);
            }
        }
        else
        {
            //starsEndedGameText.text = "No conseguiste estrellas ;( ";
        }
        _logSystem.SaveData();
    }

    public void OpenSettingsMenu()
    {
        playerHorse.SetInSettings(true);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        //_eventSystem.SetSelectedGameObject(firstSettingButton);
    }

    public void OpenSettingsMenuPregame()
    {
        playerHorse.SetInSettings(true);
        preGameCanvas.SetActive(false);
        settingsMenu.SetActive(true);
        //_eventSystem.SetSelectedGameObject(firstSettingButton);
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
        playerHorse.SetInSettings(false);
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
        _eventSystem.SetSelectedGameObject(settingButtonPause);
    }

    public void CloseSettingsMenuPregame()
    {
        playerHorse.SetInSettings(false);
        preGameCanvas.SetActive(true);
        settingsMenu.SetActive(false);
        _eventSystem.SetSelectedGameObject(settingButtonPregame);
    }

    public void ResetGame()
    {
        if (isReseting) { return; }
        isReseting = true;
        AudioManager.instance.changeTheme(16);

        //playerHorse.Init();
        /*foreach (var enemy in enemyHorses)
        {
            enemy.Init();
        }*/
        horsesSpawner.Init();
        timeCounter.Init();
        metaController.Init();

        Init();
        //FadeController.Fade("HorsesRace");
    }

    public void PauseGame(bool isPaused)
    {
        if (isReseting) { return; }
        paused = isPaused;
        if (isPaused)
        {
            //Time.timeScale = 0;
            pauseMenu.SetActive(true);
            _eventSystem.SetSelectedGameObject(continueButton);
            //playerInput.SwitchCurrentActionMap("UIMap");
            Invoke("PauseEnemys", 0.5f);
        }
        else
        {

            //Time.timeScale = 1;
            pauseMenu.SetActive(false);
            //playerInput.SwitchCurrentActionMap("ActionMap");
            Invoke("UnPauseEnemys", 0.5f);
        }

    }

    public void UnPauseGame()
    {
        if (isReseting) { return; }
        playerHorse.UnPauseGame();
    }

    void PauseEnemys()
    {
        if (paused)
        {
            foreach (var enemy in enemyHorses)
            {
                enemy.EndGame();
            }

        }

    }

    void UnPauseEnemys()
    {
        if (!paused)
        {
            foreach (var enemy in enemyHorses)
            {
                enemy.StartGame();
            }
        }
    }
    string FormatTime()
    {
        return ((Mathf.Floor(raceTime / 60).ToString("00")) + " : " + (Mathf.Floor(raceTime) % 60).ToString("00"));
    }
    void CalculateStars(int position)
    {
        int auxStars = 0;

        if (position == 1)
        {
            auxStars++;
            _star1.color = _starDoneColor;
            //
            _textStar1.text = "1. Ganar la carrera: " + position + "/1";
            _textStar2.text = "2. 30 segundos o menos: " + Mathf.Floor(raceTime) + "/30";

            if (PlayerPrefs.GetInt("BestTimeHorse", 0) == 0 || ((int)Mathf.Floor(raceTime)) < PlayerPrefs.GetInt("BestTimeHorse", 0))
            {
                PlayerPrefs.SetInt("BestTimeHorse", ((int)Mathf.Floor(raceTime)));
            }

            _textBestTime.text = "Mejor Tiempo" + "\n" + PlayerPrefs.GetInt("BestTimeHorse");
            _textCurrentTime.text = "Tiempo" + "\n" + ((int)Mathf.Floor(raceTime));
            if (raceTime < 31.0f)
            {
                auxStars++;
                _star2.color = _starDoneColor;
            }
            else
            {
                _star2.color = _starNotDoneColor;
            }

            if (!playerHorse.GetComboFailed() && position == 1)
            {
                auxStars++;
                _star3.color = _starDoneColor;
                _textStar3.text = "3. No fallar ningún combo: 1/1";
            }
            else
            {
                _star3.color = _starNotDoneColor;
                _textStar3.text = "3. No fallar ningún combo: 0/1";

            }

        }
        else
        {
            _star1.color = _starNotDoneColor;
            _star1.gameObject.SetActive(false);
            _star2.gameObject.SetActive(false);
            _star3.gameObject.SetActive(false);
            _youLoseText.SetActive(true);

            _textBestTime.text = "Mejor Tiempo" + "\n" + "-";
            _textCurrentTime.text = "Tiempo" + "\n" + "-";
        }


        stars = auxStars;
        _logSystem._S = stars;
        /*if (position == 1 && !playerHorse.GetComboFailed() && raceTime <= 30.0f) { stars = 3; }
        else if (position == 1 && raceTime <= 30.0f) { stars = 2; }
        else if (position == 1) { stars = 1; }
        else { stars = 0; }*/
    }

    public void DisableComboPanel()
    {
        playerHorse.NextMoveEnd();
    }
    public void OpenConfirmationPregamePanel()
    {
        exitConfirmationPregame.SetActive(true);
    }

    public void CloseConfirmationPregamePanel()
    {
        exitConfirmationPregame.SetActive(false);
        //_eventSystem.SetSelectedGameObject(_startGame);
    }

    public void OpenConfirmationPostgamePanel()
    {
        exitConfirmationPostGame.SetActive(true);
        //_eventSystem.SetSelectedGameObject(noButtonExitPostgame);
    }


    public void CloseConfirmationPostgamePanel()
    {
        exitConfirmationPostGame.SetActive(false);
        //_eventSystem.SetSelectedGameObject(noButtonExitPostgame);
    }

    public void OpenConfirmationPanel()
    {
        exitConfirmationMenu.SetActive(true);
        //_eventSystem.SetSelectedGameObject(noButtonExit);
    }

    public void CloseConfirmationPanel()
    {
        exitConfirmationMenu.SetActive(false);
        //_eventSystem.SetSelectedGameObject(continueButton);
    }
    public void OpenConfirmationResetPanel()
    {
        resetConfirmationMenu.SetActive(true);
        //_eventSystem.SetSelectedGameObject(noButtonReset);
    }

    public void CloseConfirmationResetPanel()
    {
        resetConfirmationMenu.SetActive(false);
        //_eventSystem.SetSelectedGameObject(continueButton);
    }

    IEnumerator Countdown()
    {
        countdownText.gameObject.SetActive(true);
        preGameButtonsCanvas.SetActive(false);
        int count = 3;
        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        countdownText.text = "";
        countdownText.gameObject.SetActive(false);
        AudioManager.instance.FadeIn(17, 0.1f);
        StartGame();
    }
}
