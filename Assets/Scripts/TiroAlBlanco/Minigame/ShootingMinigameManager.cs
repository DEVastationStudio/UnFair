﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMinigameManager : MonoBehaviour
{
    #region Variables
    public UIGeneral _uiGeneral;
    public SpawnerDianas _spawnerDianas;
    public PistolaScript _pistolaScript;
    public DynamicDifficultyManager _dynamicDifficultyManager;
    public VFXManager _vfxManager;
    public LetrasUnfairManager _letrasManager;
    #endregion Variables

    #region Metodos
    private void Start()
    {
        _pistolaScript._probDianaDorada = 110; 
        _pistolaScript._probReloj = 110;
        _uiGeneral.FasePreGame();
        FadeController.FinishLoad();
    }

    public void StartGame() 
    {
        _uiGeneral.FaseGame();
        _spawnerDianas.SpawnInit();
    }

    public void RestartGame() 
    {
        _pistolaScript._probDianaDorada = 110;
        _uiGeneral.FasePreGame();
    }
    #endregion Metodos
}
