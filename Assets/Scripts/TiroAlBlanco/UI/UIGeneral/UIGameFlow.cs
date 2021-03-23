using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIGeneral : MonoBehaviour
{
    #region Variables
    //Fases de la partida
    [HideInInspector]
    public enum Fases
    {
        PREGAME,
        GAME,
        POSTGAME
    }
    [HideInInspector] public Fases faseActual;
    #endregion Variables

    #region Metodos
    public void FasePreGame() 
    {
        faseActual = Fases.PREGAME;
        _PostGameContainer.SetActive(false);
        _PreGameContainer.SetActive(true);
        _estrellasTxt.text = "";
        _estrellasTxt.text += "1xStar -> " + _estrella1 + "\n";
        _estrellasTxt.text += "2xStar -> " + _estrella2 + "\n";
        _estrellasTxt.text += "3xStar -> " + _estrella3;

    }
    public void FaseGame()
    {
        faseActual = Fases.GAME;
        _PreGameContainer.SetActive(false);
        _GameContainer.SetActive(true);
        InitUI();
    }
    public void FasePostGame()
    {
        faseActual = Fases.POSTGAME;
        _GameContainer.SetActive(false);
        _PostGameContainer.SetActive(true);
        Diana[] DianasRestantes = FindObjectsOfType<Diana>();
        if (_puntuacionActual >= _estrella3)
        {
            GameProgress.SetStars(1,3);
            _estrellasConseguidasTxt.text = "3 Stars";
        }
        else if (_puntuacionActual >= _estrella2)
        {
            GameProgress.SetStars(1, 2);
            _estrellasConseguidasTxt.text = "2 Stars";
        }
        else if (_puntuacionActual >= _estrella1)
        {
            GameProgress.SetStars(1, 1);
            _estrellasConseguidasTxt.text = "1 Star";
        }
        else _estrellasConseguidasTxt.text = "No Stars";

        _puntuacionFinalTxt.text = "Score: " + _puntuacionActual;
        for (int i = 0; i < DianasRestantes.Length; i++) 
        {
            Destroy(DianasRestantes[i].gameObject);
        }
    }
    #endregion Metodos
}
