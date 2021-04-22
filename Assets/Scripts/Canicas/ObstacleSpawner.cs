using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rampPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject[] spawnPoints;
    private List<GameObject> spawnList;
    [SerializeField] private DynamicDifficultyManager DDM;
    [SerializeField] private GameObject thrower;
    private float rand;


    void Start()
    {
        GenerateRandomObstacle();
    }

    public void GenerateRandomObstacle()
    {
        GameObject auxInst;
        int auxPosList;
        spawnList = new List<GameObject>(spawnPoints);//lista para expulsar
        for (int i = 0; i < (int)DDM.GetValue(1); i++)
        {
            auxPosList = Random.Range(0, spawnList.Count);
            TargetPoints targetAux = spawnList[auxPosList].GetComponent<TargetPoints>();
            if (Random.value > 0.5f)
            {
                //Rampa
                auxInst = Instantiate(rampPrefab, targetAux.GetTargets()[Random.Range(0,2)].position/*targetAux.gameObject.transform.position*/, Quaternion.identity);

                switch (targetAux.GetPosSpawn().ToString())
                {
                    case "North":
                        if (Random.value > 0.5f)
                        {
                            auxInst.transform.rotation = Quaternion.Euler(-4.0f, 8.0f, -3.0f);
                        }
                        else
                        {
                            auxInst.transform.rotation = Quaternion.Euler(-4.0f, -8.0f, 3.0f);
                        }
                        break;
                    case "South":
                        if (Random.value > 0.5f)
                        {
                            auxInst.transform.rotation = Quaternion.Euler(-4.0f, 8.0f, -3.0f);
                        }
                        else
                        {
                            auxInst.transform.rotation = Quaternion.Euler(-4.0f, -8.0f, 3.0f);
                        }
                        break;
                    case "East":
                        auxInst.transform.rotation = Quaternion.Euler(0f, -45f, 15.0f);//de derecha a izquierda la rampa
                        break;
                    case "West":
                        auxInst.transform.rotation = Quaternion.Euler(0f, 45f, -15.0f);//de izquierda a derecha la rampa
                        break;
                }

            }
            else
            {   //Pared
                auxInst = Instantiate(wallPrefab, targetAux.GetTargets()[Random.Range(0,2)].position/*targetAux.gameObject.transform.position*/, Quaternion.identity);
                switch (targetAux.GetPosSpawn().ToString())
                {
                    case "North":
                        if (Random.value > 0.5f)
                        {
                            auxInst.transform.rotation = Quaternion.Euler(0.0f, -5.0f, 2.0f);
                        }
                        else
                        {
                            auxInst.transform.rotation = Quaternion.Euler(0.0f, 5.0f, -2.0f);
                        }
                        break;
                    case "South":
                        if (Random.value > 0.5f)
                        {
                            auxInst.transform.rotation = Quaternion.Euler(0.0f, -5.0f, 2.0f);
                        }
                        else
                        {
                            auxInst.transform.rotation = Quaternion.Euler(0.0f, 5.0f, -2.0f);
                        }
                        break;
                    case "East":
                        auxInst.transform.rotation = Quaternion.Euler(0.0f, 30f, -13f);
                        break;
                    case "West":
                        auxInst.transform.rotation = Quaternion.Euler(0.0f, -30f, 13f);
                        break;
                }

            }
            auxInst.GetComponent<PathMovement>().SetTargets(targetAux.GetTargets());
            spawnList.RemoveAt(auxPosList);
        }
    }
}
