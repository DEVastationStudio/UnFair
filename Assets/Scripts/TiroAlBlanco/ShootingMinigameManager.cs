using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMinigameManager : MonoBehaviour
{
    #region Variables
    public UIGeneral _uiGeneral;
    public SpawnerDianas _spawnerDianas;
    #endregion Variables

    #region Metodos
    private void Start()
    {
        _uiGeneral.InitUI();
        _spawnerDianas.SpawnInit();
    }
    #endregion Metodos
}
