using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rampPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private Transform[] spawnPoints;
    private float rand;


    void Start()
    {
        GenerateRandomObstacle();
    }

    void Update()
    {

    }

    public void GenerateRandomObstacle()
    {
        GameObject auxInst;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            rand = Random.value;
            if (rand < 0.80f)
            {
                if (Random.value > 0.5f)
                {
                    auxInst = Instantiate(rampPrefab, spawnPoints[i].position, Quaternion.identity);
                    auxInst.transform.rotation = Quaternion.Euler(-90f, 0f, -90.0f);//(new Vector3(110f, -180f, -90f));
                }
                else
                {
                    auxInst = Instantiate(wallPrefab, spawnPoints[i].position, Quaternion.identity);
                    if (i == 0)
                    {
                        auxInst.transform.rotation = Quaternion.Euler(-106f, -4f, 60f);
                    }
                    else if (i == 1)
                    {
                        auxInst.transform.rotation = Quaternion.Euler(-115f, 6f, 120f);
                    }
                    else
                    {
                        auxInst.transform.rotation = Quaternion.Euler(-60f, 180f, -90f);
                    }
                }
            }
            else
            {
                print("No instancio");
            }
        }
    }
}
