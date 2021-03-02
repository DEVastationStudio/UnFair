using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiraScript : MonoBehaviour
{
    [SerializeField] private GameObject _mira;
    [SerializeField] private GameObject _mousePoint;

    void Update()
    {
        //Opcion 1
        Vector3 pos = (_mousePoint.transform.position - _mira.transform.position);
        _mira.transform.position += pos.normalized * pos.magnitude * Time.deltaTime * 2;

        //Opcion 2
    }
}
