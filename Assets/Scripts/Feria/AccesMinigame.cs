using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccesMinigame : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject _infoUI;
    [SerializeField] private string _scene;

    private PlayerController _player;

    #endregion Variables

    #region Metodos
    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        Debug.Log("Se ejecuta esto");
    }

    private void OnTriggerEnter(Collider other)
    {
        _player._isNearMinigame = true;
        _player.SetScene(_scene);
        _infoUI.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        _player._isNearMinigame = false;
        _player.SetScene("-1");
        _infoUI.SetActive(false);
    }

    #endregion Metodos
}


