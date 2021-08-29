using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour
{
    private bool hasCollided;
    private float timeToIncrease;
    private Rigidbody rb;
    private float timeToFall;
    void Start()
    {
        timeToFall = 0.0f;
        hasCollided = false;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    private void OnCollisionStay(Collision other)
    {
        if (rb.velocity.y < 0)
        {
            timeToFall += Time.deltaTime;
            print("bola cayendo");
            if (timeToFall > 1.5f)
            {
                rb.velocity = new Vector3(rb.velocity.x, (rb.velocity.y - 2f), rb.velocity.z);
            }
            else if (timeToFall > 0.5f)
            {
                rb.velocity = new Vector3(rb.velocity.x, (rb.velocity.y - 1.5f), rb.velocity.z);

            }
            else
            {
                rb.velocity = new Vector3(rb.velocity.x, (rb.velocity.y - 1f), rb.velocity.z);
            }
        }
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
