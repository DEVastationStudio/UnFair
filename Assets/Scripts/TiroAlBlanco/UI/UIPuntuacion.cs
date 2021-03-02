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
        _puntuacionText.text = "Score: " + _puntuacionActual;
    }
    public void IncreasePuntuacion(int points) 
    {
        _puntuacionActual += points;
        _puntuacionText.text = "Score: " + _puntuacionActual;
    }
    #endregion Metodos
}
