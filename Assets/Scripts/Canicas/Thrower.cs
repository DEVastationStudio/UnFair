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
    [SerializeField] private GameObject sphere;
    private HUD_Marbles hud;
    private bool canThrow;
    private Vector3 currentPos;
    Quaternion currentRot;
    private Trajectory trajectory;
    private int rotation;
    private bool gameStarted;
    private float rotx, rotz;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 hitPos;
    /*private bool leftRotation;
    private bool rightRotation;*/


    void Start()
    {
        gameStarted = false;
        rotation = 0;
        canThrow = true;
        trajectory = FindObjectOfType<Trajectory>();
        hud = FindObjectOfType<HUD_Marbles>();
        currentPos = transform.position;
        currentRot = transform.rotation;
        rotx = -20;
        rotz = 0;
        ray = new Ray(transform.position, transform.forward);
        //CreatePrediction();
    }

    void Update()
    {
        if (!gameStarted) { return; }
        Rotate();
        /*if (currentRot != transform.rotation)
        {*/
        ray.direction = transform.forward;
        Debug.DrawRay(ray.origin, ray.direction.normalized * 100f, Color.red);
        if (Physics.Raycast(ray, out hit, 20.0f) && hit.transform.CompareTag("Obstacle"))
        {

            hitPos = hit.point;
            //Instantiate(sphere, hitPos, Quaternion.identity);
            CreatePrediction(ray.origin, hitPos);

        }

        //}

        /*if (currentPos != transform.position)
        {
            CreatePrediction();
        }*/

        currentRot = transform.rotation;

    }

    public void SetGameStarted()
    {
        gameStarted = true;
    }

    public void SetGameFinished()
    {
        gameStarted = false;
    }
    
    private void Rotate()
    {
        transform.Rotate(0, rotationSpeed * rotation, 0.0f);
        Vector3 currentRot = transform.localEulerAngles;
        currentRot.y = Mathf.Clamp(((currentRot.y > 180) ? currentRot.y - 360 : currentRot.y), -45.0f, 45.0f);
        currentRot.x = rotx;
        currentRot.z = rotz;
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

    void CreatePrediction(Vector3 origin, Vector3 dst)
    {
        //trajectory.PathCreation(ballPref, firePoint.transform.position, CalculateForce());
        trajectory.LineCreation(origin, dst);
    }

    private void OnSpaceAction(InputValue value)
    {
        if (!gameStarted) { return; }
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
        if (!gameStarted) { return; }
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
