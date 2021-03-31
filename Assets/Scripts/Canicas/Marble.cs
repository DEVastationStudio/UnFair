using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour
{
    private bool hasCollided;
    void Start()
    {
        hasCollided = false;
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            hasCollided = true;
        }
    }

    public bool GetCollided()
    {
        return hasCollided;
    }
}
