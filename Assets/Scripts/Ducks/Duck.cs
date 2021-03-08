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

    protected override void OnBasketEnter(bool player)
    {
        switch (type)
        {
            case Type.NORMAL:
                if (player) _gameManager.playerScore++;
                else if (!player) _gameManager.aiScore++;
                break;
            case Type.BLACK:
                if (player) _gameManager.playerScore = Mathf.Max(_gameManager.playerScore - 2, 0);
                else if (!player) _gameManager.aiScore = Mathf.Max(_gameManager.aiScore - 2, 0);
                break;
            case Type.GOLD:
                if (player) _gameManager.playerScore += 5;
                else if (!player) _gameManager.aiScore += 5;
                break;
            case Type.PLAYER:
                _gameManager.playerScore += 2;
                break;
            case Type.AI:
                _gameManager.aiScore += 2;
                break;
        }
    }
}
