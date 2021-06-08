using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class PlayerHorse : MonoBehaviour
{
    private enum Scheme
    {
        KeyboardMouse,
        Gamepad
    };
    private Scheme previousScheme;
    [SerializeField] private PlayerInput input;
    private float mov;
    [SerializeField] private float timeForKeys = 1.5f;
    [SerializeField] private Sprite[] comboImages;//total de posibles botones//UP,LEFT,RIGHT,DOWN,SPACE,X,A
    [SerializeField] private Image[] hudImages;//botones que pueden salir en el HUD//0,1,2
    [SerializeField] private GameObject comboBackground;
    [SerializeField] private GameObject newPosPlayer;
    private float currentTime;
    private Vector3 newPos;
    private Vector3 auxPos;
    public string[] comb;
    private string[] availableKeys;
    private int posComb;
    private int correctSequence;
    private bool combCreated;
    private int endedCombos;//veces realizadas el combo actual
    private int endedTotalCombos;//combos totales
    [SerializeField] private TextMeshProUGUI[] comboText;
    private bool endedCurrentCombo;
    private bool restartingComboText;
    private bool gameStarted;
    private bool isPaused;
    private bool comboFailed;
    [SerializeField] private TimeCounter timeCounter;
    [SerializeField] private DynamicDifficultyManager DDM;
    private int combosFinishedDDM;
    private int failedCombosDDM;
    private float velocityCombos;
    [SerializeField] private HUD_Manager hud;
    private bool inSettingsMenu;
    private bool validButton;
    private ButtonControl buttonPressed;
    private int buttonsPressed;
    private bool buttonPressing;
    private bool joystickUsed;
    private bool joystickReseted;
    private string lastJoystickPos;
    private Animator animator;
    private Transform initialPos;
    private Vector3 _initialNewPosPlayer;
    private bool finishingRace;

    #region UnityMethods

    void Awake()
    {
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _initialNewPosPlayer = newPosPlayer.transform.position;

        //Init();
        //FadeController.FinishLoad();
    }

    void Update()
    {
        if (!gameStarted || !timeCounter.GetActivatedTimer()) { return; }
        if (input.currentControlScheme.Equals("KeyboardMouseScheme"))
        {
            if (!endedCurrentCombo && previousScheme == Scheme.Gamepad)
            {
                Debug.Log("De mando a teclado");
                previousScheme = Scheme.KeyboardMouse;
                ChangedSchemeText();
            }
            if (combCreated && Keyboard.current.anyKey.wasPressedThisFrame)
            {
                if (!(Keyboard.current[Key.W].wasPressedThisFrame || Keyboard.current[Key.A].wasPressedThisFrame || Keyboard.current[Key.S].wasPressedThisFrame || Keyboard.current[Key.D].wasPressedThisFrame || Keyboard.current[Key.E].wasPressedThisFrame || Keyboard.current[Key.Space].wasPressedThisFrame || Keyboard.current[Key.UpArrow].wasPressedThisFrame || Keyboard.current[Key.DownArrow].wasPressedThisFrame
                || Keyboard.current[Key.LeftArrow].wasPressedThisFrame || Keyboard.current[Key.RightArrow].wasPressedThisFrame || Keyboard.current[Key.Escape].wasPressedThisFrame || Keyboard.current[Key.Enter].wasPressedThisFrame))
                {
                    //Debug.Log("Tecla ajena al conjunto de teclas creadas para el minijuego");
                    ResetCorrect(false, true);
                    return;
                }
            }

        }
        else if (input.currentControlScheme.Equals("GamepadScheme"))
        {
            if (!endedCurrentCombo && previousScheme == Scheme.KeyboardMouse)
            {
                Debug.Log("De teclado a mando");
                previousScheme = Scheme.Gamepad;
                ChangedSchemeText();
            }
            if (!buttonPressing)
            {
                buttonsPressed = Gamepad.current.allControls.Count(x => x is ButtonControl button && x.IsPressed() && !x.synthetic);
                if (buttonsPressed > 1)
                {
                    if (!joystickUsed)
                    {
                        //print("muchos botones pulsados");
                        ResetCorrect(false, true);
                        return;
                    }

                }
                else if (buttonsPressed == 1)
                {
                    buttonPressed = (ButtonControl)Gamepad.current.allControls.FirstOrDefault(x => x is ButtonControl button && x.IsPressed() && !x.synthetic);
                    //print("Key pressed: " + buttonPressed.ToString());
                    buttonPressing = true;
                    if (validButton)
                    {
                        //print("tocaste tecla de las validas");
                        validButton = false;
                        //oneButtonPressed = false;
                    }
                    else
                    {
                        if (!joystickUsed)
                        {
                            //print("tocaste tecla de las NO validas");
                            ResetCorrect(false, true);
                            return;

                        }
                    }
                }
            }
            else
            {
                if (!buttonPressed.IsPressed())
                {
                    //print("Soltaste el botón: " + buttonPressed.ToString());
                    buttonPressing = false;
                    buttonPressed = null;
                }

            }
        }

        if (posComb <= 0) { return; }
        currentTime += Time.deltaTime;
        velocityCombos += Time.deltaTime;
        if (currentTime >= timeForKeys)
        {
            //Debug.Log("Mucho tiempo entre tecla y tecla");
            ResetCorrect(false, true);
        }
    }

    #endregion UnityMethods

    #region KeysActions
    private void OnSpaceAction(InputValue value)
    {
        if (value.Get<float>() == 0) return;
        if (input.currentControlScheme.Equals("GamepadScheme")) { validButton = true; }
        CombinationManagement("Space");
        lastJoystickPos = "";
    }

    private void OnAAction(InputValue value)
    {
        if (input.currentControlScheme.Equals("GamepadScheme"))
        {
            validButton = true;
            joystickUsed = false;
        }
        CombinationManagement("Left");
    }

    private void OnDAction(InputValue value)
    {
        if (input.currentControlScheme.Equals("GamepadScheme"))
        {
            validButton = true;
            joystickUsed = false;
        }
        CombinationManagement("Right");
    }

    private void OnWAction(InputValue value)
    {
        if (input.currentControlScheme.Equals("GamepadScheme"))
        {
            validButton = true;
            joystickUsed = false;
        }
        CombinationManagement("Up");
    }

    private void OnSAction(InputValue value)
    {
        if (input.currentControlScheme.Equals("GamepadScheme"))
        {
            validButton = true;
            joystickUsed = false;
        }
        CombinationManagement("Down");
    }

    void OnEscAction(InputValue value)
    {
        if (input.currentControlScheme.Equals("GamepadScheme")) { validButton = true; }
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
                    //Time.timeScale = 0;
                    timeCounter.DeactivateTimer();
                    input.SwitchCurrentActionMap("UIMap");
                }
                else
                {
                    //Time.timeScale = 1;
                    timeCounter.ActivateTimer();
                    input.SwitchCurrentActionMap("ActionMap");
                }
                hud.PauseGame(isPaused);

            }
        }
        else
        {
            if (inSettingsMenu)
            {
                hud.CloseSettingsMenu();
            }
        }

    }

    void OnLeftStick(InputValue value)
    {
        if (input.currentControlScheme.Equals("GamepadScheme"))
        {
            validButton = true;
            joystickUsed = true;
            /*print("valor del joystick: " + value.Get<Vector2>());
            print("last pos joystick: " + lastJoystickPos);*/

            if (joystickReseted && (value.Get<Vector2>().x > 0.85f) && (value.Get<Vector2>().y > -0.5f || value.Get<Vector2>().y < 0.5f))
            {
                if (!lastJoystickPos.Equals("Right"))
                {
                    //derecha
                    joystickUsed = true;
                    joystickReseted = false;
                    lastJoystickPos = "Right";
                    CombinationManagement(lastJoystickPos);
                }
            }
            else if (joystickReseted && (value.Get<Vector2>().x < -0.85f) && (value.Get<Vector2>().y > -0.5f || value.Get<Vector2>().y < 0.5f))
            {
                if (!lastJoystickPos.Equals("Left"))
                {
                    //izquierda
                    joystickUsed = true;
                    joystickReseted = false;
                    lastJoystickPos = "Left";
                    CombinationManagement(lastJoystickPos);
                }
            }
            else if (joystickReseted && (value.Get<Vector2>().y > 0.85f) && (value.Get<Vector2>().x > -0.5f || value.Get<Vector2>().x < 0.5f))
            {
                if (!lastJoystickPos.Equals("Up"))
                {
                    //arriba
                    joystickUsed = true;
                    joystickReseted = false;
                    lastJoystickPos = "Up";
                    CombinationManagement(lastJoystickPos);
                }
            }
            else if (joystickReseted && (value.Get<Vector2>().y < -0.85f) && (value.Get<Vector2>().x > -0.5f || value.Get<Vector2>().x < 0.5f))
            {
                if (!lastJoystickPos.Equals("Down"))
                {
                    //abajo
                    joystickUsed = true;
                    joystickReseted = false;
                    lastJoystickPos = "Down";
                    CombinationManagement(lastJoystickPos);
                }
            }
            else if (!joystickReseted && (value.Get<Vector2>().x > -0.15f || value.Get<Vector2>().x < 0.15f) && (value.Get<Vector2>().y > -0.15f || value.Get<Vector2>().y < 0.15f))
            {
                joystickReseted = true;
                //lastJoystickPos = "";
            }
        }
    }


    #endregion KeysActions

    public void Init(Transform pos)
    {
        newPosPlayer.transform.position = _initialNewPosPlayer;
        finishingRace = false;
        for (int i = 0; i < hudImages.Length; i++)
        {
            hudImages[i].gameObject.SetActive(true);
        }
        comboBackground.SetActive(true);
        initialPos = pos;
        transform.position = initialPos.position;
        transform.rotation = initialPos.rotation;
        availableKeys = new string[] { "Left", "Up", "Down", "Right", "Space" }; //this array maybe shoyuld be changed out of this script in order to have the possibility of rebinding keys
        gameStarted = false;
        isPaused = false;
        comboFailed = false;
        inSettingsMenu = false;


        input.SwitchCurrentActionMap("UIMap");
        if (input.currentControlScheme.Equals("KeyboardMouseScheme"))
        {
            previousScheme = Scheme.KeyboardMouse;
        }
        else if (input.currentControlScheme.Equals("GamepadScheme"))
        {
            previousScheme = Scheme.Gamepad;
        }
        buttonsPressed = 0;
        buttonPressing = false;
        validButton = false;
        joystickUsed = false;
        joystickReseted = true;
        lastJoystickPos = "";
        combosFinishedDDM = 0;
        velocityCombos = 0.0f;
        restartingComboText = false;
        endedCurrentCombo = false;
        endedCombos = 0;
        endedTotalCombos = 0;
        currentTime = 0.0f;
        combCreated = false;
        GenerateCombination();
        posComb = 0;
        correctSequence = 0;

    }
    public void StartGame()
    {
        gameStarted = true;
        input.SwitchCurrentActionMap("ActionMap");
    }
    public void EndGame()
    {
        gameStarted = false;
        DDM.SaveParameters();
    }

    public void UnPauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = 1;
        print("pausa quitada");
        timeCounter.ActivateTimer();
        input.SwitchCurrentActionMap("ActionMap");

        hud.PauseGame(isPaused);
    }

    public void SetInSettings(bool inSettings)
    {
        inSettingsMenu = inSettings;
    }
    private void GenerateCombination()
    {
        if (finishingRace) { return; }
        int keys = 3;//Random.Range(3, 6);
        comb = new string[keys];
        string temp = "";
        string[] tempArrows = new string[keys];
        //Debug.Log("Combinación a realizar");
        for (int i = 0; i < comb.Length; i++)
        {
            temp = (string)availableKeys[Random.Range(0, availableKeys.Length)];

            if (tempArrows.Contains(temp))
            {
                do
                {
                    temp = (string)availableKeys[Random.Range(0, availableKeys.Length)];
                }
                while (tempArrows.Contains(temp));
            }


            if (i >= 2)
            {
                if (temp.Equals(comb[i - 2]) && temp.Equals(comb[i - 1]))
                {
                    Debug.Log("Mucha repetición");
                    string aux;
                    do
                    {
                        aux = (string)availableKeys[Random.Range(0, availableKeys.Length)];
                    }
                    while (!aux.Equals(temp));
                    temp = aux;
                }
            }
            tempArrows[i] = temp;
            comb[i] = temp;
            Debug.Log(comb[i]);
        }
        ShowCombo();
        combCreated = true;
        endedCombos = 0;
        if (endedTotalCombos > 0)
        {
            ResetCorrect(false, false);
        }
        //ResetCorrect(false, false);
    }
    private void ShowCombo()
    {
        //string auxText = "";
        /*
        comboImages
        hudImages
        */
        for (int i = 0; i < comb.Length; i++)
        {
            switch (comb[i].ToString())
            {
                case "Left":
                    hudImages[i].sprite = comboImages[1];
                    //auxText += "←";
                    break;
                case "Up":
                    hudImages[i].sprite = comboImages[0];
                    //auxText += "↑";
                    break;
                case "Down":
                    hudImages[i].sprite = comboImages[3];
                    //auxText += "↓";
                    break;
                case "Right":
                    hudImages[i].sprite = comboImages[2];
                    //auxText += "→";
                    break;
                case "Space":
                    if (input.currentControlScheme.Equals("KeyboardMouseScheme"))
                    {
                        //auxText += "Space";
                        hudImages[i].sprite = comboImages[4];
                    }
                    else if (input.currentControlScheme.Equals("GamepadScheme"))
                    {
                        //auxText += "X";
                        hudImages[i].sprite = comboImages[6];
                    }
                    break;
            }
            /*comboText[i].color = Color.white;
            comboText[i].text = auxText;
            auxText = "";*/
            hudImages[i].color = Color.white;
        }
    }
    private void CombinationManagement(string keyPressed)
    {
        if (!combCreated || restartingComboText || !gameStarted || !timeCounter.GetActivatedTimer() || finishingRace) { return; }
        if (posComb < comb.Length)
        {
            if (comb[posComb].Equals(keyPressed))
            {
                //comboText[posComb].color = Color.red;
                hudImages[posComb].color = Color.red;
                correctSequence++;
                currentTime = 0.0f;
                if (correctSequence == comb.Length)
                {
                    ComboFinished();
                }
                else
                {
                    posComb++;
                }
            }
            else if (posComb == 0)
            {
                //no need to reset the sequence cause correct didnt started
            }
            else
            {
                ResetCorrect(false, true);
            }
        }
    }

    private void ComboFinished()
    {
        endedCombos++;
        endedTotalCombos++;
        combosFinishedDDM++;
        failedCombosDDM = 0;
        float ddmVel;
        if (velocityCombos < 1.0f)
        {
            ddmVel = 0.75f;
        }
        else if (velocityCombos > 3.0f)
        {
            ddmVel = 0.0f;
        }
        else
        {
            ddmVel = 0.5f;
        }
        DDM.SetValue(0, ddmVel);//VELOCITY
        velocityCombos = 0.0f;
        ResetCorrect(true, false);
    }

    private void Move()
    {
        /*if (finishingRace)
        {
            DisableComboPanel();
        }*/
        //Debug.Log("combinacion correcta");
        animator.SetTrigger("running");/*
        mov = Random.Range(0.25f, 0.33f);
        auxPos = (Vector3.forward * mov);
        newPos = new Vector3(transform.position.x + auxPos.x, transform.position.y + auxPos.y, transform.position.z + auxPos.z);*/
        //transform.position = Vector3.MoveTowards(transform.position, newPos, 0.75f);
        //transform.position = Vector3.Lerp(transform.position,newPos,0.5f);
        //Vector3 velocity = Vector3.zero;
        //transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, 0.5f);//Lerp(transform.position,newPos,0.5f);
        //print("La gráfica de la repetición tiene: " + DDM.GetValue(1));
        /*float aux = Random.Range(40.0f, 99.0f);
        //print("Random number: " + aux);
        if ((endedCombos >= 2) || (DDM.GetValue(1) > aux))//REPASAR
        {
            combCreated = false;
            GenerateCombination();
        }*/
        mov = Random.Range(0.24f, 0.27f);
        auxPos = (Vector3.forward * mov);
        newPos = new Vector3(transform.position.x + auxPos.x, transform.position.y + auxPos.y, transform.position.z + auxPos.z);
        //newPosPlayer.transform.position = newPos;        
        newPosPlayer.transform.position = new Vector3(newPosPlayer.transform.position.x, newPosPlayer.transform.position.y, newPosPlayer.transform.position.z + mov);
        StartCoroutine(Movement());

        float aux = Random.Range(40.0f, 99.0f);
        //print("Random number: " + aux);
        if ((endedCombos >= 2) || (DDM.GetValue(1) > aux))//REPASAR
        {
            combCreated = false;
            GenerateCombination();
        }

    }

    IEnumerator Movement()
    {
        while (transform.position != newPos)
        {
            float step = 0.35f * Time.fixedDeltaTime;
            transform.position = Vector3.MoveTowards(transform.position, newPos, step);
            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }

    private void ResetCorrect(bool ended, bool failedCombo)
    {
        if (posComb == 0 || finishingRace) { return; }
        restartingComboText = true;
        StopCoroutine(ResetShowedText());
        StartCoroutine(ResetShowedText());
        //Debug.Log("Secuencia reseteada");
        correctSequence = 0;
        posComb = 0;
        currentTime = 0.0f;

        if (ended)
        {
            endedCurrentCombo = true;
            DDM.SetValue(2, 1.0f);
        }
        else if (failedCombo && !endedCurrentCombo)
        {
            comboFailed = true;
            failedCombosDDM++;
            combosFinishedDDM = 0;
            DDM.SetValue(2, 0.0f);
        }

        if (failedCombosDDM >= 2)
        {
            DDM.SetValue(1, 0.0f);
            failedCombosDDM = 0;
            combosFinishedDDM = 0;
        }

        if (combosFinishedDDM >= 5)
        {
            DDM.SetValue(1, 1.0f);
            combosFinishedDDM = 0;
            failedCombosDDM = 0;
        }

        /*if ((failedCombosDDM + combosFinishedDDM) >= 5)
        {
            if (combosFinishedDDM < 5)
            {
                if (combosFinishedDDM < failedCombosDDM)
                {
                    DDM.SetValue(1, 0.0f);
                }
                else
                {
                    DDM.SetValue(1, 0.5f);
                }

            }
            else
            {
                DDM.SetValue(1, 1.0f);
            }
            failedCombosDDM = 0;
            combosFinishedDDM = 0;

        }*/
    }

    
    private void DisableComboPanel()
    {
        print("que llegas mi loco");
        gameStarted = false;
        for (int i = 0; i < hudImages.Length; i++)
        {
            hudImages[i].gameObject.SetActive(false);
        }
        comboBackground.SetActive(false);

    }

    public void NextMoveEnd()
    {
        finishingRace = true;
        DisableComboPanel();
    }

    public bool GetComboFailed()
    {
        return comboFailed;
    }

    private void ChangedSchemeText()
    {
        for (int i = 0; i < hudImages.Length; i++)
        {
            if (hudImages[i].sprite.Equals(comboImages[6]))
            {
                //comboText[i].text = "Space";
                hudImages[i].sprite = comboImages[4];//space
            }
            else if (hudImages[i].sprite.Equals(comboImages[4]))
            {
                //comboText[i].text = "X";
                hudImages[i].sprite = comboImages[6];//A
            }
        }
        /*for (int i = 0; i < comboText.Length; i++)
        {
            if (comboText[i].text.Equals("X"))
            {
                comboText[i].text = "Space";
            }
            else if (comboText[i].text.Equals("Space"))
            {
                comboText[i].text = "X";
            }
        }*/
    }

    private IEnumerator ResetShowedText()
    {
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < hudImages.Length; i++)
        {
            hudImages[i].color = Color.white;
        }
        if (endedCurrentCombo)
        {
            endedCurrentCombo = false;
            Move();
        }
        restartingComboText = false;
        lastJoystickPos = "";
    }
}
