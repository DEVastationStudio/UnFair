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

        _sprite.transform.forward = camPos - _sprite.transform.position;//new Vector3(-_mainCamera.transform.forward.x, _sprite.transform.forward.y, -_mainCamera.transform.forward.z);

        //Set Animator direction

        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        float angle = Vector3.SignedAngle(_mainCamera.transform.forward, Vector3.forward, Vector3.up);
        Vector2 rotatedLastDir = Quaternion.Euler(0, 0, -angle - 90) * lastDir;

        float up = Mathf.Max(rotatedLastDir.y, 0);
        float down = -Mathf.Min(rotatedLastDir.y, 0);
        float left = -Mathf.Min(rotatedLastDir.x, 0);
        float right = Mathf.Max(rotatedLastDir.x, 0);

        if (up > 0.3f)
        {
            if (right > 0.4f)
            {
                _animator.SetLayerWeight(1, 0);
                _animator.SetLayerWeight(2, 1);
                _animator.SetLayerWeight(8, 0);
            }
            else if (left > 0.4f)
            {
                _animator.SetLayerWeight(1, 0);
                _animator.SetLayerWeight(2, 0);
                _animator.SetLayerWeight(8, 1);
            }
            else
            {
                _animator.SetLayerWeight(1, 1);
                _animator.SetLayerWeight(2, 0);
                _animator.SetLayerWeight(8, 0);
            }
            _animator.SetLayerWeight(3, 0);
            _animator.SetLayerWeight(4, 0);
            _animator.SetLayerWeight(5, 0);
            _animator.SetLayerWeight(6, 0);
            _animator.SetLayerWeight(7, 0);
        }
        else if (down > 0.3f)
        {
            if (right > 0.4f)
            {
                _animator.SetLayerWeight(5, 0);
                _animator.SetLayerWeight(4, 1);
                _animator.SetLayerWeight(6, 0);
            }
            else if (left > 0.4f)
            {
                _animator.SetLayerWeight(5, 0);
                _animator.SetLayerWeight(4, 0);
                _animator.SetLayerWeight(6, 1);
            }
            else
            {
                _animator.SetLayerWeight(5, 1);
                _animator.SetLayerWeight(4, 0);
                _animator.SetLayerWeight(6, 0);
            }
            _animator.SetLayerWeight(1, 0);
            _animator.SetLayerWeight(2, 0);
            _animator.SetLayerWeight(3, 0);
            _animator.SetLayerWeight(7, 0);
            _animator.SetLayerWeight(8, 0);
        }
        else if (right > 0.4f)
        {
            _animator.SetLayerWeight(1, 0);
            _animator.SetLayerWeight(2, 0);
            _animator.SetLayerWeight(3, 1);
            _animator.SetLayerWeight(4, 0);
            _animator.SetLayerWeight(5, 0);
            _animator.SetLayerWeight(6, 0);
            _animator.SetLayerWeight(7, 0);
            _animator.SetLayerWeight(8, 0);
        }
        else if (left > 0.4f)
        {
            _animator.SetLayerWeight(1, 0);
            _animator.SetLayerWeight(2, 0);
            _animator.SetLayerWeight(3, 0);
            _animator.SetLayerWeight(4, 0);
            _animator.SetLayerWeight(5, 0);
            _animator.SetLayerWeight(6, 0);
            _animator.SetLayerWeight(7, 1);
            _animator.SetLayerWeight(8, 0);
        }
    }

    public void MovePlayer(Vector2 position, float duration)
    {
        StartCoroutine(MovePlayerCR(position, duration));
    }
    public IEnumerator MovePlayerCR(Vector2 position, float duration)
    {
        float elapsedTime = 0;
        Vector3 oldPos = transform.position;
        Vector3 newPos = new Vector3(position.x, oldPos.y, position.y);
        float vel = _velocity;
        _velocity = 0;
        Vector2 dir = new Vector3(newPos.x - oldPos.x, oldPos.z - newPos.z);

        SetDirection(dir);

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(oldPos, newPos, elapsedTime/duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = newPos;
        _animator.SetBool("Moving", false);
        _dir = new Vector2(0, 0);
        _velocity = vel;
    }

    #endregion Metodos
}
