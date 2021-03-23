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

    [Header("Title screen")]
    [SerializeField] private List<GameObject> _virtualCameras;
    [SerializeField] private GameObject _titleScreen;

    [Header("Estrellas")]
    [SerializeField] private List<GameObject> _tiroAlBlancoStars;

    [Header("Estrellas: Caballos")]
    [SerializeField] private List<GameObject> _caballosStars;

    [Header("Estrellas: Patos")]
    [SerializeField] private List<GameObject> _patosStars;

    [Header("Estrellas: Canicas")]
    [SerializeField] private List<GameObject> _canicasStars;

    #endregion Variables

    #region Metodos
    public void StartGame()
    {
        _titleScreen.SetActive(false);
        _virtualCameras[0].SetActive(true);
    }
    public void OpenPauseMenu() { 

        _basePauseMenu.SetActive(true);

        int numStars = GameProgress.GetStars(1);
        if (numStars >= 1) _tiroAlBlancoStars[0].SetActive(true);
        if (numStars >= 2) _tiroAlBlancoStars[1].SetActive(true);
        if (numStars == 3) _tiroAlBlancoStars[2].SetActive(true);

        numStars = GameProgress.GetStars(2);
        if (numStars >= 1) _caballosStars[0].SetActive(true);
        if (numStars >= 2) _caballosStars[1].SetActive(true);
        if (numStars == 3) _caballosStars[2].SetActive(true);

        numStars = GameProgress.GetStars(3);
        if (numStars >= 1) _patosStars[0].SetActive(true);
        if (numStars >= 2) _patosStars[1].SetActive(true);
        if (numStars == 3) _patosStars[2].SetActive(true);

        numStars = GameProgress.GetStars(4);
        if (numStars >= 1) _canicasStars[0].SetActive(true);
        if (numStars >= 2) _canicasStars[1].SetActive(true);
        if (numStars == 3) _canicasStars[2].SetActive(true);
    }
    public void ClosePauseMenu() { _basePauseMenu.SetActive(false); _playerInput.SwitchCurrentActionMap("ActionMap"); }
    public void OpenPauseExit() { _exitConfirmationPause.SetActive(true); }
    public void ClosePauseExit() { _exitConfirmationPause.SetActive(false); }

    public void Exit()
    {
        Application.Quit();
    }
    #endregion Metodos

}
