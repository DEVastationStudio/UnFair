using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DucksLogSystem : MonoBehaviour
{
    /*
     * PD -> Player Ducks; Total ducks fished by the player (For points only)
     * ED -> Enemy Ducks; Total ducks fished by the ai
     * BD -> Boost Ducks; Total ducks fished by the player (For boosts only)

     * PS -> Player Score
     * ES -> Enemy Score

     * PD0 -> Player Ducks 0; Purple (-2) ducks fished by the player (for points only)
     * PD1 -> Player Ducks 1; White (+1) ducks fished by the player (for points only)
     * PD2 -> Player Ducks 2; Green (+2) ducks fished by the player (for points only)
     * PD3 -> Player Ducks 3; Time (+5s) ducks fished by the player (for points only)
     * PD4 -> Player Ducks 4; Golden (+5) ducks fished by the player (for points only)
     * PD5 -> Player Ducks 5; Big (+7) ducks fished by the player (for points only)

     * ED0 -> Player Ducks 0; Purple (-2) ducks fished by the ai (for points only)
     * ED1 -> Player Ducks 1; White (+1) ducks fished by the ai (for points only)
     * ED2 -> Player Ducks 2; Green (+2) ducks fished by the ai (for points only)
     * ED3 -> Player Ducks 3; Time (+5s) ducks fished by the ai (for points only)
     * ED4 -> Player Ducks 4; Golden (+5) ducks fished by the ai (for points only)
     * ED5 -> Player Ducks 5; Big (+7) ducks fished by the ai (for points only)

     * BD0 -> Player Ducks 0; Purple (-2) ducks fished by the player (for boosts only)
     * BD1 -> Player Ducks 1; White (+1) ducks fished by the player (for boosts only)
     * BD2 -> Player Ducks 2; Green (+2) ducks fished by the player (for boosts only)
     * BD3 -> Player Ducks 3; Time (+5s) ducks fished by the player (for points only)
     * BD4 -> Player Ducks 4; Golden (+5) ducks fished by the player (for boosts only)
     * BD5 -> Player Ducks 5; Big (+7) ducks fished by the player (for boosts only)

     * GT -> Game Time
    */
    #region Variables
    [SerializeField] private DucksGameManager _gameManager;

    [HideInInspector] public int _PD = 0;
    [HideInInspector] public int _ED = 0;
    [HideInInspector] public int _BD = 0;

    [HideInInspector] public int _PS = 0;
    [HideInInspector] public int _ES = 0;

    [HideInInspector] public int _PD0 = 0;
    [HideInInspector] public int _PD1 = 0;
    [HideInInspector] public int _PD2 = 0;
    [HideInInspector] public int _PD3 = 0;
    [HideInInspector] public int _PD4 = 0;
    [HideInInspector] public int _PD5 = 0;

    [HideInInspector] public int _ED0 = 0;
    [HideInInspector] public int _ED1 = 0;
    [HideInInspector] public int _ED2 = 0;
    [HideInInspector] public int _ED3 = 0;
    [HideInInspector] public int _ED4 = 0;
    [HideInInspector] public int _ED5 = 0;

    [HideInInspector] public int _BD0 = 0;
    [HideInInspector] public int _BD1 = 0;
    [HideInInspector] public int _BD2 = 0;
    [HideInInspector] public int _BD3 = 0;
    [HideInInspector] public int _BD4 = 0;
    [HideInInspector] public int _BD5 = 0;

    [HideInInspector] public int _GT = 0;

    [HideInInspector] public float _DDMValStart = 0;
    [HideInInspector] public float _DDMValEnd = 0;

    private string _fileName;
    private const string _DATA_PATH = "/../Minigame_Data/DucksMinigame/";
    private string _directoryPath;
    #endregion

    #region Metodos

    private void Start()
    {
        _directoryPath = Application.dataPath + _DATA_PATH;
        _fileName = Application.dataPath + _DATA_PATH + "Ducks_v" + Application.version + ".csv";

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
                "PLAYER_DUCKS" + " " +
                "ENEMY_DUCKS" + " " +
                "BOOST_DUCKS" + " " +
                "PLAYER_SCORE" + " " +
                "ENEMY_SCORE" + " " +
                "PLAYER_DUCKS_0" + " " +
                "PLAYER_DUCKS_1" + " " +
                "PLAYER_DUCKS_2" + " " +
                "PLAYER_DUCKS_3" + " " +
                "PLAYER_DUCKS_4" + " " +
                "PLAYER_DUCKS_5" + " " +
                "ENEMY_DUCKS_0" + " " +
                "ENEMY_DUCKS_1" + " " +
                "ENEMY_DUCKS_2" + " " +
                "ENEMY_DUCKS_3" + " " +
                "ENEMY_DUCKS_4" + " " +
                "ENEMY_DUCKS_5" + " " +
                "BOOST_DUCKS_0" + " " +
                "BOOST_DUCKS_1" + " " +
                "BOOST_DUCKS_2" + " " +
                "BOOST_DUCKS_3" + " " +
                "BOOST_DUCKS_4" + " " +
                "BOOST_DUCKS_5" + " " +
                "GAME_TIME" + " " +
                "START_DIFFICULTY" + " " +
                "END_DIFFICULTY"
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
                _PD + " " +
                _ED + " " +
                _BD + " " +
                _PS + " " +
                _ES + " " +
                _PD0 + " " +
                _PD1 + " " +
                _PD2 + " " +
                _PD3 + " " +
                _PD4 + " " +
                _PD5 + " " +
                _ED0 + " " +
                _ED1 + " " +
                _ED2 + " " +
                _ED3 + " " +
                _ED4 + " " +
                _ED5 + " " +
                _BD0 + " " +
                _BD1 + " " +
                _BD2 + " " +
                _BD3 + " " +
                _BD4 + " " +
                _BD5 + " " +
                _GT + " " +
                _DDMValStart + " " +
                _DDMValEnd
            );

            sw.Close();
        }
        ResetVariables();
        Debug.Log("Data Saved");
        PlayerPrefs.SetInt("playedGamesPatos",PlayerPrefs.GetInt("playedGamesPatos")+1);
    }

    public void ResetVariables()
    {
        _PD = 0;
        _ED = 0;
        _BD = 0;
        _PS = 0;
        _ES = 0;
        _PD0 = 0;
        _PD1 = 0;
        _PD2 = 0;
        _PD3 = 0;
        _PD4 = 0;
        _PD5 = 0;
        _ED0 = 0;
        _ED1 = 0;
        _ED2 = 0;
        _ED3 = 0;
        _ED4 = 0;
        _ED5 = 0;
        _BD0 = 0;
        _BD1 = 0;
        _BD2 = 0;
        _BD3 = 0;
        _BD4 = 0;
        _BD5 = 0;
        _GT = 0;
        _DDMValStart = 0;
        _DDMValEnd = 0;
    }
    #endregion
}
