using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    #region Variables

    [Header("Movement variables")]
    [SerializeField] private float _velocity;

    #endregion Variables

    #region Metodos
    void Update()
    {
        Move();
    }

    #endregion Metodos
}
