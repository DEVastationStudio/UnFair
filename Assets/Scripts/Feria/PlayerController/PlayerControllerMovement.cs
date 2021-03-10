using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    #region Variables

    private Vector2 _dir;

    #endregion Variables

    #region Metodos

    private void SetDirection(Vector2 dir) 
    {
        _dir = dir;
    }
    private void Move()
    {
        transform.position = new Vector3(transform.position.x + _dir.y * Time.deltaTime * _velocity, transform.position.y, transform.position.z - _dir.x * Time.deltaTime * _velocity);
    }

    #endregion Metodos
}
