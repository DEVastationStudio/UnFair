﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaController : MonoBehaviour
{
    [SerializeField] private HUD_Manager hUD_Manager;
    [SerializeField] private TimeCounter timeCounter;
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

    private void OnTriggerEnter(Collider other)
    {
        if (playerFinished) { return; }
        if (other.CompareTag("Player"))
        {
            timeCounter.DeactivateTimer();
            Debug.Log("Llegaste a la meta en posición: " + playerPos);
            playerFinished = true;
            hUD_Manager.RaceFinished(playerPos);
        }
        else if (other.CompareTag("RivalHorse"))
        {
            playerPos++;
            Debug.Log("Rival llegó");
        }
    }
}
