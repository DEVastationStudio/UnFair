using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetrasUnfairManager : MonoBehaviour
{
    public List<GameObject> Letras;
    public List<GameObject> Guiones;
    [SerializeField] private Color _goldColor;
    [SerializeField] private Color _whiteColor;
    [SerializeField] private ShootingMinigameManager _gameManager;
    [SerializeField] private Slider _barraLetras;

    [Header("Luces")]
    [SerializeField] private int _numOfBlinks;
    [SerializeField] private float _blinkSpeed;
    [SerializeField] private Sprite _lightOff;
    [SerializeField] private Sprite _lightWaiting;
    [SerializeField] private Sprite _lightCorrect;
    [SerializeField] private Sprite _lightWin; 
    [SerializeField] private Sprite _lightWrong;
    public List<GameObject> LucesU;
    public List<GameObject> LucesN;
    public List<GameObject> LucesF;
    public List<GameObject> LucesA;
    public List<GameObject> LucesI;
    public List<GameObject> LucesR;

    public void ShowLetter(int i) 
    {
        Letras[i].SetActive(true);
        Guiones[i].SetActive(false);
    }

    public void HideLetter(int i)
    {
        //if (i < 0) return;
        Letras[i].SetActive(false);
        Guiones[i].SetActive(true);
    }

    public void CorrectLetterShoot()
    {
        _gameManager._letrasManager.ShowLetter(_gameManager._spawnerDianas._currentLetter);
        ChangeLightColor(_lightCorrect, _gameManager._spawnerDianas._currentLetter);
        _gameManager._spawnerDianas._currentLetter = Mathf.Clamp(_gameManager._spawnerDianas._currentLetter + 1, 0, 6);
        if(_gameManager._spawnerDianas._currentLetter<6)
            ChangeLightColor(_lightWaiting, _gameManager._spawnerDianas._currentLetter);
        if (_gameManager._spawnerDianas._currentLetter == 6) 
        {
            for (int i = 0; i < 6; i++)
                ChangeLightColor(_lightWin, i);
            _gameManager._letrasManager.GoldRush();
        }
    }

    public void ResetWord(bool startOfGame = false)
    {
        if (_gameManager._spawnerDianas._isOnGoldRush) return;
        for (int i = _gameManager._spawnerDianas._currentLetter; i >= 0; i--)
        {
            _gameManager._spawnerDianas._currentLetter = Mathf.Clamp(_gameManager._spawnerDianas._currentLetter - 1, 0, 6);
            _gameManager._letrasManager.HideLetter(_gameManager._spawnerDianas._currentLetter);
        }
        if (!startOfGame)
        {
            StartCoroutine(WrongLetterLights());
        }
        else 
        {
            for (int i = 1; i < 6; i++)
                ChangeLightColor(_lightOff, i);
            ChangeLightColor(_lightWaiting, 0);
        }
    }

    public void GoldRush() 
    {
        StartCoroutine(WinningUnfair());
    }

    IEnumerator WinningUnfair()
    {
        _gameManager._logSystem._GR++;
        foreach (GameObject g in Letras) 
        {
            SpriteRenderer s = g.GetComponent<SpriteRenderer>();
            Color c = s.color;
            c = _goldColor;
            s.color = c;
        }
        _gameManager._spawnerDianas._isOnGoldRush = true;
        _gameManager._spawnerDianas._activeLetter = true;
        _barraLetras.value = 100;
        _barraLetras.gameObject.SetActive(true);
        float tBase = 0.01f;
        float tMax = 100f;
        float tTotal = 4;
        _gameManager._logSystem._GRTime = tTotal;
        while (_barraLetras.value > 0) 
        {
            yield return new WaitForSeconds(tBase);
            _barraLetras.value -= Mathf.Clamp((tMax/tTotal)*tBase, 0, 100);
        }
        _barraLetras.gameObject.SetActive(false);
        _barraLetras.value = 100;
        _gameManager._spawnerDianas._isOnGoldRush = false;
        _gameManager._spawnerDianas._activeLetter = false;
        int count = 0;
        foreach (GameObject g in Letras)
        {
            SpriteRenderer s = g.GetComponent<SpriteRenderer>();
            Color c = s.color;
            c = _whiteColor;
            s.color = c;
            if (count < 0) count = 0;
            HideLetter(count);
            count++;
        }
        _gameManager._spawnerDianas._currentLetter = 0;
        for (int i = 1; i < 6; i++)
            ChangeLightColor(_lightOff, i);
        ChangeLightColor(_lightWaiting, 0);
    }

    private void ChangeLightColor(Sprite sp, int l) 
    {
        List<GameObject> inUse;
        switch (l) 
        {
            case 0:
                inUse = LucesU;
                break;
            case 1:
                inUse = LucesN;
                break;
            case 2:
                inUse = LucesF;
                break;
            case 3:
                inUse = LucesA;
                break;
            case 4:
                inUse = LucesI;
                break;
            case 5:
                inUse = LucesR;
                break;
            default:
                inUse = new List<GameObject>();
                break;
        }

        foreach (GameObject g in inUse)
        {
            g.GetComponent<SpriteRenderer>().sprite = sp;
        }
    }

    IEnumerator WrongLetterLights() 
    {
        for (int i = 0; i < 6; i++)
            ChangeLightColor(_lightWrong, i);
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 6; i++)
            ChangeLightColor(_lightOff, i);
        yield return new WaitForSeconds(0.2f);
        ChangeLightColor(_lightWaiting, 0);
    }

    IEnumerator BlinkingLights(int letterToBlink, Sprite blinkingColor)
    {
        ChangeLightColor(blinkingColor, letterToBlink);
        for (int i = 0; i < _numOfBlinks; i++)
        {
            ChangeLightColor(_lightOff, letterToBlink);
            yield return new WaitForSeconds(_blinkSpeed);
            ChangeLightColor(blinkingColor, letterToBlink);
        }
    }

}
