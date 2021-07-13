using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DianaContainer : MonoBehaviour
{
    #region Variables
    public List<GameObject> _targets;
    private int _targetInUse;
    private Diana _d;
    #endregion

    #region Metodos
    public void WakeUpTarget(int type, int posInArray, int letter = -1)
    {
        if (type == -1)
        {
            _targets[0].SetActive(true);
            _d = _targets[0].GetComponent<Diana>();
            _targetInUse = 0;
            _d._first = true;
            if (letter != -1)
                _d._activeLetter = letter;
        }
        else
        {
            _targets[type].SetActive(true);
            _d = _targets[type].GetComponent<Diana>();
            _targetInUse = type;
            _d._first = false;
            if (letter != -1)
                _d._activeLetter = letter;
        }
        _d._pos = posInArray;
        _d.StartDiana();
    }

    public void SleepTarget()
    {
        _targets[_targetInUse].SetActive(false);
    }
    #endregion
}
