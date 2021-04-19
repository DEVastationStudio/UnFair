using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

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
    [SerializeField] private int _estrella1;
    [SerializeField] private int _estrella2;
    [SerializeField] private int _estrella3;
    [SerializeField] private TextMeshProUGUI _estrellasConseguidasTxt;

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
    [SerializeField] private GameObject _tutorialContainer;

    #endregion Variables

    #region Metodos
    public void InitUI()
    {
        StartTimer();
        ResetPuntuacion();
    }
    #endregion Metodos
}
