using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : FloatingObject
{
    public enum Type { NORMAL, PLAYER, AI, BLACK, GOLD }
    [SerializeField] private Renderer _renderer;
    public Type type;

    public DucksGameManager _gameManager;

    protected override void Start()
    {
        base.Start();
        Material _material = _renderer.material;
        switch (type)
        {
            case Type.NORMAL:
                _material.color = Color.white;
                break;
            case Type.PLAYER:
                _material.color = Color.green;
                break;
            case Type.AI:
                _material.color = Color.red;
                break;
            case Type.BLACK:
                _material.color = Color.black;
                break;
            case Type.GOLD:
                _material.color = Color.yellow;
                break;
        }
        _renderer.material = _material;
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
            rigidBody.AddForce(pos/4, ForceMode.Force);
            //Debug.DrawRay(transform.position, Vector3.Cross(transform.position, Vector3.up));
        }
    }

    protected override void OnBasketEnter(bool player)
    {
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
                break;
            case Type.BLACK:
                if (player) 
                {
                    _gameManager.playerScore = Mathf.Max(_gameManager.playerScore - 2, 0);
                    _gameManager.SetLastDuck(0);
                }
                else if (!player) _gameManager.aiScore = Mathf.Max(_gameManager.aiScore - 2, 0);
                break;
            case Type.GOLD:
                if (player) 
                {
                    _gameManager.playerScore += 5;
                    _gameManager.SetLastDuck(1);
                }
                else if (!player) _gameManager.aiScore += 5;
                break;
            case Type.PLAYER:
                _gameManager.playerScore += 2;
                if (player)
                {
                    _gameManager.SetLastDuck(0.75f);
                }
                break;
            case Type.AI:
                _gameManager.aiScore += 2;
                if (player)
                {
                    _gameManager.SetLastDuck(0.25f);
                }
                break;
        }

        if (player && (type == Type.BLACK || type == Type.AI))
        {
            _gameManager.noBadDucks = false;
        }
    }
}
