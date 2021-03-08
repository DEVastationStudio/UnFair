using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RodController : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    public GameObject rodTip;
    public Vector3 positionOffset;
    private float _initialHeight;
    private const int _heightOffset = 1;
    private float _height;
    private bool _mouseDown;
    public BoxCollider magnetHitbox;
    public GameObject magnet;
    private Vector3 _mousePos;
    private bool _isGamepad;
    private Vector2 _gamepadCoords;

    void Start()
    {
        positionOffset = transform.position - rodTip.transform.position;
        positionOffset.y = 0;
        _height = 0;
        _initialHeight = transform.position.y;
        _isGamepad = false;
    }


    void Update()
    {
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
        Vector3 newPos = new Vector3(_mousePos.x, _initialHeight - _height, _mousePos.z) + positionOffset;
        transform.position = Vector3.MoveTowards(transform.position, newPos, 8 * Time.deltaTime);
    }

    private void OnLook(InputValue value)
    {
        if (input.currentControlScheme.Equals("KeyboardMouseScheme"))
        {
            Vector2 pos = value.Get<Vector2>();

            _mousePos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 10));
            _isGamepad = false;
        }
        else
        {
            _gamepadCoords = value.Get<Vector2>();
            _isGamepad = true;
            print(_gamepadCoords);
        }
    }

    private void OnMouseLeftAction(InputValue value)
    {
        _mouseDown = ((value.Get<float>()==1) && magnet.tag == "Magnet");
    }
}
