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
    [SerializeField] private PlayerInput input;


    private Vector2 _pos;
    private bool _disparo;
    [HideInInspector] public int _probDianaDorada;
    [HideInInspector] public int _probReloj;
    #endregion Variables

    #region Metodos
    private void Update()
    {
        PointerUpdate();
    }

    void OnLook(InputValue value) 
    {
        _pos = value.Get<Vector2>();
    }

    void OnEscAction()
    {
        if (_gameManager._uiGeneral.faseActual == UIGeneral.Fases.GAME) 
        {
            _gameManager._uiGeneral.Pause();
        }
    }

    private void PointerUpdate() 
    {
        if (_gameManager._uiGeneral.faseActual != UIGeneral.Fases.GAME) return;
        if (input.currentControlScheme.Equals("KeyboardMouseScheme"))
            _mousePoint.transform.position = new Vector3(_pos.x, _pos.y, _mousePoint.transform.position.z);
        else if (input.currentControlScheme.Equals("GamepadScheme"))
            _mousePoint.transform.position += new Vector3(_pos.x * 6, _pos.y * 6, _mousePoint.transform.position.z);
    }

    void OnMouseLeftAction()
    {
        _disparo = !_disparo;
        if (_disparo != true) return;
        if (_gameManager._uiGeneral.faseActual != UIGeneral.Fases.GAME) return;
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(_mira.transform.position), out hit, 100);
        if (hit.collider != null && (hit.transform.tag == "Diana"))
        {
            Diana diana = hit.transform.gameObject.GetComponent<Diana>();
            diana.GetComponent<Diana>()._hit = true;
            _gameManager._spawnerDianas.DestroyTarget(diana._pos);
            _gameManager._uiGeneral.IncreasePuntuacion(diana._points);
            if (hit.transform.tag == "Diana")
                _gameManager._dynamicDifficultyManager.SetValue(0, diana.GetPercentageLifeTime());

            Destroy(diana.gameObject);
            if (Random.Range(0, 100) > _probReloj) 
            {
                _probReloj = 110;
                CallSpawnRetard(2);
            }
            else if (Random.Range(0, 100) > _probDianaDorada)
            {
                _probDianaDorada = 110;
                CallSpawnRetard(1);
            }
            else 
            {
                _probDianaDorada-=5; 
                _probReloj -= 2;
                CallSpawnRetard(0);
            }
        }
        else if (hit.collider != null && (hit.transform.tag == "DianaDorada" || hit.transform.tag == "Reloj")) 
        {
            GameObject diana = hit.transform.gameObject;
            _gameManager._spawnerDianas.DestroyTarget(diana.GetComponent<Diana>()._pos);
            if(hit.transform.tag == "DianaDorada")
                _gameManager._uiGeneral.IncreasePuntuacion(diana.GetComponent<Diana>()._points);
            else
                _gameManager._uiGeneral.AddTime(5);

            if (hit.transform.tag == "DianaDorada")
                _gameManager._dynamicDifficultyManager.SetValue(0, 0.90f);
            else if (hit.transform.tag == "Reloj")
                _gameManager._dynamicDifficultyManager.SetValue(0, 1f);

            Destroy(diana);
            CallSpawnRetard(0);
        }
        else if (hit.collider != null && hit.transform.tag == "Pared")
        {
            _gameManager._dynamicDifficultyManager.SetValue(0, 0f);
            _gameManager._uiGeneral.IncreasePuntuacion(-5);
        }
    }

    public void AutomaticDespawn() 
    {
        if (Random.Range(0, 100) > _probReloj)
        {
            _probReloj = 110;
            CallSpawnRetard(2);
        }
        else if (Random.Range(0, 100) > _probDianaDorada)
        {
            _probDianaDorada = 110;
            CallSpawnRetard(1);
        }
        else
        {
            _probDianaDorada -= 5;
            _probReloj -= 2;
            CallSpawnRetard(0);
        }
    }

    public void CallSpawnRetard(int type) 
    {
        StartCoroutine(SpawnRetard(type));
    }

    private IEnumerator SpawnRetard(int type) 
    {
        yield return new WaitForSeconds(0.5f);
        if(_gameManager._uiGeneral.faseActual == UIGeneral.Fases.GAME)
            _gameManager._spawnerDianas.SpawnNewTarget(type);
    }

    #endregion Metodos
}
