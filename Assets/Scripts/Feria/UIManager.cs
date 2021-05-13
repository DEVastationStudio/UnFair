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

    [Header("Pause menu")]
    [SerializeField] private GameObject _basePauseMenu;
    [SerializeField] private GameObject _exitConfirmationPause;

    [Header("Title screen")]
    [SerializeField] private List<GameObject> _virtualCameras;
    [SerializeField] private GameObject _titleScreen;
    [SerializeField] private GameObject _startBtn;

    [Header("Ajustes")]
    [SerializeField] private GameObject _ajustes;
    [SerializeField] private GameObject _primerAjustesBtn;
    [SerializeField] private GameObject _entrarAjustesBtn;
    private bool _isPause = false;

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
    [SerializeField] private GameObject _pauseButtonExit;
    [SerializeField] private GameObject _continuarNoria;
    [Header("Intro cutscene")]
    [SerializeField] private ConversationHelper introNpc;

    #endregion Variables

    #region Metodos

    public void StartGame()
    {
        _playerInput.gameObject.transform.position = new Vector3(-188.84f, 4.9f, 0.4f);
        _playerInput.SwitchCurrentActionMap("UIMap");
        _titleScreen.SetActive(false);
        _virtualCameras[0].SetActive(true);
        StartCoroutine(WaitXSeconds(4));
    }
    public void OpenPauseMenu() {

        _playerInput.SwitchCurrentActionMap("UIMap");
        _basePauseMenu.SetActive(true);
        int totalStars = 0;
        int numStars = GameProgress.GetStars(1);
        totalStars += numStars;
        if (numStars >= 1) _tiroAlBlancoStars[0].SetActive(true);
        if (numStars >= 2) _tiroAlBlancoStars[1].SetActive(true);
        if (numStars == 3) _tiroAlBlancoStars[2].SetActive(true);

        numStars = GameProgress.GetStars(2);
        totalStars += numStars;
        if (numStars >= 1) _caballosStars[0].SetActive(true);
        if (numStars >= 2) _caballosStars[1].SetActive(true);
        if (numStars == 3) _caballosStars[2].SetActive(true);

        numStars = GameProgress.GetStars(3);
        totalStars += numStars;
        if (numStars >= 1) _patosStars[0].SetActive(true);
        if (numStars >= 2) _patosStars[1].SetActive(true);
        if (numStars == 3) _patosStars[2].SetActive(true);

        numStars = GameProgress.GetStars(4);
        totalStars += numStars;
        if (numStars >= 1) _canicasStars[0].SetActive(true);
        if (numStars >= 2) _canicasStars[1].SetActive(true);
        if (numStars == 3) _canicasStars[2].SetActive(true);

        Color c = _noriaIcono.GetComponent<Image>().color;
        if (totalStars < 6)
            _noriaIcono.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0.3f);
        else
            _noriaIcono.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 1f);

        _eventSystem.SetSelectedGameObject(_pauseButtonExit);
    }
    public void ClosePauseMenu() 
    { 
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
    public void OpenNoriaNotAvailable() { 
        Debug.Log("Si que entro"); 
        _noriaNotAvailable.SetActive(true); 
        /*_eventSystem.SetSelectedGameObject(_continuarNoria);*/
        _playerInput.SwitchCurrentActionMap("UIMap");
        Debug.Log("Si que termino");
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
            _eventSystem.SetSelectedGameObject(_startBtn);
        }
        else
        {
            _basePauseMenu.SetActive(true);
            _eventSystem.SetSelectedGameObject(_entrarAjustesBtn);
        }
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
            _playerInput.SwitchCurrentActionMap("ActionMap");
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
    #endregion Metodos

}
