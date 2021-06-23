using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDianas : MonoBehaviour
{
    #region Variables
    [SerializeField] private List<GameObject> _spawnPoints;
    [SerializeField] private List<GameObject> _possibleTargets;
    [SerializeField] private List<DianaContainer> _possibleTargetContainers;
    [SerializeField] private ShootingMinigameManager _gameManager;
    [SerializeField] private int _maxNumDianas;

    [HideInInspector] public int numDianas;
    [HideInInspector] public bool _activeLetter;
    [HideInInspector] public int _currentLetter;
    [HideInInspector] public bool _isOnGoldRush;

    private const int MAX_LETTER_COUNT = 2;
    private int _countToCheatLetter;

    private bool[] targetsInUse;
    #endregion Variables

    #region Metodos
    public void SpawnInit()
    {
        numDianas = 0;
        _currentLetter = 0;
        _countToCheatLetter = MAX_LETTER_COUNT;
        targetsInUse = new bool[_spawnPoints.Count];
        SpawnNewTarget(0);
        SpawnNewTarget(0);
        SpawnNewTarget(0);
    }

    public void SpawnNewTarget(int type) 
    {
        if (numDianas >= _maxNumDianas) return;
        numDianas++;
        int i = Random.Range(0, _possibleTargetContainers.Count - 1);
        while (targetsInUse[i] && _possibleTargetContainers.Count > numDianas)
        {
            if (i + 1 >= _possibleTargetContainers.Count)
                i = 0;
            else
                i++;
        }

        if (targetsInUse[i])
            return;

        if (_isOnGoldRush) 
        {
            Spawn(9, i);
            return;
        }

        if (type == 0 && _gameManager._pistolaScript._timeToSpawnLetter <= 0)
        {
            type = -10;
        }

        if (type == -1)
            Spawn(type, i, true);
        else if (type == -10)
            if (_currentLetter < 6 && !_activeLetter && !_gameManager._spawnerDianas._isOnGoldRush) 
            {
                _activeLetter = true;
                int aux = Random.Range(_currentLetter, Mathf.Clamp(_currentLetter+2, _currentLetter, 5));
                Debug.Log("RangeMin: "+ _currentLetter + " -- RangeMax: "+ Mathf.Clamp(_currentLetter + 2, _currentLetter, 5) + " -- Aux: " + aux);
                if (_countToCheatLetter > 0)
                {
                    if (_currentLetter == aux)
                    {
                        _countToCheatLetter = MAX_LETTER_COUNT;
                        Spawn(_currentLetter + 3, i, true, _currentLetter);
                        _gameManager._logSystem._DL++;
                    }
                    else
                    {
                        _countToCheatLetter--;
                        Spawn(aux + 3, i, true, aux);
                        _gameManager._logSystem._DL++;
                    }
                }
                else 
                {
                    _countToCheatLetter = MAX_LETTER_COUNT;
                    Spawn(_currentLetter + 3, i, true, _currentLetter);
                    _gameManager._logSystem._DR++;
                }
            }else
                Spawn(0, i, true);
        else
            Spawn(type, i);
    }

    public void DestroyTarget(int targetPos, Diana d = null) 
    {
        numDianas--;
        if (d != null)
        {
            d.GetComponentInParent<Animator>().SetBool("isActive",false);
            d._dianaContainer.SleepTarget();
            StartCoroutine(WaitAnimation(targetPos));
        }
    }

    IEnumerator WaitAnimation(int targetPos) 
    {
        yield return new WaitForSeconds(1);
        targetsInUse[targetPos] = false;
    }

    private void Spawn(int type, int posInArray, bool first = false, int letter = -1)
    {
        switch (type) 
        {
            case 0:
                _gameManager._logSystem._DN++;
                break;
            case 1:
                _gameManager._logSystem._DD++;
                break;
            case 2:
                _gameManager._logSystem._DR++;
                break;
            case 9:
                _gameManager._logSystem._DGR++;
                break;
        }
        targetsInUse[posInArray] = true;
        _possibleTargetContainers[posInArray].GetComponent<Animator>().SetBool("isActive", true);
        _possibleTargetContainers[posInArray].WakeUpTarget(type, posInArray, letter);
    }
    #endregion Metodos
}
