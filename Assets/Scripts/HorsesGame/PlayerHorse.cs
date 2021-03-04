using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class PlayerHorse : MonoBehaviour
{
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
    [SerializeField] private TextMeshProUGUI comboText;
    #region UnityMethods
    void Start()
    {
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
            if (combCreated && Keyboard.current.anyKey.wasPressedThisFrame)
            {
                if (!(Keyboard.current[Key.W].wasPressedThisFrame || Keyboard.current[Key.A].wasPressedThisFrame || Keyboard.current[Key.S].wasPressedThisFrame || Keyboard.current[Key.D].wasPressedThisFrame || Keyboard.current[Key.E].wasPressedThisFrame || Keyboard.current[Key.Space].wasPressedThisFrame))
                {
                    //Debug.Log("Tecla ajena al conjunto de teclas creadas para el minijuego");
                    ResetCorrect();
                    return;
                }
            }

        }
        else if (input.currentControlScheme.Equals("GamepadScheme"))
        {
            if (combCreated && Gamepad.current.wasUpdatedThisFrame)
            {
                if (!(Gamepad.current[GamepadButton.DpadUp].wasPressedThisFrame || Gamepad.current[GamepadButton.DpadLeft].wasPressedThisFrame || Gamepad.current[GamepadButton.DpadDown].wasPressedThisFrame || Gamepad.current[GamepadButton.DpadRight].wasPressedThisFrame || Gamepad.current[GamepadButton.South].wasPressedThisFrame || Gamepad.current[GamepadButton.East].wasPressedThisFrame
                || Gamepad.current[GamepadButton.DpadUp].wasReleasedThisFrame || Gamepad.current[GamepadButton.DpadLeft].wasReleasedThisFrame || Gamepad.current[GamepadButton.DpadDown].wasReleasedThisFrame || Gamepad.current[GamepadButton.DpadRight].wasReleasedThisFrame || Gamepad.current[GamepadButton.South].wasReleasedThisFrame || Gamepad.current[GamepadButton.East].wasReleasedThisFrame))
                {
                    Debug.Log("Tecla ajena al conjunto de teclas creadas para el minijuego - MANDO");
                    ResetCorrect();
                    return;
                }
            }
        }

        if (posComb <= 0) { return; }
        currentTime += Time.deltaTime;
        if (currentTime >= timeForKeys)
        {
            //Debug.Log("Mucho tiempo entre tecla y tecla");
            ResetCorrect();
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
        int keys = Random.Range(3, 6);
        comb = new string[keys];
        string temp = "";
        //Debug.Log("Combinación a realizar");
        for (int i = 0; i < comb.Length; i++)
        {
            temp = (string)availableKeys[Random.Range(0, 6)];

            if (i >= 2)
            {
                if (temp.Equals(comb[i - 2]) && temp.Equals(comb[i - 1]))
                {
                    Debug.Log("Mucha repetición");
                    string aux;
                    do
                    {
                        aux = (string)availableKeys[Random.Range(0, 6)];
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
        ResetCorrect();
    }
    private void ShowCombo()
    {
        //REVISAR
        string auxText = "";
        for (int i = 0; i < comb.Length; i++)
        {
            if (i < comb.Length - 1)
            {
                auxText += comb[i] + " + ";
            }
            else
            {
                auxText += comb[i];
            }

        }
        comboText.text = auxText;
    }
    private void CombinationManagement(string keyPressed)
    {
        if (!combCreated) { return; }
        if (posComb < comb.Length)
        {
            if (comb[posComb].Equals(keyPressed))
            {
                correctSequence++;
                currentTime = 0.0f;
                if (correctSequence == comb.Length)
                {
                    Move();
                }
                else
                {
                    posComb++;
                }
            }
            else
            {
                ResetCorrect();
            }
        }
    }

    private void Move()
    {
        endedCombos++;
        ResetCorrect();
        //Debug.Log("combinacion correcta");
        mov = Random.Range(0.1f, 0.2f);
        auxPos = (Vector3.forward * mov);
        newPos = new Vector3(transform.position.x + auxPos.x, transform.position.y + auxPos.y, transform.position.z + auxPos.z);
        transform.position = Vector3.MoveTowards(transform.position, newPos, 1);
        if (endedCombos > 2 || Random.Range(0, 99) > 65)
        {
            combCreated = false;
            GenerateCombination();
        }
    }

    private void ResetCorrect()
    {
        //Debug.Log("Secuencia reseteada");
        correctSequence = 0;
        posComb = 0;
        currentTime = 0.0f;
    }
}
