using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : FloatingObject
{
    public enum Type { NORMAL, PLAYER, AI, BLACK, GOLD, BIG }
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material[] _materials;
    public Type type;

    public DucksGameManager _gameManager;

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
                }
                else if (!player) _gameManager.aiScore++;
                _gameManager._vfxManager.InstantiateVFX(0, transform.position);
                break;
            case Type.BLACK:
                if (player) 
                {
                    _gameManager.playerScore = Mathf.Max(_gameManager.playerScore - 2, 0);
                    _gameManager.SetLastDuck(0);
                }
                else if (!player) _gameManager.aiScore = Mathf.Max(_gameManager.aiScore - 2, 0);
                _gameManager._vfxManager.InstantiateVFX(1, transform.position);
                break;
            case Type.GOLD:
                if (player) 
                {
                    _gameManager.playerScore += 5;
                    _gameManager.SetLastDuck(1);
                }
                else if (!player) _gameManager.aiScore += 5;
                _gameManager._vfxManager.InstantiateVFX(2, transform.position);
                break;
            case Type.BIG:
                if (player) 
                {
                    _gameManager.playerScore += 7;
                    _gameManager.SetLastDuck(1);
                }
                else if (!player) _gameManager.aiScore += 7;
                _gameManager._vfxManager.InstantiateVFX(3, transform.position);
                break;
            case Type.PLAYER:
                _gameManager.playerScore += 2;
                if (player)
                {
                    _gameManager.SetLastDuck(0.75f);
                }
                _gameManager._vfxManager.InstantiateVFX(4, transform.position);
                break;
            case Type.AI:
                _gameManager.aiScore += 2;
                if (player)
                {
                    _gameManager.SetLastDuck(0.25f);
                }
                _gameManager._vfxManager.InstantiateVFX(4, transform.position);
                break;
        }

        if (player && (type == Type.BLACK || type == Type.AI))
        {
            _gameManager.noBadDucks = false;
        }
    }
}
