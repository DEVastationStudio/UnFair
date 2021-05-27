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
    [SerializeField] private GameObject postGameCanvas;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject firstSettingButton;
    [SerializeField] private GameObject startGameButtonPregame;
    [SerializeField] private TextMeshProUGUI positionText;
    [SerializeField] private TextMeshProUGUI timeSpent;
    [SerializeField] private TextMeshProUGUI starsEndedGameText;
    [SerializeField] private TextMeshProUGUI starsObtained;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private ConversationHelper conversation;
    //[SerializeField] private PlayerInput playerInput;

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
        postGameCanvas.SetActive(false);
        pauseMenu.SetActive(false);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        preGameCanvas.SetActive(true);
        preGameButtonsCanvas.SetActive(true);
        _eventSystem.SetSelectedGameObject(_startGame);
        starsObtained.text = "Stars obtained: " + GameProgress.GetStars(2);
    }

    private void StartGame()
    {
        preGameCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
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
        AudioManager.instance.FadeOut(17,0.1f);
        StartCoroutine(Countdown());
    }

    public void RaceFinished(int position)
    {
        raceTime = timeCounter.GetTimeSpent();
        timeSpent.text = FormatTime();
        playerHorse.EndGame();
        CalculateStars(position); //comprobar que no se llame al reset combo una vez se haya finalizado la carrera

        foreach (var enemy in enemyHorses)
        {
            enemy.EndGame();
        }

        inGameCanvas.SetActive(false);
        postGameCanvas.SetActive(true);
        conversation.StartConversation();
        //_eventSystem.SetSelectedGameObject(_resetGame);

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
            positionText.text = "You lost the race ;(";
        }
        else
        {
            positionText.text = "You finished at " + position + "" + ordinal;
        }

        if (stars > 0)
        {
            starsEndedGameText.text = "You got " + stars + " stars";
            if (stars > GameProgress.GetStars(2))
            {
                GameProgress.SetStars(2, stars);
            }
        }
        else
        {
            starsEndedGameText.text = "Sorry you got no stars ;( ";
        }
    }

    public void OpenSettingsMenu()
    {
        playerHorse.SetInSettings(true);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        _eventSystem.SetSelectedGameObject(firstSettingButton);
    }

    public void OpenSettingsMenuPregame()
    {
        playerHorse.SetInSettings(true);
        preGameCanvas.SetActive(false);
        settingsMenu.SetActive(true);
        _eventSystem.SetSelectedGameObject(firstSettingButton);
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
        _eventSystem.SetSelectedGameObject(continueButton);
    }

    public void CloseSettingsMenuPregame()
    {
        playerHorse.SetInSettings(false);
        preGameCanvas.SetActive(true);
        settingsMenu.SetActive(false);
        _eventSystem.SetSelectedGameObject(startGameButtonPregame);
    }

    public void ResetGame()
    {
        if (isReseting) { return; }
        isReseting = true;
        AudioManager.instance.changeTheme(16);
        Init();

        //playerHorse.Init();
        /*foreach (var enemy in enemyHorses)
        {
            enemy.Init();
        }*/
        horsesSpawner.Init();
        timeCounter.Init();
        metaController.Init();

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
            if (!playerHorse.GetComboFailed())
            {
                auxStars++;
            }

            if (raceTime < 31.0f)
            {
                auxStars++;
            }

        }
        stars = auxStars;
        /*if (position == 1 && !playerHorse.GetComboFailed() && raceTime <= 30.0f) { stars = 3; }
        else if (position == 1 && raceTime <= 30.0f) { stars = 2; }
        else if (position == 1) { stars = 1; }
        else { stars = 0; }*/
    }

    public void DisableComboPanel()
    {
        playerHorse.NextMoveEnd();
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
        AudioManager.instance.FadeIn(17,0.1f);
        StartGame();
    }
}
