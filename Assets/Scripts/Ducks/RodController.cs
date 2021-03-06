using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RodController : MonoBehaviour
{
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
        if (_isGamepad)
        {
            _mousePos = rodTip.transform.position + new Vector3(_gamepadCoords.x, 0, _gamepadCoords.y);
        }
        //Read Mouse Down
        _mouseDown = Mouse.current.leftButton.isPressed && magnet.tag == "Magnet";
        magnetHitbox.enabled = _mouseDown && _height >= 1;
        if (_mouseDown && _height < 1)
        {
            _height = Mathf.Min(_height + 2 * Time.deltaTime, 1);
        }
        else if (!_mouseDown && _height > 0)
        {
            _height = Mathf.Max(_height - 2 * Time.deltaTime, 0);
        }

        //Move rod
        Vector3 newPos = new Vector3(_mousePos.x, _initialHeight - _height, _mousePos.z) + positionOffset;
        transform.position = Vector3.MoveTowards(transform.position, newPos, 5 * Time.deltaTime);
    }

    private void OnLook(InputValue value)
    {
        Vector2 pos = value.Get<Vector2>();

        _mousePos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 10));
        _isGamepad = false;
    }
    private void OnLookGamepad(InputValue value)
    {
        _gamepadCoords = value.Get<Vector2>();
        if (Mathf.Abs(_gamepadCoords.x) <= 0.2) _gamepadCoords.x = 0;
        if (Mathf.Abs(_gamepadCoords.y) <= 0.2) _gamepadCoords.y = 0;
        _isGamepad = true;
        print(_gamepadCoords);
    }
}
