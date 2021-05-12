using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    #region Variables

    private Vector2 _dir;
    public Vector2 lastDir;

    #endregion Variables

    #region Metodos

    private void SetDirection(Vector2 dir) 
    {
        _dir = dir;
        if (dir.x != 0 || dir.y != 0)
        {
            lastDir = dir;
            _animator.SetBool("Moving", true);
        }
        else
        {
            _animator.SetBool("Moving", false);
        }
    }
    private void Move()
    {
        //Update position
        //transform.position = new Vector3(transform.position.x + _dir.y * Time.deltaTime * _velocity, transform.position.y, transform.position.z - _dir.x * Time.deltaTime * _velocity);

        _rigidbody.velocity = new Vector3(_dir.y * Time.deltaTime * _velocity, 0, -_dir.x * Time.deltaTime * _velocity);

        //Look towards the camera
        Vector3 camPos = _mainCamera.transform.position;
        camPos.y = _sprite.transform.position.y;

        _sprite.transform.forward = camPos - _sprite.transform.position;

        //_sprite.flipX = true;

        //Set Animator direction

        float angle = Vector3.SignedAngle(_mainCamera.transform.forward, Vector3.forward, Vector3.up);
        Vector2 rotatedLastDir = Quaternion.Euler(0,0,-angle-90)*lastDir;
        
        float up = Mathf.Max(rotatedLastDir.y, 0);
        float down = -Mathf.Min(rotatedLastDir.y, 0);
        float left = -Mathf.Min(rotatedLastDir.x, 0);
        float right = Mathf.Max(rotatedLastDir.x, 0);

        if (up > 0.4f)
        {
            if (right > 0.4f)
            {
                _animator.SetLayerWeight(1,0);
                _animator.SetLayerWeight(2,1);
                _animator.SetLayerWeight(8,0);
            }
            else if (left > 0.4f)
            {
                _animator.SetLayerWeight(1,0);
                _animator.SetLayerWeight(2,0);
                _animator.SetLayerWeight(8,1);
            }
            else
            {
                _animator.SetLayerWeight(1,1);
                _animator.SetLayerWeight(2,0);
                _animator.SetLayerWeight(8,0);
            }
            _animator.SetLayerWeight(3,0);
            _animator.SetLayerWeight(4,0);
            _animator.SetLayerWeight(5,0);
            _animator.SetLayerWeight(6,0);
            _animator.SetLayerWeight(7,0);
        }
        else if (down > 0.4f)
        {
            if (right > 0.4f)
            {
                _animator.SetLayerWeight(5,0);
                _animator.SetLayerWeight(4,1);
                _animator.SetLayerWeight(6,0);
            }
            else if (left > 0.4f)
            {
                _animator.SetLayerWeight(5,0);
                _animator.SetLayerWeight(4,0);
                _animator.SetLayerWeight(6,1);
            }
            else
            {
                _animator.SetLayerWeight(5,1);
                _animator.SetLayerWeight(4,0);
                _animator.SetLayerWeight(6,0);
            }
            _animator.SetLayerWeight(1,0);
            _animator.SetLayerWeight(2,0);
            _animator.SetLayerWeight(3,0);
            _animator.SetLayerWeight(7,0);
            _animator.SetLayerWeight(8,0);
        }
        else if (right > 0.4f)
        {
            _animator.SetLayerWeight(1,0);
            _animator.SetLayerWeight(2,0);
            _animator.SetLayerWeight(3,1);
            _animator.SetLayerWeight(4,0);
            _animator.SetLayerWeight(5,0);
            _animator.SetLayerWeight(6,0);
            _animator.SetLayerWeight(7,0);
            _animator.SetLayerWeight(8,0);
        }
        else if (left > 0.4f)
        {
            _animator.SetLayerWeight(1,0);
            _animator.SetLayerWeight(2,0);
            _animator.SetLayerWeight(3,0);
            _animator.SetLayerWeight(4,0);
            _animator.SetLayerWeight(5,0);
            _animator.SetLayerWeight(6,0);
            _animator.SetLayerWeight(7,1);
            _animator.SetLayerWeight(8,0);
        }
    }

    #endregion Metodos
}
