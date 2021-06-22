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

    [Header("VFX")]
    [SerializeField] private GameObject _sparks;
    [SerializeField] private float _sparksTime;

    [Header("Otros Scripts")]
    [SerializeField] private ShootingMinigameManager _gameManager;
    [SerializeField] private PlayerInput input;

    [Header("Probabilidades")]
    [SerializeField] private int _restaDianaDorada = 10;
    [SerializeField] private int _restaDianaReloj = 5;

    private Vector2 _pos;
    private bool _disparo;
    private bool _isLastReloj;
    [HideInInspector] public float _timeToSpawnLetter;
    [HideInInspector] public int _probDianaDorada;
    [HideInInspector] public int _probReloj;
    #endregion Variables

    #region Metodos
    private void Update()
    {
        _timeToSpawnLetter -= Time.deltaTime;
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
        if (hit.transform == null) return;
        if (hit.transform.tag == "Diana" || hit.transform.tag == "DianaDorada" || hit.transform.tag == "Reloj" || hit.transform.tag == "DianaConLetra")
        {
            hit.transform.gameObject.GetComponent<Diana>().Hit(true, hit.point);
        }
        else if(hit.transform.tag == "Pared") 
        {
            _gameManager._logSystem._Miss++;
            _gameManager._dynamicDifficultyManager.SetValue(0, 0f);
            _gameManager._uiGeneral.IncreasePuntuacion(-5);
            _gameManager._vfxManager.InstantiateVFX(3, hit.point);
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
            _probDianaDorada -= _restaDianaDorada;
            _probReloj -= _restaDianaReloj;
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
