using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public Rigidbody rigidBody;

    private float waterHeight;

    void Start() {
        waterHeight = 0;
    }    
    void FixedUpdate()
    {
        waterHeight = (Mathf.Cos(transform.position.y) + Mathf.Cos(transform.position.x + (5 * Time.timeSinceLevelLoad)) + Mathf.Cos(transform.position.z + (5 * Time.timeSinceLevelLoad)))*0.1f;

        if (transform.position.y < waterHeight)
        {
            //Floating force
            rigidBody.AddForce(new Vector3(0, Mathf.Abs(Physics.gravity.y) * (Mathf.Clamp(waterHeight-transform.position.y, 0, 1) * 3), 0), ForceMode.Acceleration);
            
            //Spin force
            Vector3 cross = Vector3.Cross(transform.position, Vector3.up);
            cross.y = 0;
            Vector3 pos = -transform.position;
            pos.y = 0;
            rigidBody.AddForce(cross, ForceMode.Force);
            rigidBody.AddForce(pos, ForceMode.Force);
            Debug.DrawRay(transform.position, Vector3.Cross(transform.position, Vector3.up));
        }
    }
}
