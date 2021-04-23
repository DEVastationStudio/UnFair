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
        Debug.Log((type == 1)?"Spawneando una diana dorada":"Spawneando una diana normal");
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
        switch (type) 
        {
            case 0:
            case 1:
            case 2:
                Spawn(/*_spawnPoints[i].transform.position, _possibleTargets[0],*/type, i);
                break;
            case 3:
                Spawn(/*_spawnPoints[i].transform.position, _possibleTargets[0],*/type, i, true);
                break;
        }
    }

    public void DestroyTarget(int targetPos, bool action = false, Diana d = null) 
    {
        numDianas--;
        Debug.Log("NumDianas:" + numDianas);
        targetsInUse[targetPos] = false;
        if(d != null)
            d._dianaContainer.SleepTarget(action);
    }

    private void Spawn(/*Vector3 pos, GameObject target,*/int type, int posInArray, bool first = false)
    {
        targetsInUse[posInArray] = true;
        //Diana aux = Instantiate(target, pos, Quaternion.identity).GetComponent<Diana>();
        //aux._pos = posInArray;
        //aux.transform.localEulerAngles += new Vector3(0, -180, 0);
        //aux.StartDiana();
        _possibleTargetContainers[posInArray].WakeUpTarget(type, posInArray);
    }
    #endregion Metodos
}
