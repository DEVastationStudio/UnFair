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
    public int _activeLetter;
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
        if (transform.CompareTag("DianaDorada") || transform.CompareTag("Reloj"))
            _lifeTime = Mathf.Clamp((0.5f 
                                     + 1.5f 
                                     * (1.0f - _gameManager._dynamicDifficultyManager.GetValue(0))
                                     -_gameManager._comboCounter._timeToTakeAwayFromTargets
                                     ),1f,3);
        if(_gameManager._spawnerDianas._isOnGoldRush)
            _lifeTime = Mathf.Clamp((2.5f 
                                     + 1.5f 
                                     * (1.0f - _gameManager._dynamicDifficultyManager.GetValue(0))
                                     - _gameManager._comboCounter._timeToTakeAwayFromTargets
                                     ),1.5f,5);
        else if (_first)
            _lifeTime = (3f + 2.5f + 2 * (1.0f - _gameManager._dynamicDifficultyManager.GetValue(0)));
        else
            _lifeTime = Mathf.Clamp((2.5f 
                                     + 1.5f 
                                     * (1.0f - _gameManager._dynamicDifficultyManager.GetValue(0))
                                     -_gameManager._comboCounter._timeToTakeAwayFromTargets
                                     ),1.5f,5);
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
        if (isHit)
        {
            if(point.x >= transform.position.x)
                this.GetComponentInParent<Animator>().Play("Diana_Hit_AntiClockwise");
            else
                this.GetComponentInParent<Animator>().Play("Diana_Hit_Clockwise");
        }
        else
            this.GetComponentInParent<Animator>().Play("DianaHide_Anim");
        _gameManager._spawnerDianas.DestroyTarget(_pos, this);
        switch (tag) 
        {
            case "Diana":
                _gameManager._pistolaScript.AutomaticDespawn();
                if (isHit)
                {
                    _gameManager._comboCounter.HitCombo();
                    _gameManager._logSystem._DNDisp++;
                    _gameManager._dynamicDifficultyManager.SetValue(0, 0.50f);
                    _gameManager._uiGeneral.IncreasePuntuacion(_points);
                    _gameManager._vfxManager.InstantiateVFX(0, point);
                }
                break;
            case "DianaDorada":
                _gameManager._pistolaScript.CallSpawnRetard(0);
                if (isHit)
                {
                    _gameManager._comboCounter.HitCombo();
                    _gameManager._dynamicDifficultyManager.SetValue(0, 0.9f);
                    _gameManager._logSystem._DDDisp++;
                    _gameManager._uiGeneral.IncreasePuntuacion(_points);
                    _gameManager._vfxManager.InstantiateVFX(1, point);
                }
                break;
            case "Reloj":
                _gameManager._pistolaScript.CallSpawnRetard(0);
                if (isHit)
                {
                    _gameManager._comboCounter.HitCombo();
                    _gameManager._logSystem._DRDisp++;
                    _gameManager._logSystem._TP += _points;
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
                    if (_activeLetter == _gameManager._spawnerDianas._currentLetter)
                    {
                        _gameManager._comboCounter.HitCombo();
                        _gameManager._logSystem._DLDispT++;
                        _gameManager._pistolaScript._timeToSpawnLetter = 2f;
                        _gameManager._vfxManager.InstantiateTrailFromAToB(transform, _gameManager._letrasManager.Letras[_gameManager._spawnerDianas._currentLetter].transform, 0);
                    }
                    else
                    {
                        _gameManager._comboCounter.MissCombo();
                        _gameManager._logSystem._DLDispF++;
                        _gameManager._letrasManager.ResetWord();
                    }
                }
                break;
            case "DianaDoradaGR":
                _gameManager._pistolaScript.CallSpawnRetard(0);
                if (isHit)
                {
                    _gameManager._comboCounter.HitCombo();
                    _gameManager._dynamicDifficultyManager.SetValue(0, 0.7f);
                    _gameManager._logSystem._DGRDisp++;
                    _gameManager._uiGeneral.IncreasePuntuacion(_points);
                    _gameManager._vfxManager.InstantiateVFX(1, point);
                }
                break;
        }
    }

    public void Update()
    {
        if (isInit || _lifeTime < 0)
        {
            _lifeTime -= Time.deltaTime;
            if (_gameManager._spawnerDianas._isOnGoldRush && transform.tag != "DianaDoradaGR")
                _lifeTime = -1;
            if(!_gameManager._spawnerDianas._isOnGoldRush && transform.tag == "DianaDoradaGR")
                _lifeTime = -1;
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
