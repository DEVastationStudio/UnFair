using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : FloatingObject
{
    public enum Type { NORMAL, PLAYER, AI, BLACK, GOLD, BIG, TIME }
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material[] _materials;
    public Type type;

    public DucksGameManager _gameManager;

    [SerializeField] private AudioSource _goodDuckSfx, _badDuckSfx, _timeDuckSfx, _boostSfx;

    protected override void Start()
    {
        base.Start();
        
        switch (type)
        {
            case Type.NORMAL:
                _renderer.material = _materials[0];
                break;
            case Type.PLAYER:
                _renderer.material = _materials[1];
                break;
            case Type.AI:
                _renderer.material = _materials[2];
                break;
            case Type.BLACK:
                _renderer.material = _materials[3];
                break;
            case Type.GOLD:
                _renderer.material = _materials[4];
                break;
            case Type.BIG:
                _renderer.material = _materials[4];
                transform.localScale = new Vector3(60,60,60);
                break;
            case Type.TIME:
                _renderer.material = _materials[5];
                break;
        }
    }
    protected override void FixedUpdate() {
        
        base.FixedUpdate();
            
        if (_gameManager.gameStarted && _inWater && transform.position.y < _waterHeight)
        {
            //Spin force
            Vector3 cross = Vector3.Cross(transform.position, Vector3.up);
            cross.y = 0;
            Vector3 pos = -transform.position;
            pos.y = 0;
            rigidBody.AddForce(cross/4, ForceMode.Force);
            rigidBody.AddForce(pos/12, ForceMode.Force);
            //Debug.DrawRay(transform.position, Vector3.Cross(transform.position, Vector3.up));
            //Debug.DrawRay(transform.position, cross/4 + pos/12, Color.red);
        }
    }

    protected override void OnBasketEnter(bool player)
    {
        if (_gameManager.gameOver) return;
        gameObject.layer = 0;
        switch (type)
        {
            case Type.NORMAL:
                if (player) 
                {
                    _gameManager.playerScore++;
                    _gameManager.SetLastDuck(0.5f);
                    _gameManager.logSystem._PD1++;
                }
                else if (!player) {
                    _gameManager.aiScore++;
                    _gameManager.logSystem._ED1++;
                }
                _goodDuckSfx.Play();
                _gameManager._vfxManager.InstantiateVFX(0, transform.position);
                break;
            case Type.BLACK:
                if (player) 
                {
                    _gameManager.playerScore = Mathf.Max(_gameManager.playerScore - 2, 0);
                    _gameManager.SetLastDuck(0);
                    _gameManager.logSystem._PD0++;
                }
                else if (!player) {
                    _gameManager.aiScore = Mathf.Max(_gameManager.aiScore - 2, 0);
                    _gameManager.logSystem._ED0++;
                }
                _badDuckSfx.Play();
                _gameManager._vfxManager.InstantiateVFX(1, transform.position);
                break;
            case Type.GOLD:
                if (player) 
                {
                    _gameManager.playerScore += 5;
                    _gameManager.SetLastDuck(1);
                    _gameManager.logSystem._PD4++;
                }
                else if (!player) {
                    _gameManager.aiScore += 5;
                    _gameManager.logSystem._ED4++;
                }
                _goodDuckSfx.Play();
                _gameManager._vfxManager.InstantiateVFX(2, transform.position);
                break;
            case Type.BIG:
                if (player) 
                {
                    _gameManager.playerScore += 7;
                    _gameManager.SetLastDuck(1);
                    _gameManager.logSystem._PD5++;
                }
                else if (!player) {
                    _gameManager.aiScore += 7;
                    _gameManager.logSystem._ED5++;
                }
                _goodDuckSfx.Play();
                _gameManager._vfxManager.InstantiateVFX(3, transform.position);
                break;
            case Type.PLAYER:
                /*_gameManager.playerScore += 2;
                if (player)
                {
                    _gameManager.SetLastDuck(0.75f);
                }*/
                if (player) 
                {
                    _gameManager.playerScore += 2;
                    _gameManager.SetLastDuck(0.75f);
                    _gameManager.logSystem._PD2++;
                }
                else if (!player) {
                    _gameManager.aiScore += 2;
                    _gameManager.logSystem._ED2++;
                }

                _goodDuckSfx.Play();
                _gameManager._vfxManager.InstantiateVFX(4, transform.position);
                break;
            case Type.AI:
                _gameManager.aiScore += 2;
                if (player)
                {
                    _gameManager.SetLastDuck(0.25f);
                }
                _goodDuckSfx.Play();
                _gameManager._vfxManager.InstantiateVFX(4, transform.position);
                break;
            case Type.TIME:
                _gameManager.IncreaseTimer();
                _timeDuckSfx.Play();
                _gameManager._vfxManager.InstantiateVFX(5, transform.position);
                if (player) 
                {
                    _gameManager.logSystem._PD3++;
                }
                else {
                    _gameManager.logSystem._ED3++;
                }
                break;
        }
        if (player)
            _gameManager.logSystem._PD++;
        else
            _gameManager.logSystem._ED++;

        if (player && type == Type.BIG)
        {
            _gameManager.caughtBigBoy = true;
            Debug.Log("Supreme Duck Caught");
        }

        if (player && type == Type.GOLD && _gameManager.IsBoosted())
        {
            _gameManager.boostedCaught = true;
            Debug.Log("Golden duck under boost caught");
        }
    }

    protected override void OnTrashEnter()
    {
        if (_gameManager.gameOver) return;
        gameObject.layer = 0;

        int boostTime = 0;
        switch (type)
        {
            case Type.NORMAL:
                boostTime = 1;
                _boostSfx.Play();
                _gameManager._vfxManager.InstantiateVFX(6, transform.position);
                _gameManager.logSystem._BD1++;
                break;
            case Type.BLACK:
                boostTime = -2;
                _gameManager.logSystem._BD0++;
                _badDuckSfx.Play();
                break;
            case Type.GOLD:
                boostTime = 5;
                _boostSfx.Play();
                _gameManager._vfxManager.InstantiateVFX(8, transform.position);
                _gameManager.logSystem._BD4++;
                break;
            case Type.BIG:
                boostTime = 7;
                _boostSfx.Play();
                _gameManager._vfxManager.InstantiateVFX(9, transform.position);
                _gameManager.logSystem._BD5++;
                break;
            case Type.PLAYER:
                boostTime = 2;
                _boostSfx.Play();
                _gameManager._vfxManager.InstantiateVFX(7, transform.position);
                _gameManager.logSystem._BD2++;
                break;
            case Type.AI:
                boostTime = 2;
                _boostSfx.Play();
                _gameManager._vfxManager.InstantiateVFX(7, transform.position);
                break;
            case Type.TIME:
                boostTime = 5;
                _boostSfx.Play();
                _gameManager._vfxManager.InstantiateVFX(8, transform.position);
                _gameManager.logSystem._BD3++;
                break;
        }

        _gameManager.BoostPlayer(boostTime);
        _gameManager.logSystem._BD++;

    }
}
