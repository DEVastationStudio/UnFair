using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public Rigidbody rigidBody;

    private float _waterHeight;
    private bool _inWater;
    private bool _magnetized;
    private GameObject _magnet;

    protected virtual void Start() {
        _waterHeight = 0;
        _inWater = true;
    }    
    void FixedUpdate()
    {
        if (_inWater)
        {
            //TO-DO: Move this line to a singleton or the water object or wherever
            _waterHeight = (Mathf.Cos(transform.position.y) + Mathf.Cos(transform.position.x + (5 * Time.timeSinceLevelLoad)) + Mathf.Cos(transform.position.z + (5 * Time.timeSinceLevelLoad)))*0.1f;

            if (transform.position.y < _waterHeight)
            {
                //Floating force
                rigidBody.AddForce(new Vector3(0, Mathf.Abs(Physics.gravity.y) * (Mathf.Clamp(_waterHeight-transform.position.y, 0, 1) * 3), 0), ForceMode.Acceleration);
                
                //Spin force
                Vector3 cross = Vector3.Cross(transform.position, Vector3.up);
                cross.y = 0;
                Vector3 pos = -transform.position;
                pos.y = 0;
                rigidBody.AddForce(cross/4, ForceMode.Force);
                rigidBody.AddForce(pos/4, ForceMode.Force);
                Debug.DrawRay(transform.position, Vector3.Cross(transform.position, Vector3.up));
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Magnet" && _inWater && !_magnetized)
        {
            _inWater = false;
            _magnetized = true;
            _magnet = other.gameObject;
            rigidBody.constraints = RigidbodyConstraints.FreezeAll;
            transform.parent = _magnet.transform;
            other.tag = "Untagged";
            transform.localPosition = new Vector3(0,-2.4f,0);
        }
        else if (other.tag == "Basket" && _magnetized)
        {
            _magnetized = false;
            rigidBody.constraints = RigidbodyConstraints.None;
            transform.parent = null;
            _magnet.tag = "Magnet";
            _magnet = null;
            OnBasketEnter(true);
        }
        else if (other.tag == "RivalBasket" && _magnetized)
        {
            _magnetized = false;
            rigidBody.constraints = RigidbodyConstraints.None;
            transform.parent = null;
            _magnet.tag = "Magnet";
            _magnet = null;
            OnBasketEnter(false);
        }
    }

    protected virtual void OnBasketEnter(bool player) {}
}
