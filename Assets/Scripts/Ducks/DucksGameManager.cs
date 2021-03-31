using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DucksGameManager : MonoBehaviour
{
    public Duck duckPrefab;
    private int _playerScore, _aiScore;
    public TMP_Text pScoreText, aScoreText, timerText, titleText, endGameText;
    public GameObject menu;
    public LayerMask duckMask;
    public Button startGameButton;

    [Header("Control por mando")]
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _resetButton;

    public int playerScore
    {
        get { return _playerScore; }
        set { OnPlayerScoreUpdate(value); _playerScore = value; }
    }
    public int aiScore
    {
        get { return _aiScore; }
        set { OnAiScoreUpdate(value); _aiScore = value; }
    }

    private float _actualTime;
    public bool gameOver;

    public int totalDucks = 250;

    private int _goldDucks, _blackDucks, _greenDucks, _redDucks;
    public bool gameStarted;
    public bool noBadDucks;

    void Start()
    {
        titleText.text += "\nStars: " + GameProgress.GetStars(3);
        noBadDucks = true;
        _goldDucks  = Mathf.RoundToInt(totalDucks*0.1f);
        _blackDucks = _goldDucks + Mathf.RoundToInt(totalDucks*0.12f);
        _greenDucks = _blackDucks + Mathf.RoundToInt(totalDucks*0.3f);
        _redDucks   = _greenDucks + Mathf.RoundToInt(totalDucks*0.3f);
        _eventSystem.SetSelectedGameObject(_startButton);
        StartCoroutine(GenerateDucks());
    }

    private IEnumerator GenerateDucks()
    {
        Duck duck;
        float angle;
        float radius;
        Vector3 pos;
        bool freeSpace;

        int attempts;

        for (int i = 0; i < totalDucks; i++)
        {
            attempts = 0;
            freeSpace = false;
            while (!freeSpace)
            {
                angle = Random.Range(0f, 360f);
                radius = Random.Range(4f, 15f);
                pos = new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));
                if (!Physics.CheckSphere(pos, 0.03f*40, duckMask))
                {
                    duck = Instantiate(duckPrefab, pos, Quaternion.Euler(-90, 0, 0));
                    duck._gameManager = this;
                    if (i < _goldDucks)
                    {
                        duck.type = Duck.Type.GOLD;
                    }
                    else if (i < _blackDucks)
                    {
                        duck.type = Duck.Type.BLACK;
                    }
                    else if (i < _greenDucks)
                    {
                        duck.type = Duck.Type.PLAYER;
                    }
                    else if (i < _redDucks)
                    {
                        duck.type = Duck.Type.AI;
                    }
                    else
                    {
                        duck.type = Duck.Type.NORMAL;
                    }
                    freeSpace = true;
                }
                else
                {
                    attempts++;
                    if (attempts%20 == 0)
                    {
                        yield return null;
                    }
                    if (attempts > 200)
                    {
                        break;
                    }
                }
                //yield return null;
            }
        }
        startGameButton.interactable = true;
        FadeController.FinishLoad();
    }

    private void OnPlayerScoreUpdate(int value)
    {
        pScoreText.text = "Player: " + value;
    }
    private void OnAiScoreUpdate(int value)
    {
        aScoreText.text = "Opponent: " + value;
    }

    public void StartGame()
    {
        _actualTime = 30;
        gameStarted = true;
        StartCoroutine(TimerUpdate());
    }

    private IEnumerator TimerUpdate()
    {
        while (_actualTime >= 0)
        {
            timerText.text = "Time: " + _actualTime;
            yield return new WaitForSeconds(1);
            _actualTime--;
        }
        //Finish game
        gameOver = true;
        endGameText.text = "Score: " + _playerScore + "/" + _aiScore;

        //Calculate stars
        int stars = 0;
        if (_playerScore > _aiScore)
        {
            stars = 1;
            int difference = Mathf.Abs(_playerScore-_aiScore);
            if (noBadDucks && difference >= 5)
            {
                stars = 3;
            }
            else if (noBadDucks || difference >= 5)
            {
                stars = 2;
            }
        }
        GameProgress.SetStars(3, stars);

        endGameText.text += "\nStars: " + stars;
        menu.SetActive(true);
        _eventSystem.SetSelectedGameObject(_resetButton);
    }

    public void ResetScene()
    {
        FadeController.Fade("Ducks");
    }
}
