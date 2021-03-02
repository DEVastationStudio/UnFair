using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHorse : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    private float mov;
    [SerializeField] private float timeForKeys = 1.5f;
    private float currentTime;
    private Vector3 newPos;
    private Vector3 auxPos;
    private bool keyJustPressed;
    private bool availableKeyPressed;
    public string[] comb;
    [SerializeField] private string[] availableKeys;
    private int posComb;
    private int correctSequence;
    private bool combCreated;

    #region UnityMethods
    void Start()
    {
        currentTime = 0.0f;
        combCreated = false;
        keyJustPressed = false;
        availableKeyPressed = false;
        GenerateCombination();
        posComb = 0;
        correctSequence = 0;
    }

    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            if (!(Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.eKey.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame))
            {
                //Debug.Log("Tecla ajena al conjunto de teclas creadas para el minijuego");
                ResetCorrect();
                return;
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

            if (i > 2)
            {
                if (temp.Equals(comb[i - 2]) && temp.Equals(comb[i - 1]))
                {
                    //Debug.Log("Mucha repetición");
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
        combCreated = true;
    }

    private void CombinationManagement(string keyPressed)
    {
        if (!combCreated) { return; }
        availableKeyPressed = true;
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
        availableKeyPressed = false;
    }

    private void Move()
    {
        ResetCorrect();
        //Debug.Log("combinacion correcta");
        mov = Random.Range(0.1f, 0.2f);
        auxPos = (Vector3.forward * mov);
        newPos = new Vector3(transform.position.x + auxPos.x, transform.position.y + auxPos.y, transform.position.z + auxPos.z);
        transform.position = Vector3.MoveTowards(transform.position, newPos, 1);
    }

    private void ResetCorrect()
    {
        //Debug.Log("Secuencia reseteada");
        correctSequence = 0;
        posComb = 0;
        currentTime = 0.0f;
    }
}
