using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHorse : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    private float mov;
    private Vector3 newPos;
    private Vector3 aux;
    private bool pulsado;
    public string[] comb;
    [SerializeField] private string[] availableKeys;
    private int posComb;
    private int correctSequence;
    private bool combCreated;
    void Start()
    {
        combCreated = false;
        pulsado = false;
        GenerateCombination();
        posComb = 0;
        correctSequence = 0;
        for (int i = 0; i < 6; i++)
        {
            //Debug.Log(availableKeys[i]);
        }
    }

    private void GenerateCombination()
    {
        /*comb = new string[3];
        Debug.Log(comb.Length);
        comb[0] = "A";
        comb[1] = "D";
        comb[2] = "Space";*/
        int keys = Random.Range(3,6);
        comb = new string[keys];
        string temp = "";
        Debug.Log("Combinación a realizar");
        for(int i = 0; i< comb.Length; i++)
        {
            temp = (string)availableKeys[Random.Range(0,6)];

            //comprobar que la combinacion no use mas de 2 veces seguidas la misma tecla
            if(i>2){
                //for(int j = i-2; j< i; j++){
                    if(temp.Equals(comb[i-2]) && temp.Equals(comb[i-1])){
                        Debug.Log("Mucha repetición");
                        string aux;
                        do{
                            aux = (string)availableKeys[Random.Range(0,6)];
                        }
                        while(!aux.Equals(temp));
                        temp = aux;
                    }
                //}
            }
            //
            comb[i] = temp;
            Debug.Log(comb[i]);
        }
        
        /*Debug.Log(comb[0]);
        Debug.Log(comb[1]);
        Debug.Log(comb[2]);*/
        combCreated = true;
    }

    private void CombinationManagement(string keyPressed)
    {
        if(!combCreated){ return;}
        if(posComb<comb.Length){
            if(comb[posComb].Equals(keyPressed)){
                correctSequence++;
                if(correctSequence==comb.Length){
                    Move();
                }else{
                    posComb ++;
                }

            }else{
                correctSequence = 0;
                posComb = 0;
            }
        }    
        
    }
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
    private void Move()
    {
        correctSequence = 0;
        posComb = 0;
        Debug.Log("combinacion correcta");
        mov = Random.Range(0.1f, 0.2f);
        aux = (Vector3.forward * mov);
        newPos = new Vector3(transform.position.x+aux.x, transform.position.y + aux.y, transform.position.z + aux.z);
        transform.position = Vector3.MoveTowards(transform.position, newPos, 1);
        pulsado = true;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (pulsado)
        {
            //transform.position = Vector3.MoveTowards(transform.position, newPos, 1);            
            pulsado = false;
        }
    }

}
