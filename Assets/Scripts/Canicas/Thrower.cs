using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Thrower : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private GameObject ballPref;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private float power;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private int ballsLeft;
    [SerializeField] private GameObject sphere;
    [SerializeField] private Slider forceBar;
    [SerializeField] private Image backgroundSlider;
    [SerializeField] private Image fillSlider;
    private float throwerForce;
    private float increaserForceSpeed = 0.5f;
    private bool pressingShoot;
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
    private bool isPaused;
    private bool inSettingsMenu;
    /*private bool leftRotation;
    private bool rightRotation;*/


    void Start()
    {
        input.SwitchCurrentActionMap("ActionMap");
        gameStarted = false;
        pressingShoot = false;
        isPaused = false;
        inSettingsMenu = false;
        throwerForce = 0.0f;
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
        if (!gameStarted || isPaused) { return; }
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
        //aqui si el bool de estar manteniendo, llamar al increase force las veces que sean necesarias
        /*if (forceBar.value < throwerForce)
        {
            forceBar.value += increaserForceSpeed * Time.deltaTime;
        }*/
        if (canThrow)
        {
            if (pressingShoot && canThrow)
            {
                IncreaseThrowForce(0.5f);//fuerza que va aumentando
                if (forceBar.value < throwerForce)
                {
                    forceBar.value += increaserForceSpeed * Time.deltaTime;
                }

            }

        }
        else
        {
            backgroundSlider.color = new Color(backgroundSlider.color.r, backgroundSlider.color.g, backgroundSlider.color.b, 0.25f);
            fillSlider.color = new Color(fillSlider.color.r, fillSlider.color.g, fillSlider.color.b, 0.25f);
        }

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
        print("Force: " + transform.forward * (power * (forceBar.value * 1.5f)));
        return transform.forward * (power * Mathf.Clamp(forceBar.value * 1.5f, 0.5f, 1.5f));
    }

    void ThrowBall()
    {
        ballsLeft--;
        GameObject ball = Instantiate(ballPref, firePoint.transform.position, Quaternion.identity);
        ball.GetComponent<Rigidbody>().AddForce(CalculateForce(), ForceMode.Impulse);
    }

    /*
    * Called in the invoke of OnSpaceAction
    */
    void ThrowBallCoyote()
    {
        if (canThrow && ballsLeft > 0)
        {
            ThrowBall();
            Invoke("ResetForceBar", 0.35f);
        }
        else
        {
            ResetForceBar();
        }

    }

    void CreatePrediction(Vector3 origin, Vector3 dst)
    {
        //trajectory.PathCreation(ballPref, firePoint.transform.position, CalculateForce());
        trajectory.LineCreation(origin, dst);
    }

    private void OnSpaceAction(InputValue value)
    {
        if (!gameStarted || isPaused) { return; }
        //if (!canThrow) return;//
        if (value.Get<float>() == 0)//sueltas tecla
        {
            pressingShoot = false;
            //Invoke("ResetForceBar", 0.35f);
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
            else
            {
                //Invoke("ResetForceBar", 0.25f);
                //Invoke("ThrowBallCoyote", 0.35f);
            }
        }
        else //presionas tecla
        {
            pressingShoot = true;
        }
    }

    private void OnMovement(InputValue value)
    {
        if (!gameStarted || isPaused) { return; }
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
    void OnEscAction(InputValue value)
    {
        if (gameStarted)
        {
            if (inSettingsMenu)
            {
                hud.CloseSettingsMenu();

            }
            else
            {
                isPaused = !isPaused;
                if (isPaused)
                {
                    Time.timeScale = 0;
                    //timeCounter.DeactivateTimer();
                    input.SwitchCurrentActionMap("UIMap");
                }
                else
                {
                    Time.timeScale = 1;
                    //timeCounter.ActivateTimer();
                    input.SwitchCurrentActionMap("ActionMap");
                }
                hud.PauseGame(isPaused);

            }
        }

    }
    public void UnPauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = 1;
        print("pausa quitada");
        //timeCounter.ActivateTimer();
        input.SwitchCurrentActionMap("ActionMap");

        hud.PauseGame(isPaused);
    }

    public void SetInSettings(bool inSettings)
    {
        inSettingsMenu = inSettings;
    }
    private void IncreaseThrowForce(float x)
    {
        throwerForce = forceBar.value + x;
        //print("thrower force: " + throwerForce + " forcebar.value: " + forceBar.value);
    }

    public int GetBallsLeft()
    {
        return ballsLeft;
    }

    public void SetCanThrow()
    {
        canThrow = true;
        backgroundSlider.color = new Color(backgroundSlider.color.r, backgroundSlider.color.g, backgroundSlider.color.b, 1.0f);
        fillSlider.color = new Color(fillSlider.color.r, fillSlider.color.g, fillSlider.color.b, 1.0f);
        ResetForceBar();
    }

    /*
    *Called in the invoke of OnSpaceAction
    */
    private void ResetForceBar()
    {
        throwerForce = 0.0f;
        forceBar.value = 0.0f;
    }
}
