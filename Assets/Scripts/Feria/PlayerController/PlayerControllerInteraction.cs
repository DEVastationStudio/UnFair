using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class PlayerController : MonoBehaviour
{
    #region Variables

    private string _scene;

    #endregion Variables

    #region Metodos

    public void SetScene(string scene) 
    {
        if (scene == "-1")
        {
            _uiManager.CloseNoriaNotAvailable();
            return;
        }
        _scene = scene;
    }
    private void StartMinigame()
    {
        if (_scene == "Noria")
        {
            int totalStars = 0;
            bool allGameWins = true;
            for (int i = 1; i <= 4; i++) 
            {
                if (GameProgress.GetStars(i) == 0) allGameWins = false;
                totalStars += GameProgress.GetStars(i);
            }
            if (totalStars >= 5 && !allGameWins) // Quitar la exclamación
                FadeController.Fade(_scene);
            else
                _uiManager.OpenNoriaNotAvailable();
        }
        else
        {
            FadeController.Fade(_scene);
        }
    }

    #endregion Metodos
}
