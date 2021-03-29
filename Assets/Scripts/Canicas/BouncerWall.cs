using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerWall : MonoBehaviour
{
    [SerializeField] private float power;
    void Start()
    {

    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        return;
        if (other.gameObject.CompareTag("Canica"))
        {
            Debug.Log("Rebote de canica en la simulacion");
            Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 direction = other.contacts[0].normal;
            if (rigidbody != null)
            {
                rigidbody.AddForce(direction * power, ForceMode.Impulse);
            }
        }
    }
}
