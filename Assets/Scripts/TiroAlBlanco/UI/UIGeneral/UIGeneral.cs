﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public partial class UIGeneral : MonoBehaviour
{
    #region Variables

    //Manager
    [Header("Managers")]
    [SerializeField] private ShootingMinigameManager _gameManager;

    //Timer
    [Header("Timer")]
    [SerializeField] private int _timeForLevel;
    [SerializeField] private TextMeshProUGUI _timerText;

    //Puntuación
    [Header("Puntuación")]
    [SerializeField] private TextMeshProUGUI _puntuacionText;
    [SerializeField] private TextMeshProUGUI _puntuacionFinalTxt;
    [SerializeField] private TextMeshProUGUI _puntuacionMaximaTxt;
    [SerializeField] private TextMeshProUGUI _reto1Conseguido;
    [SerializeField] private TextMeshProUGUI _reto2Conseguido;
    [SerializeField] private TextMeshProUGUI _reto3Conseguido;


    //Containers de las fases
    [Header("Fases")]
    [SerializeField] private GameObject _PreGameContainer;
    [SerializeField] private GameObject _inGameContainer;
    [SerializeField] private GameObject _outGameContainer;
    [SerializeField] private GameObject _PostGameContainer;
    [SerializeField] private TextMeshProUGUI _countdown;

    //Puntuaciones necesarias
    [Header("Retos")]
    [SerializeField] private TextMeshProUGUI _estrellasTxt;
    [SerializeField] private Image _estrella1;
    [SerializeField] private Image _estrella2;
    [SerializeField] private Image _estrella3;
    [SerializeField] private Color _StarDoneColor;
    [SerializeField] private Color _StarNotDoneColor;

    //Botonoes necesarios para la navegación por mando
    [Header("Control por mando")]
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _resetButton;

    //Dialogos
    [Header("NPC")]
    [SerializeField] private ConversationHelper _npcConversationHelper;

    //Menú de pausa
    [Header("Menú de pausa")]
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _continuarBtn;
    [HideInInspector] public bool _isPaused;

    [Header("Ajustes")]
    [SerializeField] private GameObject _menuAjustes;
    [SerializeField] private GameObject _ajustesBtnMain;
    [SerializeField] private GameObject _ajustesBtnPausa;
    [SerializeField] private GameObject _primerAjuste;
    private bool _isPause;

    [Header("Tutorial")]
    [SerializeField] private ConversationHelper _npcTutorialConversationHelper;

    [Header("Player")]
    [SerializeField] public PlayerInput _playerInput;

    #endregion Variables

    #region Metodos
    public void InitUI()
    {
        StartTimer();
        ResetPuntuacion();
    }
    #endregion Metodos
}
