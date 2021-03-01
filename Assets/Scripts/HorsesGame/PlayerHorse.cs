using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHorse : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    private float mov;
    private Vector3 newPos;
    private Vector3 aux;
    private bool pulsado;

    void Start()
    {
        pulsado = false;
    }

    private void OnSpaceAction(InputValue value)
    {
        Move();
    }

    private void Move()
    {
        mov = Random.Range(0.05f, 0.13f);
        aux = (Vector3.forward * mov);
        newPos = new Vector3(transform.position.x+aux.x, transform.position.y + aux.y, transform.position.z + aux.z);
        pulsado = true;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (pulsado)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPos, 1);            
            pulsado = false;
        }
    }

}
