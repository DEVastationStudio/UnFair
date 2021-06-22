using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class LogSystem : MonoBehaviour
{
    /* ---ORDEN DE ARCHIVO---
     * DN -> Diana Normal
     * DD -> Diana Dorada
     * DR -> Diana Reloj
     * DL -> Diana Letra
     * GR -> Gold Rush
     * TP -> Tiempo en partida
     * Orden archivo: [Date - DN - DNDisp - DD - DDDisp - DR - DRDisp - DL - DLDispT - DLDispF - GR - GRTime - DGR - DGRDisp - Miss - TP - DDMVal]
    */
    #region Variables
    [SerializeField] private ShootingMinigameManager _gameManager;

    [HideInInspector] public int _DN = 0;
    [HideInInspector] public int _DD = 0;
    [HideInInspector] public int _DR = 0;
    [HideInInspector] public int _DL = 0;
    [HideInInspector] public int _Miss = 0;
    [HideInInspector] public int _DNDisp = 0;
    [HideInInspector] public int _DDDisp = 0;
    [HideInInspector] public int _DRDisp = 0;
    [HideInInspector] public int _DLDispT = 0;
    [HideInInspector] public int _DLDispF = 0;
    [HideInInspector] public int _TP = 0;
    [HideInInspector] public int _GR = 0;
    [HideInInspector] public float _GRTime = 0;
    [HideInInspector] public int _DGR = 0;
    [HideInInspector] public int _DGRDisp = 0;
    [HideInInspector] public float _DDMValStart = 0;
    [HideInInspector] public float _DDMValEnd = 0;
    [HideInInspector] public float _Score = 0;

    private string _fileName;
    #endregion

    #region Metodos

    private void Start()
    {
        _fileName = Application.dataPath + "/Shooting_Minigame_Data.csv";
        if (!File.Exists(_fileName))
        {
            StartData();
        }
    }


    public void StartData()
    {
        using (StreamWriter sw = new StreamWriter(_fileName, true))
        {
            sw.WriteLine(
                "DATE" + " "+
                "DN" + " " +
                "DNDisp" + " " +
                "DD" + " " +
                "DDDisp" + " " +
                "DR" + " " +
                "DRDisp" + " " +
                "DL" + " " +
                "DLDispT" + " " +
                "DLDispF" + " " +
                "GR" + " " +
                "GRTime" + " " +
                "DGR" + " " +
                "MISS" + " " +
                "TP" + " " +
                "DDMValStart" + " " +
                "DDMValEnd" + " "+
                "SCORE"
            );
            sw.Close();
        }
    }

    public void SaveData() 
    {
        using (StreamWriter sw = new StreamWriter(_fileName, true))
        {   
            sw.WriteLine(
                DateTime.Today.Day+"/"+ DateTime.Today.Month +"/"+ DateTime.Today.Year + " " +
                _DN + " " +
                _DNDisp + " " +
                _DD + " " +
                _DDDisp + " " +
                _DR + " " +
                _DRDisp + " " +
                _DL + " " +
                _DLDispT + " " +
                _DLDispF + " " +
                _GR + " " +
                _GRTime + " " +
                _DGR + " " +
                _Miss + " " +
                _TP + " " +
                _DDMValStart + " " +
                _DDMValEnd + " " + 
                _Score
            );
            sw.Close();
        }
        ResetVariables();
        Debug.Log("Data Saved");
    }

    private void ResetVariables()
    {
        _DN = 0;
        _DD = 0;
        _DR = 0;
        _DL = 0;
        _Miss = 0;
        _DNDisp = 0;
        _DDDisp = 0;
        _DRDisp = 0;
        _DLDispT = 0;
        _DLDispF = 0;
        _TP = 0;
        _GR = 0;
        _GRTime = 0;
        _DGR = 0;
        _DGRDisp = 0;
        _DDMValStart = 0;
        _DDMValEnd = 0;
        _Score = 0;
}

    #endregion
}
