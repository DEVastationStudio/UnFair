using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDianas : MonoBehaviour
{
    #region Variables
    [SerializeField] private List<GameObject> _spawnPoints;
    [SerializeField] private List<GameObject> _possibleTargets;

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
        Debug.Log((type == 1)?"Spawneando una diana dorada":"Spawneando una diana normal");
        int i = Random.Range(0, _spawnPoints.Count - 1);
        while (targetsInUse[i] && _spawnPoints.Count > numDianas)
        {
            if (i + 1 >= _spawnPoints.Count)
                i = 0;
            else
                i++;
        }
        if (targetsInUse[i])
            return;
        switch (type) 
        {
            case 0:
                Spawn(_spawnPoints[i].transform.position, _possibleTargets[0], i);
                break;
            case 1:
                Spawn(_spawnPoints[i].transform.position, _possibleTargets[1], i);
                break;
            case 2:
                Spawn(_spawnPoints[i].transform.position, _possibleTargets[2], i);
                break;
            case 3:
                Spawn(_spawnPoints[i].transform.position, _possibleTargets[0], i, true);
                break;
        }
    }

    public void DestroyTarget(int targetPos) 
    {
        numDianas--;
        targetsInUse[targetPos] = false;
    }

    private void Spawn(Vector3 pos, GameObject target, int posInArray, bool first = false)
    {
        targetsInUse[posInArray] = true;
        Diana aux = Instantiate(target, pos, Quaternion.identity).GetComponent<Diana>();
        aux._pos = posInArray;
        if (first)
            aux._first = true;
        aux.transform.localEulerAngles += new Vector3(0, -180, 0);
        aux.StartDiana();
        numDianas++;
    }
    #endregion Metodos
}
