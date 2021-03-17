﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DucksGameManager : MonoBehaviour
{
    public Duck duckPrefab;
    private int _playerScore, _aiScore;
    public TMP_Text pScoreText, aScoreText, timerText, titleText;
    public GameObject menu;
    public LayerMask duckMask;
    public Button startGameButton;
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

    void Start()
    {
        _goldDucks  = Mathf.RoundToInt(totalDucks*0.1f);
        _blackDucks = _goldDucks + Mathf.RoundToInt(totalDucks*0.12f);
        _greenDucks = _blackDucks + Mathf.RoundToInt(totalDucks*0.3f);
        _redDucks   = _greenDucks + Mathf.RoundToInt(totalDucks*0.3f);
        StartCoroutine(GenerateDucks());
    }

    private IEnumerator GenerateDucks()
    {
        Duck duck;
        float angle;
        float radius;
        Vector3 pos;
        bool freeSpace;
        for (int i = 0; i < totalDucks; i++)
        {
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
                //yield return null;
            }
        }
        startGameButton.interactable = true;
        yield return null;
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
        titleText.text = "Score: " + _playerScore + "/" + _aiScore;
        menu.SetActive(true);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene("Ducks");
    }
}
