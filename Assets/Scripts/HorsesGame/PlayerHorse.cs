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
    private float currentTime;
    private Vector3 newPos;
    private Vector3 auxPos;
    public string[] comb;
    [SerializeField] private string[] availableKeys;
    private int posComb;
    private int correctSequence;
    private bool combCreated;
    private int endedCombos;
    [SerializeField] private TextMeshProUGUI[] comboText;
    private bool endedCurrentCombo;
    private bool nonArrowKey;
    private bool restartingComboText;
    #region UnityMethods
    void Start()
    {
        if(input.currentControlScheme.Equals("KeyboardMouseScheme"))
        {
            previousScheme = Scheme.KeyboardMouse;
        }else if(input.currentControlScheme.Equals("GamepadScheme"))
        {
            previousScheme = Scheme.Gamepad;
        }
        restartingComboText  = false;
        nonArrowKey = false;
        endedCurrentCombo = false;
        endedCombos = 0;
        currentTime = 0.0f;
        combCreated = false;
        GenerateCombination();
        posComb = 0;
        correctSequence = 0;
    }

    void Update()
    {
        if (input.currentControlScheme.Equals("KeyboardMouseScheme"))
        {
            if (!endedCurrentCombo && nonArrowKey && previousScheme == Scheme.Gamepad)
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
                    ResetCorrect(false);
                    return;
                }
            }

        }
        else if (input.currentControlScheme.Equals("GamepadScheme"))
        {
            if (!endedCurrentCombo && nonArrowKey && previousScheme == Scheme.KeyboardMouse)
            {
                Debug.Log("De teclado a mando");
                previousScheme = Scheme.Gamepad;
                ChangedSchemeText();
            }
            if (combCreated && Gamepad.current.wasUpdatedThisFrame)
            {
                if (!(Gamepad.current[GamepadButton.DpadUp].wasPressedThisFrame || Gamepad.current[GamepadButton.DpadLeft].wasPressedThisFrame || Gamepad.current[GamepadButton.DpadDown].wasPressedThisFrame || Gamepad.current[GamepadButton.DpadRight].wasPressedThisFrame || Gamepad.current[GamepadButton.South].wasPressedThisFrame || Gamepad.current[GamepadButton.East].wasPressedThisFrame
                || Gamepad.current[GamepadButton.DpadUp].wasReleasedThisFrame || Gamepad.current[GamepadButton.DpadLeft].wasReleasedThisFrame || Gamepad.current[GamepadButton.DpadDown].wasReleasedThisFrame || Gamepad.current[GamepadButton.DpadRight].wasReleasedThisFrame || Gamepad.current[GamepadButton.South].wasReleasedThisFrame || Gamepad.current[GamepadButton.East].wasReleasedThisFrame || Gamepad.current[GamepadButton.LeftStick].wasPressedThisFrame || Gamepad.current[GamepadButton.LeftStick].wasReleasedThisFrame))
                {
                    Debug.Log("Tecla ajena al conjunto de teclas creadas para el minijuego - MANDO");
                    ResetCorrect(false);
                    return;
                }
            }
        }

        if (posComb <= 0) { return; }
        currentTime += Time.deltaTime;
        if (currentTime >= timeForKeys)
        {
            //Debug.Log("Mucho tiempo entre tecla y tecla");
            ResetCorrect(false);
        }
    }

    #endregion UnityMethods

    #region KeysActions
    private void OnSpaceAction(InputValue value)
    {
        CombinationManagement("Space");
    }

    private void OnAAction(InputValue value)
    {
        CombinationManagement("A");
    }

    private void OnDAction(InputValue value)
    {
        CombinationManagement("D");
    }

    private void OnWAction(InputValue value)
    {
        CombinationManagement("W");
    }

    private void OnSAction(InputValue value)
    {
        CombinationManagement("S");
    }

    private void OnEAction(InputValue value)
    {
        CombinationManagement("E");
    }
    #endregion KeysActions

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
        ResetCorrect(false);
    }
    private void ShowCombo()
    {
        nonArrowKey = false;
        string auxText = "";
        for (int i = 0; i < comb.Length; i++)
        {
            switch (comb[i].ToString())
            {
                case "A":
                    auxText += "←";
                    break;
                case "W":
                    auxText += "↑";
                    break;
                case "S":
                    auxText += "↓";
                    break;
                case "D":
                    auxText += "→";
                    break;
                case "Space":
                    if (input.currentControlScheme.Equals("KeyboardMouseScheme"))
                    {
                        auxText += "Space";
                    }
                    else if (input.currentControlScheme.Equals("GamepadScheme"))
                    {
                        auxText += "Circle";
                    }
                    nonArrowKey = true;
                    break;
            }
            comboText[i].color = Color.white;
            comboText[i].text = auxText;
            auxText = "";
        }
    }
    private void CombinationManagement(string keyPressed)
    {
        if (!combCreated || restartingComboText) { return; }
        if (posComb < comb.Length)
        {
            if (comb[posComb].Equals(keyPressed))
            {
                comboText[posComb].color = Color.red;
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
            else if(posComb==0)
            {
                //no need to reset the sequence cause correct didnt started
            }
            else
            {
                ResetCorrect(false);
            }
        }
    }

    private void ComboFinished()
    {
        endedCombos++;
        ResetCorrect(true);
    }

    private void Move()
    {
        //Debug.Log("combinacion correcta");
        mov = Random.Range(0.2f, 0.3f);
        auxPos = (Vector3.forward * mov);
        newPos = new Vector3(transform.position.x + auxPos.x, transform.position.y + auxPos.y, transform.position.z + auxPos.z);
        transform.position = Vector3.MoveTowards(transform.position, newPos, 0.75f);
        if (endedCombos >= 2 || Random.Range(0, 99) > 63)
        {
            combCreated = false;
            GenerateCombination();
        }
    }

    private void ResetCorrect(bool ended)
    {
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
        }
    }

    private void ChangedSchemeText()
    {
        for (int i = 0; i < comboText.Length; i++)
        {
            if (comboText[i].text.Equals("Circle"))
            {
                comboText[i].text = "Space";
            }
            else if (comboText[i].text.Equals("Space"))
            {
                comboText[i].text = "Circle";
            }
        }
    }

    private IEnumerator ResetShowedText()
    {
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < comboText.Length; i++)
        {
            comboText[i].color = Color.white;
        }
        if (endedCurrentCombo)
        {
            endedCurrentCombo = false;
            Move();
        }
        restartingComboText = false;
    }
}
