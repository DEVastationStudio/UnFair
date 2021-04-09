using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana : MonoBehaviour
{
    public int _points;
    public int _pos;
    public bool _hit;
    public bool _first;
    private ShootingMinigameManager _gameManager;
    private bool isInit;
    private float _lifeTime;
    private float _maxLifeTime;

    public void StartDiana()
    {
        _gameManager = FindObjectOfType<ShootingMinigameManager>();
        if (transform.tag == "DianaDorada" || transform.tag == "Reloj")
            _lifeTime = (0.5f + 2*(1.0f-_gameManager._dynamicDifficultyManager.GetValue(0)));
        else if (_first)
            _lifeTime = (3f + 2.5f + 2*(1.0f - _gameManager._dynamicDifficultyManager.GetValue(0)));
        else
            _lifeTime = (2.5f + 2*(1.0f - _gameManager._dynamicDifficultyManager.GetValue(0)));
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
            if (!_hit && _lifeTime<= 0)
            {
                _hit = true;
                _gameManager._spawnerDianas.DestroyTarget(gameObject.GetComponent<Diana>()._pos);
                _gameManager._uiGeneral.IncreasePuntuacion(gameObject.GetComponent<Diana>()._points);
                if (transform.tag == "DianaDorada" || transform.tag == "Reloj")
                    _gameManager._pistolaScript.AutomaticDespawn();
                else
                    _gameManager._pistolaScript.CallSpawnRetard(0);
                _gameManager._dynamicDifficultyManager.SetValue(0, 0.25f);
                Destroy(gameObject);
            }
        }
    }
}
