using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIGeneral : MonoBehaviour
{
    #region Variables
    //Fases de la partida
    [HideInInspector]
    public enum Fases
    {
        PREGAME,
        GAME,
        POSTGAME
    }
    [HideInInspector] public Fases faseActual;

    #endregion Variables

    #region Metodos
    public void FasePreGame() 
    {
        faseActual = Fases.PREGAME;
        _PostGameContainer.SetActive(false);
        _PreGameContainer.SetActive(true);
        _estrellasTxt.text = "";
        _estrellasTxt.text += "1xStar -> " + _estrella1 + "\n";
        _estrellasTxt.text += "2xStar -> " + _estrella2 + "\n";
        _estrellasTxt.text += "3xStar -> " + _estrella3;
        _eventSystem.SetSelectedGameObject(_startButton);
        _playerInput.SwitchCurrentActionMap("UIMap");
        Debug.Log("Scheme cambiado");
    }
    public void FaseGame()
    {
        faseActual = Fases.GAME;
        _gameManager._letrasManager.ResetWord(true);
        _PreGameContainer.SetActive(false);
        StartCoroutine(Countdown());
    }
    public void FasePostGame()
    {
        faseActual = Fases.POSTGAME;
        _inGameContainer.SetActive(false);
        _outGameContainer.SetActive(false);
        _PostGameContainer.SetActive(true);
        _gameManager._dynamicDifficultyManager.SaveParameters();
        _gameManager._logSystem._DDMValEnd = _gameManager._dynamicDifficultyManager.GetSkillLevel();
        _gameManager._letrasManager.ResetWord(true);
        Diana[] DianasRestantes = FindObjectsOfType<Diana>();
        if (_puntuacionActual >= _estrella3)
        {
            GameProgress.SetStars(1,3);
            _estrellasConseguidasTxt.text = "3 Stars";
        }
        else if (_puntuacionActual >= _estrella2)
        {
            GameProgress.SetStars(1, 2);
            _estrellasConseguidasTxt.text = "2 Stars";
        }
        else if (_puntuacionActual >= _estrella1)
        {
            GameProgress.SetStars(1, 1);
            _estrellasConseguidasTxt.text = "1 Star";
        }
        else _estrellasConseguidasTxt.text = "No Stars";

        _puntuacionFinalTxt.text = "Score: " + _puntuacionActual;
        _gameManager._logSystem._Score = _puntuacionActual;
        for (int i = 0; i < DianasRestantes.Length; i++) 
        {
           DianasRestantes[i]._dianaContainer.SleepTarget();
           DianasRestantes[i].GetComponentInParent<Animator>().SetBool("isActive", false);
        }
        _gameManager._spawnerDianas._activeLetter = false;
        _gameManager._spawnerDianas._currentLetter = 0;
        _gameManager._spawnerDianas._isOnGoldRush = false;
        _playerInput.SwitchCurrentActionMap("UIMap");
        _npcConversationHelper.StartConversation();
        _gameManager._logSystem.SaveData();
    }

    public void Pause()
    {
        _isPaused = !_isPaused;
        if (_isPaused)
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
            _eventSystem.SetSelectedGameObject(_continuarBtn);
            _playerInput.SwitchCurrentActionMap("UIMap");
        }
        else
        {
            Time.timeScale = 1;
            _pauseMenu.SetActive(false);
            _playerInput.SwitchCurrentActionMap("ActionMap");
        }
    }

    public void OpenAjustes(bool isPause)
    {
        _isPause = isPause;
        _menuAjustes.SetActive(true);
        _eventSystem.SetSelectedGameObject(_primerAjuste);
    }
    public void CloseAjustes()
    {
        _menuAjustes.SetActive(false);
        if (_isPause)
        {
            _eventSystem.SetSelectedGameObject(_ajustesBtnMain);
        }
        else
        {
            _eventSystem.SetSelectedGameObject(_ajustesBtnPausa);
        }
    }

    public void OpenTutorial() 
    {
        _npcTutorialConversationHelper.StartConversation();
    }

    public void CloseTutorial()
    {
        _playerInput.SwitchCurrentActionMap("UIMap");
    }

    public void ExitCurrentGame() 
    {
        Time.timeScale = 1;
        FadeController.Fade("TiroAlBlanco");
    }

    IEnumerator Countdown() 
    {
        _countdown.gameObject.SetActive(true);
        int count = 3;
        while (count > 0)
        {
            _countdown.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
            if(count == 1)
                _gameManager._spawnerDianas.SpawnInit();
        }
        _gameManager._logSystem._DDMValStart = _gameManager._dynamicDifficultyManager.GetSkillLevel();
        _countdown.text = "";
        _countdown.gameObject.SetActive(false);
        _inGameContainer.SetActive(true);
        _outGameContainer.SetActive(true);
        InitUI();
        AudioManager.instance.FadeIn(10,0.1f);
        _playerInput.SwitchCurrentActionMap("ActionMap");
    }
    #endregion Metodos
}
