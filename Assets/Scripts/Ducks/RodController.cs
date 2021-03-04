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

    void Start()
    {
        positionOffset = transform.position - rodTip.transform.position;
        positionOffset.y = 0;
        _height = 0;
        _initialHeight = transform.position.y;
    }


    void Update()
    {
        //Read Mouse Position
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));

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
        Vector3 newPos = new Vector3(worldMousePos.x, _initialHeight - _height, worldMousePos.z) + positionOffset;
        transform.position = Vector3.MoveTowards(transform.position, newPos, 5 * Time.deltaTime);
    }
}
