using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana : MonoBehaviour
{
    public int _points;
    public int _pos;
    public bool _hit;
    private ShootingMinigameManager _gameManager;

    private void Start()
    {
        if (transform.tag == "DianaDorada" || transform.tag == "Reloj")
        {
            _gameManager = FindObjectOfType<ShootingMinigameManager>();
            StartCoroutine(DianaDorada());
        }
    }

    IEnumerator DianaDorada() 
    {
        yield return new WaitForSeconds(2);
        if (!_hit)
        {
            _hit = true;
            _gameManager._spawnerDianas.DestroyTarget(gameObject.GetComponent<Diana>()._pos);
            _gameManager._uiGeneral.IncreasePuntuacion(gameObject.GetComponent<Diana>()._points);
            _gameManager._pistolaScript.CallSpawnRetard(0);
            Destroy(gameObject);
        }
        else 
        {
            Debug.Log("No me destruyo porque no me sale de ahí");
        }
    }
}
