using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerController : MonoBehaviour
{
    #region Variables

    [HideInInspector] public bool _isNearMinigame;

    #endregion Variables
    #region Metodos

    private void OnMovement(InputValue value)
    {
        SetDirection(value.Get<Vector2>());
    }

    private void OnEAction() 
    {
        if (_isNearMinigame)
            StartMinigame();
    }

    #endregion Metodos
}
