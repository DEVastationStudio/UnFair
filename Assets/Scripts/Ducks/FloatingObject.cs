using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public Rigidbody rigidBody;

    protected float _waterHeight;
    protected bool _inWater;
    protected bool _magnetized;
    protected GameObject _magnet;
    public Collider col1, col2;

    protected virtual void Start() {
        _waterHeight = 0;
        _inWater = true;
    }    
    protected virtual void FixedUpdate()
    {
        if (_inWater)
        {
            //TO-DO: Move this line to a singleton or the water object or wherever
            _waterHeight = (Mathf.Cos(transform.position.y) + Mathf.Cos(transform.position.x + (5 * Time.timeSinceLevelLoad)) + Mathf.Cos(transform.position.z + (5 * Time.timeSinceLevelLoad)))*0.3f;

            if (transform.position.y < _waterHeight)
            {
                //Floating force
                rigidBody.AddForce(new Vector3(0, Mathf.Abs(Physics.gravity.y) * (Mathf.Clamp(_waterHeight-transform.position.y, 0, 1) * 3), 0), ForceMode.Acceleration);
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
            gameObject.layer = 0;
            transform.localScale *= 1.2f;
            StartCoroutine(MoveToMagnet());
            //transform.localPosition = new Vector3(0,-1.8f,0);
        }
        else if (other.tag == "Basket" && _magnetized)
        {
            _magnetized = false;
            rigidBody.constraints = RigidbodyConstraints.None;
            transform.parent = null;
            _magnet.tag = "Magnet";
            _magnet = null;
            transform.localScale /= 1.2f;
            OnBasketEnter(true);
        }
        else if (other.tag == "RivalBasket" && _magnetized)
        {
            _magnetized = false;
            rigidBody.constraints = RigidbodyConstraints.None;
            transform.parent = null;
            _magnet.tag = "Magnet";
            _magnet = null;
            transform.localScale /= 1.2f;
            OnBasketEnter(false);
        }
    }

    protected IEnumerator MoveToMagnet()
    {
        Vector3 targetPos = transform.parent.transform.position + new Vector3(0,-1.8f,0);
        col1.enabled = false;
        col2.enabled = false;

        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            targetPos = transform.parent.transform.position + new Vector3(0,-1.8f,0);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.1f);
            Debug.DrawLine(transform.position, targetPos, Color.red);
            yield return null;
        }
        transform.localPosition = new Vector3(0,-1.8f,0);
        col1.enabled = true;
        col2.enabled = true;
    }

    protected virtual void OnBasketEnter(bool player) {}
}
