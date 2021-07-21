using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboCounter : MonoBehaviour
{
    [SerializeField] private int _MaxFont;
    [SerializeField] private int _MinFont;
    [SerializeField] private float _decayVel;
    [SerializeField] private int _decayStep;
    [SerializeField] private float _takeawayTimeStep;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private ShootingMinigameManager _gameManager;

    [HideInInspector] public int _maxCombo = 0;
    [HideInInspector] public int _combo = 0;
    [HideInInspector] public float _timeToTakeAwayFromTargets = 0;

    private float _timePassed = 0;

    public void HitCombo()
    {
        _combo++;
        _timeToTakeAwayFromTargets += _takeawayTimeStep;
        if (_combo > _maxCombo)
            _maxCombo = _combo;
        _text.text = "X" + _combo;
        _text.fontSize = _MaxFont;
    }
    public void MissCombo()
    {
        _combo = 0;
        _text.fontSize = 0;
        _timeToTakeAwayFromTargets = 0;
    }

    private void Update()
    {
        if (_text.fontSize < _MinFont)
        {
            _text.fontSize = 0;
            _combo = 0;
        }
        else 
        {
            _timePassed += Time.deltaTime;
            if (_timePassed > _decayVel) 
            {
                _timePassed = 0;
                _text.fontSize -= _decayStep;
            }
        }

    }
}
