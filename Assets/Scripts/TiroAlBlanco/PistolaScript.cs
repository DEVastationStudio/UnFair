using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PistolaScript : MonoBehaviour
{
    [Header("Interfaz de disparo")]
    [SerializeField] private GameObject _mira;
    [SerializeField] private GameObject _mousePoint;

    [Header("Otros Scripts")]
    [SerializeField] private UIGeneral _uiManager;

    private Vector2 pos;

    private void Update()
    {
        pos = Mouse.current.position.ReadValue();
        _mousePoint.transform.position = new Vector3(pos.x, pos.y, _mousePoint.transform.position.z);
    }
    void OnMouseLeftAction()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(_mira.transform.position), out hit, 100);
        if (hit.collider != null && hit.transform.tag == "Diana")
        {
            _uiManager.IncreasePuntuacion(10);
            Destroy(hit.transform.gameObject);
        }
    }
}
