using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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
    private bool comboFailed;
    [SerializeField] private TimeCounter timeCounter;
    [SerializeField] private DynamicDifficultyManager DDM;
    private int combosFinishedDDM;
    private int failedCombosDDM;
    private float velocityCombos;
    #region UnityMethods

    void Awake()
    {
        availableKeys = new string[] { "Left", "Up", "Down", "Right", "Space" }; //this array maybe shoyuld be changed out of this script in order to have the possibility of rebinding keys
        gameStarted = false;
        comboFailed = false;
    }
    void Start()
    {
        if (input.currentControlScheme.Equals("KeyboardMouseScheme"))
        {
            previousScheme = Scheme.KeyboardMouse;
        }
        else if (input.currentControlScheme.Equals("GamepadScheme"))
        {
            previousScheme = Scheme.Gamepad;
        }
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
        FadeController.FinishLoad();
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
                || Keyboard.current[Key.LeftArrow].wasPressedThisFrame || Keyboard.current[Key.RightArrow].wasPressedThisFrame))
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
            if (combCreated && Gamepad.current.wasUpdatedThisFrame)
            {
                if (!(Gamepad.current[GamepadButton.DpadUp].wasPressedThisFrame || Gamepad.current[GamepadButton.DpadLeft].wasPressedThisFrame || Gamepad.current[GamepadButton.DpadDown].wasPressedThisFrame || Gamepad.current[GamepadButton.DpadRight].wasPressedThisFrame || Gamepad.current[GamepadButton.South].wasPressedThisFrame || Gamepad.current[GamepadButton.East].wasPressedThisFrame
                || Gamepad.current[GamepadButton.DpadUp].wasReleasedThisFrame || Gamepad.current[GamepadButton.DpadLeft].wasReleasedThisFrame || Gamepad.current[GamepadButton.DpadDown].wasReleasedThisFrame || Gamepad.current[GamepadButton.DpadRight].wasReleasedThisFrame || Gamepad.current[GamepadButton.South].wasReleasedThisFrame || Gamepad.current[GamepadButton.East].wasReleasedThisFrame))
                {
                    Debug.Log("Tecla ajena al conjunto de teclas creadas para el minijuego - MANDO");
                    ResetCorrect(false, true);
                    return;
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
        CombinationManagement("Space");
    }

    private void OnAAction(InputValue value)
    {
        CombinationManagement("Left");
    }

    private void OnDAction(InputValue value)
    {
        CombinationManagement("Right");
    }

    private void OnWAction(InputValue value)
    {
        CombinationManagement("Up");
    }

    private void OnSAction(InputValue value)
    {
        CombinationManagement("Down");
    }

    /*private void OnEAction(InputValue value)
    {
        CombinationManagement("E");
    }*/
    #endregion KeysActions
    public void StartGame()
    {
        gameStarted = true;
    }
    public void EndGame()
    {
        gameStarted = false;
        DDM.SaveParameters();
    }
    private void GenerateCombination()
    {
        int keys = 3;//Random.Range(3, 6);
        comb = new string[keys];
        string temp = "";
        //Debug.Log("Combinación a realizar");
        for (int i = 0; i < comb.Length; i++)
        {
            temp = (string)availableKeys[Random.Range(0, availableKeys.Length)];

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
        if (!combCreated || restartingComboText || !gameStarted || !timeCounter.GetActivatedTimer()) { return; }
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
        //Debug.Log("combinacion correcta");
        mov = Random.Range(0.25f, 0.33f);
        auxPos = (Vector3.forward * mov);
        newPos = new Vector3(transform.position.x + auxPos.x, transform.position.y + auxPos.y, transform.position.z + auxPos.z);
        transform.position = Vector3.MoveTowards(transform.position, newPos, 0.75f);
        print("La gráfica de la repetición tiene: " + DDM.GetValue(1));
        float aux = Random.Range(40.0f, 99.0f);
        print("Random number: " + aux);
        if ((endedCombos >= 2) || (DDM.GetValue(1) > aux))//REPASAR
        {
            combCreated = false;
            GenerateCombination();
        }
    }

    private void ResetCorrect(bool ended, bool failedCombo)
    {
        if (posComb == 0) { return; }
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
    }
}
