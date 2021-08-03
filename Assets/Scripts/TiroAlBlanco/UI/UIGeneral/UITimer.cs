using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIGeneral : MonoBehaviour
{
    #region Variables
    private int actualTime;
    private bool _timerOn;
    private float _timePassed;
    #endregion Variables

    #region Metodos
    private void StartTimer() 
    {
        ResetTimer();
        _timerOn = true;
    }
    public void ResetTimer()
    {
        _timerOn = false;
        actualTime = _timeForLevel;
        _timePassed = 0;
        _timerText.text = "Time: " + _timeForLevel;
        _gameManager._logSystem._TP += _timeForLevel;
    }

    private void Update()
    {
        if (_timerOn)
        {
            if (_timePassed >= 1 && actualTime>0)
            {
                _timerText.text = "Time: " + actualTime;
                actualTime--;
                _timePassed = 0;
            }
            else if(_timePassed < 1 && actualTime > 0)
                _timePassed += Time.deltaTime;
            else
            {
                faseActual = Fases.POSTGAME;
                _timerOn = false;
                FasePostGame(); 
            }
        }
        
    }

    public void AddTime(int t) 
    {
        actualTime += t;
        _timerText.text = "Time: " + actualTime;
    }
    #endregion Metodos
}
