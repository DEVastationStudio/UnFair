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


    void Start()
    {
        trajectory = FindObjectOfType<Trajectory>();
        currentPos = transform.position;
        currentRot = transform.rotation;
        CreatePrediction();
    }

    void Update()
    {
        //poner como rotar y moverse para poder cambiar hacia donde va la pelota y eso
        if (currentRot != transform.rotation)
        {
            CreatePrediction();
        }

        if (currentPos != transform.position)
        {
            CreatePrediction();
        }

        currentRot = transform.rotation;
        //pulsar para disparar

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

    private void OnAAction(InputValue value)
    {
    }

    private void OnDAction(InputValue value)
    {
    }
}
