using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    #region Variables

    [Header("Movement variables")]
    [SerializeField] private float _velocity;
    [SerializeField] private SpriteRenderer _sprite;

    [Header("Interfaz")]
    [SerializeField] private UIManager _uiManager;

    [Header("Cameras")]
    [SerializeField] private GameObject _titleCamera;
    [SerializeField] private GameObject _gameCamera;
    [SerializeField] private GameObject  _dummyCamera;
    [SerializeField] private GameObject _titleScreen;

    #endregion Variables

    #region Metodos
    void Update()
    {
        Move();
    }

    void Start() {
        StartCoroutine(LoadGameScene());
    }
    private IEnumerator LoadGameScene()
    {
        FadeController.instance.player = this;
        if (FadeController.instance.storedPlayerPosition)
        {
            transform.position = FadeController.instance.lastPlayerPosition;
            yield return new WaitForSeconds(0.5f);
            _titleCamera.SetActive(false);
            _dummyCamera.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _dummyCamera.SetActive(false);
            _gameCamera.SetActive(true);
            _titleScreen.SetActive(false);
        }
        yield return null;
        FadeController.FinishLoad();
    }

    #endregion Metodos
}
