using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUD_Manager : MonoBehaviour
{
    [SerializeField] private GameObject preGameCanvas;
    [SerializeField] private GameObject preGameButtonsCanvas;
    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private GameObject postGameCanvas;
    [SerializeField] private TextMeshProUGUI positionText;
    [SerializeField] private TextMeshProUGUI timeSpent;
    [SerializeField] private TextMeshProUGUI starsEndedGameText;
    [SerializeField] private TextMeshProUGUI starsObtained;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private ConversationHelper conversation;

    [Header("Control por mando")]
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _startGame;
    [SerializeField] private GameObject _resetGame;

    private TimeCounter timeCounter;
    EnemyHorse[] enemyHorses;
    PlayerHorse playerHorse;
    private float raceTime;
    private int stars;
    void Start()
    {
        stars = -1;
        raceTime = 0.0f;
        playerHorse = FindObjectOfType<PlayerHorse>();
        enemyHorses = FindObjectsOfType<EnemyHorse>();
        timeCounter = this.GetComponent<TimeCounter>();
        countdownText.gameObject.SetActive(false);
        inGameCanvas.SetActive(false);
        postGameCanvas.SetActive(false);
        preGameCanvas.SetActive(true);
        _eventSystem.SetSelectedGameObject(_startGame);
        starsObtained.text = "Stars obtained: " + GameProgress.GetStars(2);
    }

    void Update()
    {

    }

    private void StartGame()
    {
        preGameCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
        playerHorse.StartGame();
        foreach (var enemy in enemyHorses)
        {
            enemy.StartGame();
        }
        timeCounter.ActivateTimer();
    }

    public void StartCountdown()
    {
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
    public void ResetGame()
    {
        FadeController.Fade("HorsesRace");
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
        StartGame();
    }
}
