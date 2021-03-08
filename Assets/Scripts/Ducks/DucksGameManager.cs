using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DucksGameManager : MonoBehaviour
{
    public Duck duckPrefab;
    private int _playerScore, _aiScore;
    public TMP_Text pScoreText, aScoreText;
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


    void Start()
    {
        Duck duck;
        float angle;
        float radius;
        Vector3 pos;
        for (int i = 0; i < 250; i++)
        {
            angle = Random.Range(0f, 360f);
            radius = Random.Range(8f, 10f);
            pos = new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));
            duck = Instantiate(duckPrefab, pos, Quaternion.identity);
            duck._gameManager = this;
            if (i < 10)
            {
                duck.type = Duck.Type.GOLD;
            }
            else if (i < 40)
            {
                duck.type = Duck.Type.BLACK;
            }
            else if (i < 115)
            {
                duck.type = Duck.Type.PLAYER;
            }
            else if (i < 190)
            {
                duck.type = Duck.Type.AI;
            }
            else
            {
                duck.type = Duck.Type.NORMAL;
            }
        }
    }

    private void OnPlayerScoreUpdate(int value)
    {
        pScoreText.text = "Player: " + value;
    }
    private void OnAiScoreUpdate(int value)
    {
        aScoreText.text = "Opponent: " + value;
    }
}
