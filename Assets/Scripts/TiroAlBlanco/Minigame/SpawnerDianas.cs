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

    private bool[] targetsInUse;
    #endregion Variables

    #region Metodos
    public void SpawnInit()
    {
        numDianas = 0;
        targetsInUse = new bool[_spawnPoints.Count];
        SpawnNewTarget(-1);
        SpawnNewTarget(-1);
        SpawnNewTarget(-1);
        _currentLetter = 0;
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
            Spawn(1, i);
            return;
        }

        if (type == 0 && _gameManager._pistolaScript._timeToSpawnLetter <= 0)
        {
            _gameManager._pistolaScript._timeToSpawnLetter = Random.Range(2f, 2f);
            type = -10;
        } 

        if (type == -1)
            Spawn(type, i, true);
        else if (type == -10)
            if (_currentLetter < 6 && !_activeLetter) 
            {
                _activeLetter = true;
                Spawn(_currentLetter + 3, i, true);
            }else
                Spawn(0, i, true);
        else
            Spawn(type, i);
    }

    public void DestroyTarget(int targetPos, Diana d = null) 
    {
        numDianas--;
        Debug.Log("NumDianas:" + numDianas);
        targetsInUse[targetPos] = false;
        if (d != null)
        {
            d.GetComponentInParent<Animator>().SetBool("isActive",false);
            d._dianaContainer.SleepTarget();
        }
    }

    private void Spawn(int type, int posInArray, bool first = false)
    {
        targetsInUse[posInArray] = true;
        _possibleTargetContainers[posInArray].GetComponent<Animator>().SetBool("isActive", true);
        _possibleTargetContainers[posInArray].WakeUpTarget(type, posInArray);
    }
    #endregion Metodos
}
