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

        string pbcheck0 = PlayerPrefs.GetString("PBCHECK0" + PlayerPrefs.GetInt("Category", 0), long.MaxValue.ToString());
        string pbcheck1 = PlayerPrefs.GetString("PBCHECK1" + PlayerPrefs.GetInt("Category", 0), long.MaxValue.ToString());
        string pbcheck2 = PlayerPrefs.GetString("PBCHECK2" + PlayerPrefs.GetInt("Category", 0), long.MaxValue.ToString());
        string pbcheck3 = PlayerPrefs.GetString("PBCHECK3" + PlayerPrefs.GetInt("Category", 0), long.MaxValue.ToString());
        string pbcheck4 = PlayerPrefs.GetString("PBCHECK4" + PlayerPrefs.GetInt("Category", 0), long.MaxValue.ToString());
        string pbcheck5 = PlayerPrefs.GetString("PBCHECK5" + PlayerPrefs.GetInt("Category", 0), long.MaxValue.ToString());
        string pbcheck6 = PlayerPrefs.GetString("PBCHECK6" + PlayerPrefs.GetInt("Category", 0), long.MaxValue.ToString());
        string pbcheck7 = PlayerPrefs.GetString("PBCHECK7" + PlayerPrefs.GetInt("Category", 0), long.MaxValue.ToString());
        string pbcheck8 = PlayerPrefs.GetString("PBCHECK8" + PlayerPrefs.GetInt("Category", 0), long.MaxValue.ToString());

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

        PlayerPrefs.SetString("PBCHECK0" + PlayerPrefs.GetInt("Category", 0), pbcheck0);
        PlayerPrefs.SetString("PBCHECK1" + PlayerPrefs.GetInt("Category", 0), pbcheck1);
        PlayerPrefs.SetString("PBCHECK2" + PlayerPrefs.GetInt("Category", 0), pbcheck2);
        PlayerPrefs.SetString("PBCHECK3" + PlayerPrefs.GetInt("Category", 0), pbcheck3);
        PlayerPrefs.SetString("PBCHECK4" + PlayerPrefs.GetInt("Category", 0), pbcheck4);
        PlayerPrefs.SetString("PBCHECK5" + PlayerPrefs.GetInt("Category", 0), pbcheck5);
        PlayerPrefs.SetString("PBCHECK6" + PlayerPrefs.GetInt("Category", 0), pbcheck6);
        PlayerPrefs.SetString("PBCHECK7" + PlayerPrefs.GetInt("Category", 0), pbcheck7);
        PlayerPrefs.SetString("PBCHECK8" + PlayerPrefs.GetInt("Category", 0), pbcheck8);


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
