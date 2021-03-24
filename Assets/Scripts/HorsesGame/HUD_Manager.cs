using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HUD_Manager : MonoBehaviour
{
    [SerializeField] private GameObject preGameCanvas;
    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private GameObject postGameCanvas;
    [SerializeField] private TextMeshProUGUI positionText;
    [SerializeField] private TextMeshProUGUI timeSpent;
    [SerializeField] private TextMeshProUGUI starsEndedGameText;
    [SerializeField] private TextMeshProUGUI starsObtained;
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
        inGameCanvas.SetActive(false);
        postGameCanvas.SetActive(false);
        preGameCanvas.SetActive(true);
        starsObtained.text = "Stars obtained: " + GameProgress.GetStars(2);
    }

    void Update()
    {

    }

    public void StartGame()
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
    public void RaceFinished(int position)
    {
        raceTime = timeCounter.GetTimeSpent();
        timeSpent.text = FormatTime();
        playerHorse.EndGame();
        CalculateStars(position);//comprobar que no se llame al reset combo una vez se haya finalizado la carrera

        foreach (var enemy in enemyHorses)
        {
            enemy.EndGame();
        }
        inGameCanvas.SetActive(false);
        postGameCanvas.SetActive(true);
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
        positionText.text = "You finished at " + position + "" + ordinal;

        if (stars > 0)
        {
            starsEndedGameText.text = "You got " + stars;
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
        SceneManager.LoadScene("HorsesRace");
    }

    string FormatTime()
    {
        return ((Mathf.Floor(raceTime) % 60).ToString("00") + " : " + (Mathf.Floor(raceTime / 60).ToString("00")));
    }
    void CalculateStars(int position)
    {
        if (position == 1 && !playerHorse.GetComboFailed() && raceTime <= 30.0f) { stars = 3; }
        else if (position == 1 && raceTime <= 30.0f) { stars = 2; }
        else if (position == 1) { stars = 1; }
        else { stars = 0; }
    }
}
