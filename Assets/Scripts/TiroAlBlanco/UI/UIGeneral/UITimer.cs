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
    #endregion Metodos
}
