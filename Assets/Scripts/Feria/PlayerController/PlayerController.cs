using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    #region Variables

    [Header("Movement variables")]
    [SerializeField] private float _velocity;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Interfaz")]
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObject _titleScreen;

    [Header("Cameras")]
    [SerializeField] private GameObject _titleCamera;
    [SerializeField] private GameObject _gameCamera;
    [SerializeField] private GameObject  _dummyCamera;
    [SerializeField] private Camera _mainCamera;

    [Header("Animator")]
    [SerializeField] private Animator _animator;

    [Header("Npcs")]
    [SerializeField] private ConversationHelper _salidaCanicas;
    [SerializeField] private ConversationHelper _salidaTiroAlBlanco;
    [SerializeField] private ConversationHelper _salidaPatos;
    [SerializeField] private ConversationHelper _salidaCaballos;

    #endregion Variables

    #region Metodos
    void FixedUpdate()
    {
        Move();
    }

    void Start() {
        StartCoroutine(LoadGameScene());
    }
    private IEnumerator LoadGameScene()
    {
        lastDir = new Vector2(0,1);
        FadeController.instance.player = this;

        _uiManager._playerInput.SwitchCurrentActionMap("UIMap");

        if (FadeController.instance.storedPlayerPosition)
        {
            transform.position = FadeController.instance.lastPlayerPosition;
            lastDir = FadeController.instance.lastPlayerDirection;
            yield return new WaitForSeconds(0.5f);
            _titleCamera.SetActive(false);
            _dummyCamera.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _dummyCamera.SetActive(false);
            _gameCamera.SetActive(true);
            _titleScreen.SetActive(false);
            //if (AudioManager.instance != null) AudioManager.instance.changeTheme(3);
            _uiManager._playerInput.SwitchCurrentActionMap("ActionMap");
        }
        yield return null;
        FadeController.FinishLoad();

        int prog = PlayerPrefs.GetInt("Progression", 0);
        if (prog == 2 && PlayerPrefs.GetInt("Stars-4", 0) > 0)
        {
            _salidaCanicas.StartConversation();
        }
        else if (prog == 4 && PlayerPrefs.GetInt("Stars-1", 0) > 0)
        {
            _salidaTiroAlBlanco.StartConversation();
        }
        else if (prog == 7 && PlayerPrefs.GetInt("Stars-3", 0) > 0)
        {
            _salidaPatos.StartConversation();
        }
        else if (prog == 9 && PlayerPrefs.GetInt("Stars-2", 0) > 0)
        {
            _salidaCaballos.StartConversation();
        }
    }

    #endregion Metodos
}
