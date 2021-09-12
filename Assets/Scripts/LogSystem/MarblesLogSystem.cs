using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MarblesLogSystem : MonoBehaviour
{
    /*
    * T -> Time; Total time of the minigame.
    * S -> Stars; Stars achieved by the player.
    * TP -> Total Points; Points obtained by the player.
    * BH -> Basket Hits; Total hits in the basket.
    * NH -> Normal Hits; Total hits in the holes.
    * TF -> Total Fails; Number of total missed balls.
    * SD -> Start Difficulty; Difficulty at the beginning of the game.
    * FD -> Final Difficulty; Difficulty at the end of the game.
    */
    #region Variables
    [SerializeField] private HUD_Marbles _hud;
    /*[SerializeField] private PlayerHorse _playerHorse;
    [SerializeField] private HorsesSpawner _horseSpawner;*/

    [HideInInspector] public float _T = 0;
    [HideInInspector] public int _S = 0;
    [HideInInspector] public int _TP = 0;
    [HideInInspector] public int _BH = 0;
    [HideInInspector] public int _NH = 0;
    [HideInInspector] public float _SD = 0;
    [HideInInspector] public float _FD = 0;

    private string _fileName;
    private const string _DATA_PATH = "/../Minigame_Data/MarblesMinigame/";
    private string _directoryPath;
    #endregion Variables

    #region Methods
    void Start()
    {
        _directoryPath = Application.dataPath + _DATA_PATH;
        _fileName = Application.dataPath + _DATA_PATH + "MarblesMinigame_v" + Application.version + ".csv";
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }

        if (!File.Exists(_fileName))
        {
            StartData();
        }
    }

    private void StartData()
    {
        using (StreamWriter sw = new StreamWriter(_fileName, true))
        {
            sw.WriteLine(
                "DATE" + " " +
                "TIME" + " " +
                "STARS" + " " +
                "TOTAL_POINTS" + " " +
                "BASKET_HITS" + " " +
                "NORMAL_HITS" + " " +
                "START_DIFFICULTY" + " " +
                "FINAL_DIFFICULTY"
            );
            sw.Close();
        }

    }

    public void SaveData()
    {
        using (StreamWriter sw = new StreamWriter(_fileName, true))
        {
            sw.WriteLine(
                DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year + " " +
                _T  + " " + 
                _S  + " " + 
                _TP  + " " + 
                _BH  + " " + 
                _NH  + " " + 
                _SD + " " + 
                _FD
            );

            sw.Close();
        }
        ResetVariables();
        Debug.Log("Data Saved");
    }

    private void ResetVariables()
    {
        _T = 0;
        _S = 0;
        _TP = 0;
        _BH = 0;
        _NH = 0;
        _SD = 0;
        _FD = 0;
    }
    #endregion Methods
}
