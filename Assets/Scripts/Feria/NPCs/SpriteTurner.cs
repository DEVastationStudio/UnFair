using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTurner : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private SpriteRenderer _sprite;
    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = _mainCamera.transform.position;
        camPos.y = _sprite.transform.position.y;

        _sprite.transform.forward = camPos - _sprite.transform.position;

        _sprite.flipX = true;
    }
}
