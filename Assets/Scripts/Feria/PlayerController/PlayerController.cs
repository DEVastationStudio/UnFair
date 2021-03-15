using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    #region Variables

    [Header("Movement variables")]
    [SerializeField] private float _velocity;
    [SerializeField] private SpriteRenderer _sprite;

    #endregion Variables

    #region Metodos
    void Update()
    {
        Move();
    }

    #endregion Metodos
}
