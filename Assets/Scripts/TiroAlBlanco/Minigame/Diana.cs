using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana : MonoBehaviour
{
    public int _points;
    public int _pos;
    public bool _hit;
    public bool _first;
    private ShootingMinigameManager _gameManager;

    public void StartDiana()
    {
        _gameManager = FindObjectOfType<ShootingMinigameManager>();
        if (transform.tag == "DianaDorada" || transform.tag == "Reloj")
            StartCoroutine(DianaTemporal(2));
        else if (_first)
            StartCoroutine(DianaTemporal(7));
        else
            StartCoroutine(DianaTemporal(4));
    }

    IEnumerator DianaTemporal(int t) 
    {
        yield return new WaitForSeconds(t);
        if (!_hit)
        {
            _hit = true;
            _gameManager._spawnerDianas.DestroyTarget(gameObject.GetComponent<Diana>()._pos);
            _gameManager._uiGeneral.IncreasePuntuacion(gameObject.GetComponent<Diana>()._points);
            if(transform.tag == "DianaDorada" || transform.tag == "Reloj")
                _gameManager._pistolaScript.AutomaticDespawn();
            else
                _gameManager._pistolaScript.CallSpawnRetard(0);
            Destroy(gameObject);
        }
        else 
        {
            Debug.Log("No me destruyo porque no me sale de ahí");
        }
    }
}
