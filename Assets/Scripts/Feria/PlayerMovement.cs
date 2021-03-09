using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _velocity;
    private Vector2 _move;
    private void OnMovement(InputValue value)
    {
        _move = value.Get<Vector2>().normalized;
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x + _move.y * Time.deltaTime * _velocity, transform.position.y, transform.position.z - _move.x * Time.deltaTime * _velocity) ;
    }
}
