﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public partial class UIGeneral : MonoBehaviour
{
    #region Variables
    //Timer
    [Header("Timer")]
    [SerializeField] private int _timeForLevel;
    [SerializeField] private TextMeshProUGUI _timerText;

    //Puntuación
    [Header("Puntuación")]
    [SerializeField] private TextMeshProUGUI _puntuacionText;
    [SerializeField] private TextMeshProUGUI _puntuacionFinalTxt;

    //Containers de las fases
    [Header("Fases")]
    [SerializeField] private GameObject _PreGameContainer;
    [SerializeField] private GameObject _GameContainer;
    [SerializeField] private GameObject _PostGameContainer;

    //Puntuaciones necesarias
    [Header("Retos")]
    [SerializeField] private TextMeshProUGUI _estrellasTxt;
    [SerializeField] private int _estrella1;
    [SerializeField] private int _estrella2;
    [SerializeField] private int _estrella3;
    [SerializeField] private TextMeshProUGUI _estrellasConseguidasTxt;


    #endregion Variables

    #region Metodos
    public void InitUI()
    {
        StartTimer();
        ResetPuntuacion();
    }
    #endregion Metodos
}