using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Thrower : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private GameObject ballPref;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private float power;
    [SerializeField] private float rotationSpeed;

    private Vector3 currentPos;
    Quaternion currentRot;
    private Trajectory trajectory;
    private int test = 0;
    private int rotation;
    /*private bool leftRotation;
    private bool rightRotation;*/


    void Start()
    {
        rotation = 0;
        trajectory = FindObjectOfType<Trajectory>();
        currentPos = transform.position;
        currentRot = transform.rotation;
        CreatePrediction();
    }

    void FixedUpdate()
    {
        Rotate();
        if (currentRot != transform.rotation)
        {
            CreatePrediction();
        }

        /*if (currentPos != transform.position)
        {
            CreatePrediction();
        }*/

        currentRot = transform.rotation;

    }
    private void Rotate()
    {
        transform.Rotate(0, rotationSpeed * rotation, 0.0f);
    }
    public Vector3 CalculateForce()
    {
        return transform.forward * power;
    }

    void ThrowBall()
    {
        GameObject ball = Instantiate(ballPref, firePoint.transform.position, Quaternion.identity);
        ball.GetComponent<Rigidbody>().AddForce(CalculateForce(), ForceMode.Impulse);
    }

    void CreatePrediction()
    {
        trajectory.PathCreation(ballPref, firePoint.transform.position, CalculateForce());
    }

    private void OnSpaceAction(InputValue value)
    {
        ThrowBall();
    }

    private void OnMovement(InputValue value)
    {
        if (value.Get<Vector2>().x < -0.2f)
        {
            rotation = -1;
        }
        else if (value.Get<Vector2>().x > 0.2f)
        {
            rotation = 1;
        }
        else
        {
            rotation = 0;
        }
    }
}
