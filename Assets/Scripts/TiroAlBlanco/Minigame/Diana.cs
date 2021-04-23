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
    public DianaContainer _dianaContainer;

    private ShootingMinigameManager _gameManager;
    private bool isInit;
    private float _lifeTime;
    private float _maxLifeTime;
    #endregion

    #region Metodos
    public void StartDiana()
    {
        _gameManager = FindObjectOfType<ShootingMinigameManager>();
        if (transform.tag == "DianaDorada" || transform.tag == "Reloj")
            _lifeTime = (0.5f + 2 * (1.0f - _gameManager._dynamicDifficultyManager.GetValue(0)));
        else if (_first)
            _lifeTime = (3f + 2.5f + 2 * (1.0f - _gameManager._dynamicDifficultyManager.GetValue(0)));
        else
            _lifeTime = (2.5f + 2 * (1.0f - _gameManager._dynamicDifficultyManager.GetValue(0)));
        _maxLifeTime = _lifeTime;
        isInit = true;
    }

    public float GetPercentageLifeTime()
    {
        return (_lifeTime / _maxLifeTime);
    }

    public void Update()
    {
        if (isInit)
        {
            _lifeTime -= Time.deltaTime;
            if (!_hit && _lifeTime <= 0)
            {
                _hit = true;
                _gameManager._spawnerDianas.DestroyTarget(_pos);
                if (transform.tag == "DianaDorada" || transform.tag == "Reloj")
                    _dianaContainer.SleepTarget(true);
                else
                    _dianaContainer.SleepTarget(false);
                _gameManager._dynamicDifficultyManager.SetValue(0, 0.25f);
                isInit = false;
                _hit = false;
            }
        }
    }
    #endregion
}
