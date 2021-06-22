using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIGeneral : MonoBehaviour
{
    #region Variables
    private int actualTime;
    #endregion Variables

    #region Metodos
    private void StartTimer() 
    {
        ResetTimer();
        StartCoroutine(TimerUpdate());
    }
    private void ResetTimer() 
    {
        actualTime = _timeForLevel;
        _timerText.text = "Time: " + _timeForLevel;
        _gameManager._logSystem._TP += _timeForLevel;
    }

    IEnumerator TimerUpdate()
    {
        while (actualTime >= 0)
        {
            _timerText.text = "Time: " + actualTime;
            yield return new WaitForSeconds(1);
            actualTime--;
        }
        faseActual = Fases.POSTGAME;
        FasePostGame();
    }

    public void AddTime(int t) 
    {
        actualTime += t;
        _timerText.text = "Time: " + actualTime;
    }
    #endregion Metodos
}
