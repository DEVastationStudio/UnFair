using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDianas : MonoBehaviour
{
    #region Variables
    [SerializeField] private List<GameObject> _spawnPoints;
    [SerializeField] private List<GameObject> _possibleTargets;
    [SerializeField] private List<DianaContainer> _possibleTargetContainers;
    [SerializeField] private int _maxNumDianas;

    [HideInInspector] public int numDianas;

    private bool[] targetsInUse;
    #endregion Variables

    #region Metodos
    public void SpawnInit()
    {
        numDianas = 0;
        targetsInUse = new bool[_spawnPoints.Count];
        SpawnNewTarget(3);
        SpawnNewTarget(3);
        SpawnNewTarget(3);
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

        if(type == 3)
            Spawn(type, i, true);
        else
            Spawn(type, i);
    }

    public void DestroyTarget(int targetPos, Diana d = null) 
    {
        numDianas--;
        Debug.Log("NumDianas:" + numDianas);
        targetsInUse[targetPos] = false;
        if(d != null)
            d._dianaContainer.SleepTarget();
    }

    private void Spawn(int type, int posInArray, bool first = false)
    {
        targetsInUse[posInArray] = true;
        _possibleTargetContainers[posInArray].WakeUpTarget(type, posInArray);
    }
    #endregion Metodos
}
