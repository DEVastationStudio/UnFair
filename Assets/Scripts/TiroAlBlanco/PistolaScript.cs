using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PistolaScript : MonoBehaviour
{
    #region Variables
    [Header("Interfaz de disparo")]
    [SerializeField] private GameObject _mira;
    [SerializeField] private GameObject _mousePoint;

    [Header("Otros Scripts")]
    [SerializeField] private ShootingMinigameManager _gameManager;

    private Vector2 pos;
    #endregion Variables

    #region Metodos
    private void Update()
    {
        if (_gameManager._uiGeneral.faseActual != UIGeneral.Fases.GAME) return;
        pos = Mouse.current.position.ReadValue();
        _mousePoint.transform.position = new Vector3(pos.x, pos.y, _mousePoint.transform.position.z);
    }

    void OnMouseLeftAction()
    {
        if (_gameManager._uiGeneral.faseActual != UIGeneral.Fases.GAME) return;
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(_mira.transform.position), out hit, 100);
        if (hit.collider != null && hit.transform.tag == "Diana")
        {
            GameObject diana = hit.transform.gameObject;
            _gameManager._spawnerDianas.DestroyTarget(diana.GetComponent<Diana>()._pos);
            _gameManager._uiGeneral.IncreasePuntuacion(diana.GetComponent<Diana>()._points);
            Destroy(diana);
            StartCoroutine(SpawnRetard());
        } else if (hit.collider != null && hit.transform.tag == "Pared") 
        {
            _gameManager._uiGeneral.IncreasePuntuacion(-5);
        }
    }

    private IEnumerator SpawnRetard() 
    {
        yield return new WaitForSeconds(1);
        _gameManager._spawnerDianas.SpawnNewTarget(0);
    }

    #endregion Metodos
}
