using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HorsesLogSystem : MonoBehaviour
{
    /*
    * T -> Time; Total time of the minigame.
    * S -> Stars; Stars achieved by the player.
    * PH -> Horse Position; Position where the player spawn.
    * CC -> Correct Combos; Combos finished correctly by the player.
    * FC -> Failed Combos; Combos failed by the player.
    * SD -> Start Difficulty; Difficulty at the beginning of the game.
    * FD -> Final Difficulty; Difficulty at the end of the game.
    */
    #region Variables
    [SerializeField] private HUD_Manager _hudManager;
    [SerializeField] private PlayerHorse _playerHorse;
    [SerializeField] private HorsesSpawner _horseSpawner;

    [HideInInspector] public float _T = 0;
    [HideInInspector] public int _S = 0;
    [HideInInspector] public int _PH = 0;
    [HideInInspector] public int _CC = 0;
    [HideInInspector] public int _FC = 0;
    [HideInInspector] public float _SD = 0;
    [HideInInspector] public float _FD = 0;

    private string _fileName;
    private const string _DATA_PATH = "/../Minigame_Data/HorsesMinigame/";
    private string _directoryPath;
    #endregion Variables

    #region Methods
    void Start()
    {
        _directoryPath = Application.dataPath + _DATA_PATH;
        _fileName = Application.dataPath + _DATA_PATH + "HorsesMinigame_v" + Application.version + ".csv";
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
                "HORSE_POSITION" + " " +
                "COMBOS_CORRECT" + " " +
                "FAILED_COMBOS" + " " +
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
                _PH  + " " + 
                _CC  + " " + 
                _FC  + " " + 
                _SD + " " + 
                _FD
            );

            sw.Close();
        }
        ResetVariables();
        Debug.Log("Data Saved");
        PlayerPrefs.SetInt("playedGamesCaballos",PlayerPrefs.GetInt("playedGamesCaballos")+1);
    }

    private void ResetVariables()
    {
        _T = 0;
        _S = 0;
        _PH = 0;
        _CC = 0;
        _FC = 0;
        _SD = 0;
        _FD = 0;
    }
    #endregion Methods
}
