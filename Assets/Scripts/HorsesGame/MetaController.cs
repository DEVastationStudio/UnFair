using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaController : MonoBehaviour
{
    private int playerPos;
    private bool playerFinished;
    void Start()
    {
        playerPos = 1;
        playerFinished = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(playerFinished){return;}
        if(other.CompareTag("Player"))
        {
            Debug.Log("Llegaste a la meta en posición: " + playerPos);
            playerFinished = true;
        }else if(other.CompareTag("RivalHorse"))
        {
            playerPos++;
            Debug.Log("Rival llegó");
        }
    }
}
