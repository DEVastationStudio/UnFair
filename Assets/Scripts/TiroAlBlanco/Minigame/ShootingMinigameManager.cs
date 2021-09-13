using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class ShootingMinigameManager : MonoBehaviour
{
    #region Variables
    public UIGeneral _uiGeneral;
    public SpawnerDianas _spawnerDianas;
    public PistolaScript _pistolaScript;
    public DynamicDifficultyManager _dynamicDifficultyManager;
    public VFXManager _vfxManager;
    public LetrasUnfairManager _letrasManager;
    public ShootingLogSystem _logSystem;
    public ComboCounter _comboCounter;
    public ShootStarManager _starManager;
    [HideInInspector] public Stopwatch _stopwatch;

    [Header("Audio")]
    public AudioSource _goldRushSfx;
    public AudioSource _shootSfx;
    public AudioSource _badSfx;
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
        AudioManager.instance.FadeOut(10,0.1f);
        _uiGeneral.FaseGame();
    }

    public void RestartGame() 
    {
        AudioManager.instance.changeTheme(9);
        _pistolaScript._probDianaDorada = 110;
        _uiGeneral.ResetTimer();
        _uiGeneral.FasePreGame();
    }
    #endregion Metodos
}
