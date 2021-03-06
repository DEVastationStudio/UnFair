﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaController : MonoBehaviour
{
    [SerializeField] private HUD_Manager hUD_Manager;
    [SerializeField] private TimeCounter timeCounter;
    private int playerPos;
    private bool playerFinished;
    private bool createNewCombo;
    void Start()
    {
        Init();
    }

    void Update()
    {

    }
    public void Init()
    {
        playerPos = 1;
        playerFinished = false;
        createNewCombo = true;
    }

    public bool GetCreateNewCombo()
    {
        return createNewCombo;
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
        if (other.CompareTag("RivalHorse"))
        {
            playerPos++;
            playerFinished = true;
            hUD_Manager.RaceFinished(playerPos);
            Debug.Log("Rival llegó");
        }
        if (other.CompareTag("NewPosPlayer"))
        {
            createNewCombo = false;
            hUD_Manager.DisableComboPanel();
        }
    }
}
