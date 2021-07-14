using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorsesSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<GameObject> enemyHorses;
    [SerializeField] private GameObject playerHorse;
    [SerializeField] private HorsesLogSystem _logSystem;
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
        _logSystem._PH = posHorse;
        for (int i = 0; i < spawnPoints.Count-1; i++)
        {
            enemyHorses[i].GetComponent<EnemyHorse>().Init(spawnPoints[i >= posHorse ? i + 1 : i]);
        }
    }
}
