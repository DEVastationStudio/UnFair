using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerController : MonoBehaviour
{
    #region Variables

    [HideInInspector] public bool _isNearMinigame;
    [HideInInspector] public bool _isNearConversation;
    private bool _isInterfaz;
    [HideInInspector] public ConversationHelper _conversation;

    #endregion Variables
    #region Metodos

    private void OnMovement(InputValue value)
    {
        if (_isInterfaz) return;
        SetDirection(value.Get<Vector2>());
    }

    private void OnEAction() 
    {
        if (_isInterfaz) return;
        if (_isNearMinigame)
            StartMinigame();
        if (_isNearConversation)
            _conversation.StartConversation();
    }

    private void OnEscAction() 
    {
        _uiManager.OpenPauseMenu();
        gameObject.GetComponent<PlayerInput>().SwitchCurrentActionMap("UIMap");
    }

    private void OnMovementUI() 
    {
        Debug.Log("Funcionaaaa");
    }

    #endregion Metodos
}
