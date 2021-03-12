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
    private TimeCounter timeCounter;
    EnemyHorse[] enemyHorses;
    PlayerHorse playerHorse;
    void Start()
    {
        playerHorse = FindObjectOfType<PlayerHorse>();
        enemyHorses = FindObjectsOfType<EnemyHorse>();
        timeCounter = this.GetComponent<TimeCounter>();
        preGameCanvas.SetActive(false);
        preGameCanvas.SetActive(false);
        preGameCanvas.SetActive(true);
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
        timeCounter.enabled = true;
    }
    public void RaceFinished(int position)
    {
        playerHorse.EndGame();
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

    }
    public void ResetGame()
    {
        SceneManager.LoadScene("HorsesRace");
    }
}
