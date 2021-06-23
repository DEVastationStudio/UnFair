using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class LogSystem : MonoBehaviour
{
    #region Variables
    [SerializeField] private string _dataName;

    private string _fileName;
    private const string _DATA_PATH = "/Minigame_Data/ShootingMinigame/";
    private string _directoryPath;
    #endregion

    #region Metodos

    private void Start()
    {
        _directoryPath = Application.dataPath + _DATA_PATH;
        _fileName = Application.dataPath + _DATA_PATH + _dataName + ".csv";

        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }

        if (!File.Exists(_fileName))
        {
            StartData();
        }
    }


    public void StartData()
    {
        using (StreamWriter sw = new StreamWriter(_fileName, true))
        {
            //Variable headers
            sw.WriteLine(
                "DATE" + " "
            );
            sw.Close();
        }
    }

    public void SaveData()
    {
        using (StreamWriter sw = new StreamWriter(_fileName, true))
        {
            //Variables to save
            sw.WriteLine(
                DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year + " "
            );
            sw.Close();
        }
        ResetVariables();
        Debug.Log("Data Saved");
    }

    private void ResetVariables()
    {
        //Reset variables
    }

    #endregion
}
