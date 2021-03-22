using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    #region Variables
    [Header("Player")]
    [SerializeField] PlayerInput _playerInput;

    [Header("Pause menu")]
    [SerializeField] private GameObject _basePauseMenu;
    [SerializeField] private GameObject _exitConfirmationPause;
    #endregion Variables

    #region Metodos
    public void OpenPauseMenu() { _basePauseMenu.SetActive(true); }
    public void ClosePauseMenu() { _basePauseMenu.SetActive(false); _playerInput.SwitchCurrentActionMap("ActionMap"); }
    public void OpenPauseExit() { _exitConfirmationPause.SetActive(true); }
    public void ClosePauseExit() { _exitConfirmationPause.SetActive(false); }

    public void Exit()
    {
        Application.Quit();
    }
    #endregion Metodos

}
