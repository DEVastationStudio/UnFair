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
        _scene = scene;
    }
    private void StartMinigame()
    {
        if (_fadeController != null)
            _fadeController.Fade(_scene);
        else
            SceneManager.LoadScene(_scene);
    }

    #endregion Metodos
}
