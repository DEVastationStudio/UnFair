using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorsesSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<GameObject> enemyHorses;
    [SerializeField] private GameObject playerHorse;
    //private List<Transform> auxSpawns;
    void Start()
    {
        Init();
        FadeController.FinishLoad();
    }

    void Update()
    {

    }

    public void Init()
    {
        //auxSpawns = spawnPoints;
        int posHorse = Random.Range(0, spawnPoints.Count);
        print("pos: " + posHorse);
        playerHorse.GetComponent<PlayerHorse>().Init(spawnPoints[posHorse]);
        //auxSpawns.RemoveAt(posHorse);
        for (int i = 0; i < spawnPoints.Count-1; i++)
        {
            //GameObject horse = enemyHorses[i];
            //posHorse = Random.Range(0, auxSpawns.Count);
            enemyHorses[i].GetComponent<EnemyHorse>().Init(spawnPoints[i >= posHorse ? i + 1 : i]);
            //auxSpawns.RemoveAt(posHorse);
        }
        //auxSpawns = null;
    }
}
