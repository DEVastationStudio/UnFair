using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana : MonoBehaviour
{
    #region Variables
    public int _points;
    public int _pos;
    public bool _hit;
    public bool _first;
    public int _numLetraDiana;
    public DianaContainer _dianaContainer;

    private ShootingMinigameManager _gameManager;
    [SerializeField] private bool isInit;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _maxLifeTime;
    #endregion

    #region Metodos
    public void StartDiana()
    {
        _hit = false;
        _gameManager = FindObjectOfType<ShootingMinigameManager>();
        if (transform.tag == "DianaDorada" || transform.tag == "Reloj")
            _lifeTime = (0.5f + 2 * (1.0f - _gameManager._dynamicDifficultyManager.GetValue(0)));
        if(_gameManager._spawnerDianas._isOnGoldRush)
            _lifeTime = (2.5f + 2 * (1.0f - _gameManager._dynamicDifficultyManager.GetValue(0)));
        else if (_first)
            _lifeTime = (3f + 2.5f + 2 * (1.0f - _gameManager._dynamicDifficultyManager.GetValue(0)));
        else
            _lifeTime = (2.5f + 2 * (1.0f - _gameManager._dynamicDifficultyManager.GetValue(0)));
        _maxLifeTime = _lifeTime;
        isInit = true;
    }

    public float GetPercentageLifeTime()
    {
        if (_lifeTime < 0) _lifeTime = 0;
        return (_lifeTime / _maxLifeTime);
    }

    public void Hit(bool isHit, Vector3 point)
    {
        string tag = transform.tag;
        _gameManager._spawnerDianas.DestroyTarget(_pos, this);
        switch (tag) 
        {
            case "Diana":
                _gameManager._pistolaScript.AutomaticDespawn();
                if (isHit) 
                {
                    _gameManager._dynamicDifficultyManager.SetValue(0, 0.50f);
                    _gameManager._uiGeneral.IncreasePuntuacion(_points);
                    _gameManager._vfxManager.InstantiateVFX(0, point);
                }
                break;
            case "DianaDorada":
                _gameManager._pistolaScript.CallSpawnRetard(0);
                if (isHit)
                {
                    if(!_gameManager._spawnerDianas._isOnGoldRush)
                     _gameManager._dynamicDifficultyManager.SetValue(0, 0.9f);
                    _gameManager._uiGeneral.IncreasePuntuacion(_points);
                    _gameManager._vfxManager.InstantiateVFX(1, point);
                }
                break;
            case "Reloj":
                _gameManager._pistolaScript.CallSpawnRetard(0);
                if (isHit)
                {
                    _gameManager._dynamicDifficultyManager.SetValue(0, 1f);
                    _gameManager._uiGeneral.IncreasePuntuacion(_points);
                    _gameManager._uiGeneral.AddTime(_points);
                    _gameManager._vfxManager.InstantiateVFX(2, point);
                }
                break;
            case "DianaConLetra":
                _gameManager._spawnerDianas._activeLetter = false;
                _gameManager._pistolaScript.CallSpawnRetard(0);
                if (isHit)
                {
                    Debug.Log("Letra " + _numLetraDiana);
                    _gameManager._letrasManager.ShowLetter(_gameManager._spawnerDianas._currentLetter);
                    Mathf.Clamp(_gameManager._spawnerDianas._currentLetter++, 0, 6);
                    if (_gameManager._spawnerDianas._currentLetter == 6)
                        _gameManager._letrasManager.GoldRush();
                }
                else
                {
                    Mathf.Clamp(_gameManager._spawnerDianas._currentLetter--, 0, 6);
                    _gameManager._letrasManager.HideLetter(_gameManager._spawnerDianas._currentLetter);
                }
                break;
        }
    }

    public void Update()
    {
        if (isInit || _lifeTime < 0)
        {
            _lifeTime -= Time.deltaTime;
            if (!_hit && _lifeTime <= 0)
            {
                Hit(false, new Vector3(0,0,0));
                isInit = false;
                _lifeTime = 0;
            }
        }
    }
    #endregion
}
