using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorse : MonoBehaviour
{
    private float mov;
    private Vector3 newPos;
    private Vector3 aux;
    private bool move;

    void Start()
    {
        move = false;
    }


    void Update()
    {
        mov = Random.Range(0.05f, 0.20f);
        GetPos();
    }

    private void FixedUpdate()
    {
        float rand = Random.Range(0.0f,15.0f);
        if (rand<12f)
        {
            return;
        }
            move = false;
            transform.position = Vector3.MoveTowards(transform.position, newPos, 0.05f);

    }

    private void GetPos()
    {
        aux = (Vector3.forward * mov);
        newPos = new Vector3(transform.position.x + aux.x, transform.position.y + aux.y, transform.position.z + aux.z);
    }
}
