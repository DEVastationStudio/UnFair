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
    private int starNum = 0;
    #endregion Variables

    #region Metodos
    public void FasePreGame()
    {
        Diana[] DianasRestantes = FindObjectsOfType<Diana>();
        for (int i = 0; i < DianasRestantes.Length; i++) 
        {
            DianasRestantes[i]._dianaContainer.SleepTarget();
            DianasRestantes[i].GetComponentInParent<Animator>().Play("DianaHide_Anim");
        }
        Time.timeScale = 1;
        _countdown.text = "";
        starNum = GameProgress.GetStars(1);
        _estrellaPreGame1.color = starNum >= 1 ? _StarDoneColor : _StarNotDoneColor;
        _estrellaPreGame2.color = starNum >= 2 ? _StarDoneColor : _StarNotDoneColor;
        _estrellaPreGame3.color = starNum >= 3 ? _StarDoneColor : _StarNotDoneColor;

        faseActual = Fases.PREGAME;
        _PostGameContainer.SetActive(false);
        _PreGameContainer.SetActive(true);
        _estrellasTxt.text = "";
        _estrellasTxt.text += "1. " + "Consigue " + _gameManager._starManager._condition1 + " puntos o más" + "\n";
        _estrellasTxt.text += "2. " + "Activa la fiebre del oro" + "\n";
        _estrellasTxt.text += "3. " + "Consigue un combo de " + _gameManager._starManager._condition3 + " o más";

        if (!PlayerPrefs.HasKey("MaxScoreShootingMinigame"))
            PlayerPrefs.SetInt("MaxScoreShootingMinigame",0);

        _gameManager._starManager.ResetStars();

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

        _gameManager._starManager.CheckStar(1);
        _gameManager._starManager.CheckStar(3);
        if (_gameManager._starManager.GetStar(1))
        {
            GameProgress.SetStars(1, 1);
            _estrella1.color = _StarDoneColor;
        }
        else _estrella1.color = _StarNotDoneColor;

        if (_gameManager._starManager.GetStar(2))
        {
            GameProgress.SetStars(1, 2);
            _estrella2.color = _StarDoneColor;
        }
        else _estrella2.color = _StarNotDoneColor;

        if (_gameManager._starManager.GetStar(3))
        {
            GameProgress.SetStars(1,3);
            _estrella3.color = _StarDoneColor;
        }
        else _estrella3.color = _StarNotDoneColor;

        if (_puntuacionActual > PlayerPrefs.GetInt("MaxScoreShootingMinigame"))
            PlayerPrefs.SetInt("MaxScoreShootingMinigame", _puntuacionActual);
        
        _reto1Conseguido.text = "1. 500 puntos o mas: " + _puntuacionActual + "/" + _gameManager._starManager._condition1;
        _reto2Conseguido.text = "2. Activa la fiebre del oro: " + _gameManager._logSystem._GR +"/1";
        _reto3Conseguido.text = "3. Combo de 25 o mas: " + _gameManager._comboCounter._maxCombo + "/" + _gameManager._starManager._condition3;
        
        _puntuacionFinalTxt.text = "Puntuación" + "\n" + _puntuacionActual;
        _puntuacionMaximaTxt.text = "Max Puntuación" + "\n" + PlayerPrefs.GetInt("MaxScoreShootingMinigame");
        _gameManager._logSystem._Score = _puntuacionActual;
        for (int i = 0; i < DianasRestantes.Length; i++) 
        {
           DianasRestantes[i]._dianaContainer.SleepTarget();
           DianasRestantes[i].GetComponentInParent<Animator>().Play("DianaHide_Anim");
        }
        _gameManager._spawnerDianas._activeLetter = false;
        _gameManager._spawnerDianas._currentLetter = 0;
        _gameManager._spawnerDianas._isOnGoldRush = false;
        _playerInput.SwitchCurrentActionMap("UIMap");
        
        //_npcConversationHelper.StartConversation();
        
        _gameManager._logSystem._Combo = _gameManager._comboCounter._maxCombo;
        _gameManager._logSystem.SaveData();
        _gameManager._comboCounter._maxCombo = 0;
        _gameManager._comboCounter._combo = 0;
    }

    public void Pause()
    {
        if(_isCountdown) return;
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
        _mainMenu.SetActive(false);
    }

    public void CloseTutorial()
    {
        _playerInput.SwitchCurrentActionMap("UIMap");
        _mainMenu.SetActive(true);
    }

    public void ExitCurrentGame() 
    {
        Time.timeScale = 1;
        FadeController.Fade("TiroAlBlanco");
    }

    IEnumerator Countdown()
    {
        _isCountdown = true;
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
        _isCountdown = false;
    }
    #endregion Metodos
}
