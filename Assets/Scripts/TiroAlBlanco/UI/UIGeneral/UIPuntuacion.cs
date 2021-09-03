using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIGeneral : MonoBehaviour
{
    #region Variables

    private int _puntuacionActual;

    #endregion Variables

    #region Metodos

    private void ResetPuntuacion()
    {
        _puntuacionActual = 0;
        _puntuacionText.text = "Puntos: " + _puntuacionActual;
    } 

    public void IncreasePuntuacion(int points) 
    {
        if (_puntuacionActual + points < 0)
            _puntuacionActual = 0;
        else 
            _puntuacionActual += points;
        _puntuacionText.text = "Puntos: " + _puntuacionActual;
    }

    public int GetPuntuacion() { return _puntuacionActual; }

    #endregion Metodos
}
