using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    #region Variables
    [Header("Player")]
    [SerializeField] public PlayerInput _playerInput;
    [SerializeField] public PlayerController _player;

    [Header("Pause menu")]
    [SerializeField] private GameObject _basePauseMenu;
    [SerializeField] private GameObject _exitConfirmationPause;
    [SerializeField] private GameObject _quitConfirmationMenu;
    [SerializeField] private Button _quitConfirmationNoButton;
    [SerializeField] private AudioSource _openMapSnd;

    [Header("Title screen")]
    [SerializeField] private List<GameObject> _virtualCameras;
    [SerializeField] private GameObject _titleScreen;
    [SerializeField] private GameObject _startBtn;
    [SerializeField] private GameObject _newGameBtn;

    [Header("Ajustes")]
    [SerializeField] private GameObject _ajustes;
    [SerializeField] private GameObject _primerAjustesBtn;
    [SerializeField] private GameObject _entrarAjustesBtn;
    [SerializeField] private Button _ajustesBtn;
    private bool _isPause = false;

    [Header("Iconos")]
    [SerializeField] private GameObject _tiroAlBlancoIcono;
    [SerializeField] private GameObject _patosIcono;
    [SerializeField] private GameObject _canicasIcono;
    [SerializeField] private GameObject _caballosIcono;

    [Header("Estrellas")]
    [SerializeField] private List<GameObject> _tiroAlBlancoStars;
    [SerializeField] private List<GameObject> _caballosStars;
    [SerializeField] private List<GameObject> _patosStars;
    [SerializeField] private List<GameObject> _canicasStars;

    [Header("Noria")]
    [SerializeField] private GameObject _noriaIcono;
    [SerializeField] private GameObject _noriaNotAvailable;

    [Header("EventSystem and buttons")]
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _pauseCloseButton;
    [SerializeField] private GameObject _continuarNoria;

    [Header("Intro cutscene")]
    [SerializeField] private ConversationHelper introNpc;

    [Header("Segunda confirmación pausa")]
    [SerializeField] private GameObject _panelSegundaConfirmacionPausa;

    [Header("Segunda confirmación menu principal")]
    [SerializeField] private GameObject _panelSegundaConfirmacionMenuPrincipal;

    [Header("Segunda confirmación partida nueva")]
    [SerializeField] private GameObject _panelSegundaConfirmacionResetearPartida;
    [SerializeField] private GameObject _noButtonNewGame;

    #endregion Variables

    #region Metodos

    private void Start()
    {
        if (PlayerPrefs.GetInt("PartidaEmpezada") == 2)
            StartGame();
        else
            SelectStartBtn();
    }

    public void StartGame()
    {
        //_playerInput.gameObject.transform.position = new Vector3(-188.84f, 4.9f, 0.4f);
        _playerInput.SwitchCurrentActionMap("UIMap");
        //PlayerPrefs.SetInt("PartidaEmpezada", 1);
        _titleScreen.SetActive(false);
        _virtualCameras[0].SetActive(true);
        StartCoroutine(WaitXSeconds(4));
        if (AudioManager.instance != null) AudioManager.instance.changeTheme(3);
    }

    public void TogglePauseMenu()
    {
        if (_quitConfirmationMenu.activeSelf)
        {
            _quitConfirmationNoButton.onClick.Invoke();
        }
        else if (_playerInput.currentActionMap.name.Equals("UIMap"))
        {
            if (_basePauseMenu.activeSelf)
            {
                ClosePauseMenu();
            }
            else if (_ajustes.activeSelf)
                CloseAjustes();
        }
        else
        {
            if (!_basePauseMenu.activeSelf)
            {
                OpenPauseMenu();
            }
        }
    }
    public void OpenPauseMenu()
    {
        _openMapSnd.Play();
        _playerInput.SwitchCurrentActionMap("UIMap");
        _basePauseMenu.SetActive(true);
        int totalStars = 0;
        int numStars = GameProgress.GetStars(1);
        totalStars += numStars;
        if (numStars == 0) _tiroAlBlancoIcono.SetActive(false);
        if (numStars >= 1) _tiroAlBlancoStars[0].SetActive(true);
        if (numStars >= 2) _tiroAlBlancoStars[1].SetActive(true);
        if (numStars == 3) _tiroAlBlancoStars[2].SetActive(true);

        numStars = GameProgress.GetStars(2);
        totalStars += numStars;
        if (numStars == 0) _caballosIcono.SetActive(false);
        if (numStars >= 1) _caballosStars[0].SetActive(true);
        if (numStars >= 2) _caballosStars[1].SetActive(true);
        if (numStars == 3) _caballosStars[2].SetActive(true);

        numStars = GameProgress.GetStars(3);
        totalStars += numStars;
        if (numStars == 0) _patosIcono.SetActive(false);
        if (numStars >= 1) _patosStars[0].SetActive(true);
        if (numStars >= 2) _patosStars[1].SetActive(true);
        if (numStars == 3) _patosStars[2].SetActive(true);

        numStars = GameProgress.GetStars(4);
        totalStars += numStars;
        if (numStars == 0) _canicasIcono.SetActive(false);
        if (numStars >= 1) _canicasStars[0].SetActive(true);
        if (numStars >= 2) _canicasStars[1].SetActive(true);
        if (numStars == 3) _canicasStars[2].SetActive(true);

        _eventSystem.SetSelectedGameObject(_pauseCloseButton);
    }
    public void ClosePauseMenu()
    {
        _openMapSnd.Play();
        _basePauseMenu.SetActive(false);
        _playerInput.SwitchCurrentActionMap("ActionMap");
    }
    public void OpenPauseExit()
    {
        _exitConfirmationPause.SetActive(true);
    }
    public void ClosePauseExit()
    {
        _exitConfirmationPause.SetActive(false);
    }
    public void OpenNoriaNotAvailable()
    {
        //Debug.Log("Si que entro");
        _noriaNotAvailable.SetActive(true);
        _playerInput.SwitchCurrentActionMap("UIMap");
        //Debug.Log("Si que termino");
    }
    public void CloseNoriaNotAvailable()
    {
        _noriaNotAvailable.SetActive(false);
        _playerInput.SwitchCurrentActionMap("ActionMap");
    }
    public void OpenAjustes(bool isPause)
    {
        _isPause = isPause;

        _ajustes.SetActive(false);
        if (_isPause)
            _titleScreen.SetActive(false);
        else
            _basePauseMenu.SetActive(false);

        _ajustes.SetActive(true);
        _eventSystem.SetSelectedGameObject(_primerAjustesBtn);
    }
    public void CloseAjustes()
    {
        _ajustes.SetActive(false);
        if (_isPause)
        {
            _titleScreen.SetActive(true);
            _ajustesBtn.Select();
        }
        else
        {
            _basePauseMenu.SetActive(true);
            _eventSystem.SetSelectedGameObject(_entrarAjustesBtn);
        }
    }

    public void OpenSegundaConfirmacionPausa()
    {
        _panelSegundaConfirmacionPausa.SetActive(true);
        //_eventSystem.SetSelectedGameObject(_botonSegundaConfirmacionPausa);
    }
    public void OpenSegundaConfirmacionMenuPrincipal()
    {
        _panelSegundaConfirmacionMenuPrincipal.SetActive(true);
        //_eventSystem.SetSelectedGameObject(_botonSegundaConfirmacionMenuPrincipal);
    }
    public void CloseSegundaConfirmacionPausa()
    {
        //_eventSystem.SetSelectedGameObject(_botonSalirPausa);
        _panelSegundaConfirmacionPausa.SetActive(false);
    }
    public void CloseSegundaConfirmacionMenuPrincipal()
    {
        //_eventSystem.SetSelectedGameObject(_botonSalirMenuPrincipal);
        _panelSegundaConfirmacionMenuPrincipal.SetActive(false);
    }

    private IEnumerator WaitXSeconds(float s)
    {
        yield return new WaitForSeconds(s);
        if (PlayerPrefs.GetInt("Progression", 0) == 0)
        {
            introNpc.StartConversation();
        }
        else
        {
            _player = FindObjectOfType<PlayerController>();

            int prog = PlayerPrefs.GetInt("Progression", 0);
            if (prog == 2 && PlayerPrefs.GetInt("Stars-4", 0) > 0)
            {
                _player._salidaCanicas.StartConversation();
            }
            else if (prog == 4 && PlayerPrefs.GetInt("Stars-1", 0) > 0)
            {
                _player._salidaTiroAlBlanco.StartConversation();
            }
            else if (prog == 7 && PlayerPrefs.GetInt("Stars-3", 0) > 0)
            {
                _player._salidaPatos.StartConversation();
            }
            else if (prog == 9 && PlayerPrefs.GetInt("Stars-2", 0) > 0)
            {
                _player._salidaCaballos.StartConversation();
            }
            else
            {
                _playerInput.SwitchCurrentActionMap("ActionMap");
            }
        }
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void NewGame(bool isHundred)
    {
        SpeedrunTimer.SetCategory(isHundred);
        RemoveData();
    }

    public void RemoveData()
    {
        //Store settings
        float Audio = PlayerPrefs.GetFloat("Audio", 1);
        float Music = PlayerPrefs.GetFloat("Music", 0.5f);
        float Sounds = PlayerPrefs.GetFloat("Sounds", 1);
        int qualityIndex = PlayerPrefs.GetInt("qualityIndex");
        int fpsValue = PlayerPrefs.GetInt("fpsValue");
        int vSyncState = PlayerPrefs.GetInt("vSyncState");

        string pb0 = PlayerPrefs.GetString("PB0", long.MaxValue.ToString());
        string pb1 = PlayerPrefs.GetString("PB1", long.MaxValue.ToString());
        int category = PlayerPrefs.GetInt("Category", 0);

        string pbcheck00 = PlayerPrefs.GetString("PBCHECK00", long.MaxValue.ToString());
        string pbcheck10 = PlayerPrefs.GetString("PBCHECK10", long.MaxValue.ToString());
        string pbcheck20 = PlayerPrefs.GetString("PBCHECK20", long.MaxValue.ToString());
        string pbcheck30 = PlayerPrefs.GetString("PBCHECK30", long.MaxValue.ToString());
        string pbcheck40 = PlayerPrefs.GetString("PBCHECK40", long.MaxValue.ToString());
        string pbcheck50 = PlayerPrefs.GetString("PBCHECK50", long.MaxValue.ToString());
        string pbcheck60 = PlayerPrefs.GetString("PBCHECK60", long.MaxValue.ToString());
        string pbcheck70 = PlayerPrefs.GetString("PBCHECK70", long.MaxValue.ToString());
        string pbcheck80 = PlayerPrefs.GetString("PBCHECK80", long.MaxValue.ToString());
        string pbcheck01 = PlayerPrefs.GetString("PBCHECK01", long.MaxValue.ToString());
        string pbcheck11 = PlayerPrefs.GetString("PBCHECK11", long.MaxValue.ToString());
        string pbcheck21 = PlayerPrefs.GetString("PBCHECK21", long.MaxValue.ToString());
        string pbcheck31 = PlayerPrefs.GetString("PBCHECK31", long.MaxValue.ToString());
        string pbcheck41 = PlayerPrefs.GetString("PBCHECK41", long.MaxValue.ToString());
        string pbcheck51 = PlayerPrefs.GetString("PBCHECK51", long.MaxValue.ToString());
        string pbcheck61 = PlayerPrefs.GetString("PBCHECK61", long.MaxValue.ToString());
        string pbcheck71 = PlayerPrefs.GetString("PBCHECK71", long.MaxValue.ToString());
        string pbcheck81 = PlayerPrefs.GetString("PBCHECK81", long.MaxValue.ToString());

        PlayerPrefs.DeleteAll();

        //Restore settings
        PlayerPrefs.SetFloat("Audio", Audio);
        PlayerPrefs.SetFloat("Music", Music);
        PlayerPrefs.SetFloat("Sounds", Sounds);
        PlayerPrefs.SetInt("qualityIndex", qualityIndex);
        PlayerPrefs.SetInt("fpsValue", fpsValue);
        PlayerPrefs.SetInt("vSyncState", vSyncState);

        PlayerPrefs.SetString("PB0", pb0);
        PlayerPrefs.SetString("PB1", pb1);
        PlayerPrefs.SetInt("Category", category);

        PlayerPrefs.SetString("PBCHECK00", pbcheck00);
        PlayerPrefs.SetString("PBCHECK10", pbcheck10);
        PlayerPrefs.SetString("PBCHECK20", pbcheck20);
        PlayerPrefs.SetString("PBCHECK30", pbcheck30);
        PlayerPrefs.SetString("PBCHECK40", pbcheck40);
        PlayerPrefs.SetString("PBCHECK50", pbcheck50);
        PlayerPrefs.SetString("PBCHECK60", pbcheck60);
        PlayerPrefs.SetString("PBCHECK70", pbcheck70);
        PlayerPrefs.SetString("PBCHECK80", pbcheck80);
        PlayerPrefs.SetString("PBCHECK01", pbcheck01);
        PlayerPrefs.SetString("PBCHECK11", pbcheck11);
        PlayerPrefs.SetString("PBCHECK21", pbcheck21);
        PlayerPrefs.SetString("PBCHECK31", pbcheck31);
        PlayerPrefs.SetString("PBCHECK41", pbcheck41);
        PlayerPrefs.SetString("PBCHECK51", pbcheck51);
        PlayerPrefs.SetString("PBCHECK61", pbcheck61);
        PlayerPrefs.SetString("PBCHECK71", pbcheck71);
        PlayerPrefs.SetString("PBCHECK81", pbcheck81);


        PlayerPrefs.SetInt("PartidaEmpezada", 2);
        FadeController.Fade("Feria");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Feria");
    }

    public void SelectStartBtn()
    {
        if (_startBtn.gameObject.activeSelf)
            _eventSystem.SetSelectedGameObject(_startBtn);
        else
            _eventSystem.SetSelectedGameObject(_newGameBtn);
    }
    #endregion Metodos

}
