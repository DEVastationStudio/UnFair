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
    [SerializeField] private int ballsLeft;
    private bool canThrow;
    private Vector3 currentPos;
    Quaternion currentRot;
    private Trajectory trajectory;
    private int rotation;
    /*private bool leftRotation;
    private bool rightRotation;*/


    void Start()
    {
        rotation = 0;
        canThrow = true;
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
        Vector3 currentRot = transform.localEulerAngles;
        currentRot.y = Mathf.Clamp(((currentRot.y > 180) ? currentRot.y - 360 : currentRot.y), -45.0f, 45.0f);
        transform.localRotation = Quaternion.Euler(currentRot);
    }
    public Vector3 CalculateForce()
    {
        return transform.forward * power;
    }

    void ThrowBall()
    {
        ballsLeft--;
        GameObject ball = Instantiate(ballPref, firePoint.transform.position, Quaternion.identity);
        ball.GetComponent<Rigidbody>().AddForce(CalculateForce(), ForceMode.Impulse);
    }

    void CreatePrediction()
    {
        trajectory.PathCreation(ballPref, firePoint.transform.position, CalculateForce());
    }

    private void OnSpaceAction(InputValue value)
    {
        if (canThrow)
        {
            if (ballsLeft > 0)
            {
                canThrow = false;
                ThrowBall();
            }
            else
            {
                Debug.Log("Te has quedado sin pelotas");
            }
        }

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

    public int GetBallsLeft()
    {
        return ballsLeft;
    }

    public void SetCanThrow()
    {
        canThrow = true;
    }
}
