using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public Rigidbody rigidBody;
    void FixedUpdate()
    {
        if (transform.position.y < 0)
        {
            rigidBody.AddForce(new Vector3(0, Mathf.Abs(Physics.gravity.y) * (Mathf.Clamp(-transform.position.y, 0, 1) * 3), 0), ForceMode.Acceleration);
        }
    }
}
