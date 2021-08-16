using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RodController : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    public GameObject rodTip;
    public Vector3 positionOffset;
    private float _initialHeight;
    private const int _heightOffset = 1;
    private float _height;
    public bool _mouseDown;
    public BoxCollider magnetHitbox;
    public GameObject magnet;
    private Vector3 _mousePos;
    private bool _isGamepad;
    private Vector2 _gamepadCoords;
    public DucksGameManager gameManager;
    public BoxCollider rodBounds, rodMagnetZone, altRodMagnetZone;
    private Bounds _bounds, _magnetBounds, _altMagnetBounds;
    private bool _isPaused;
    private float _boostTimer, _badBoostTimer;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _continueBtn;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Transform _basketLock, _altBasketLock;

    void Start()
    {
        positionOffset = transform.position - rodTip.transform.position;
        positionOffset.y = 0;
        _height = 0;
        _initialHeight = transform.position.y;
        _isGamepad = false;

        _bounds = rodBounds.bounds;
        rodBounds.enabled = false;

        _magnetBounds = rodMagnetZone.bounds;
        rodMagnetZone.enabled = false;

        _altMagnetBounds = altRodMagnetZone.bounds;
        altRodMagnetZone.enabled = false;
    }


    void FixedUpdate()
    {
        if (!gameManager.gameStarted) return;
        
        //Read Mouse Position
        if (_isGamepad && _gamepadCoords.magnitude != 0)
        {
            _mousePos = transform.position - positionOffset + new Vector3(_gamepadCoords.x, 0, _gamepadCoords.y);
        }
        //Read Mouse Down
        magnetHitbox.enabled = _mouseDown && _height >= 1;
        if (_mouseDown && _height < 1)
        {
            _height = Mathf.Min(_height + 1 * Time.deltaTime, 1);
        }
        else if (!_mouseDown && _height > 0)
        {
            _height = Mathf.Max(_height - 1 * Time.deltaTime, 0);
        }

        //Move rod
        if (!gameManager.gameOver)
        {
            Vector3 newPos;

            if (magnet.tag.Equals("Untagged") && _mousePos.x > _magnetBounds.min.x && _mousePos.x < _magnetBounds.max.x && _mousePos.z > _magnetBounds.min.z && _mousePos.z < _magnetBounds.max.z)
                newPos = new Vector3(_basketLock.position.x, _initialHeight - _height, _basketLock.position.z) + positionOffset;
            else if (magnet.tag.Equals("Untagged") && _mousePos.x > _altMagnetBounds.min.x && _mousePos.x < _altMagnetBounds.max.x && _mousePos.z > _altMagnetBounds.min.z && _mousePos.z < _altMagnetBounds.max.z)
                newPos = new Vector3(_altBasketLock.position.x, _initialHeight - _height, _altBasketLock.position.z) + positionOffset;
            else
                newPos = new Vector3(_mousePos.x, _initialHeight - _height, _mousePos.z) + positionOffset;

            newPos.x = Mathf.Clamp(newPos.x, _bounds.min.x, _bounds.max.x);
            newPos.z = Mathf.Clamp(newPos.z, _bounds.min.z, _bounds.max.z);

            float multiplier = 1.0f;
            float badMultiplier = 1.0f;
            if (_boostTimer > 0)
            {
                multiplier = 1.75f;
                _boostTimer -= Time.fixedDeltaTime;
            }
            if (_badBoostTimer > 0)
            {
                //badMultiplier = 0.25f;
                _badBoostTimer -= Time.fixedDeltaTime;
            }

            transform.position = Vector3.MoveTowards(transform.position, newPos, multiplier * badMultiplier * 10 * Time.deltaTime);
        }
    }

    private void OnLook(InputValue value)
    {
        if (input.currentControlScheme.Equals("KeyboardMouseScheme"))
        {
            Vector2 pos = value.Get<Vector2>();

            if (pos.x == 0 && pos.y == 0) return;

            _mousePos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 30));

            _isGamepad = false;
        }
        else
        {
            _gamepadCoords = value.Get<Vector2>();
            _isGamepad = true;
        }
    }

    private void OnMovement(InputValue value)
    {
        _isGamepad = true;
        _gamepadCoords = value.Get<Vector2>();
    }

    private void OnMouseLeftAction(InputValue value)
    {
        _mouseDown = ((value.Get<float>() == 1) && magnet.tag == "Magnet" && !gameManager.gameOver);
    }
    private void OnSpaceAction(InputValue value)
    {
        _mouseDown = ((value.Get<float>() == 1) && magnet.tag == "Magnet" && !gameManager.gameOver);
    }

    void OnEscAction(InputValue value)
    {       
        if (gameManager.gameStarted && !gameManager.gameOver) 
        {
            Pause();
        }
    }
    public void Pause()
    {
        _isPaused = !_isPaused;
        if (_isPaused)
        {
            _playerInput.SwitchCurrentActionMap("UIMap");
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
            _eventSystem.SetSelectedGameObject(_continueBtn);
        }
        else
        {
            _playerInput.SwitchCurrentActionMap("ActionMap");
            Time.timeScale = 1;
            _pauseMenu.SetActive(false);
        }
    }
    public void ExitCurrentGame() 
    {
        //Time.timeScale = 1;
        //FadeController.Fade("Ducks");

        Pause();
        gameManager.ResetScene();
    }

    public void Boost(int boostTime)
    {
        if (boostTime > 0)
            _boostTimer += boostTime;
        else
            _badBoostTimer -= boostTime;
    }
}
