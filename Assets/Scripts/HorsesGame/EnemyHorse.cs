using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorse : MonoBehaviour
{
    private float mov;
    private Vector3 newPos;
    private Vector3 aux;
    private bool gameStarted;
    private bool moving;
    [SerializeField] private DynamicDifficultyManager DDM;
    private Animator animator;
    private Transform initialPos;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        //Init();
    }


    void Update()
    {
        if (!gameStarted) { return; }
        //mov = Random.Range(0.05f, DDM.GetValue(0));
        //GetPos();
    }

    private void FixedUpdate()
    {
        if (!gameStarted || moving) { return; }
        float rand = Random.Range(0.0f, 15.0f);
        if (rand < 14.25f)
        {
            return;
        }
        moving = true;
        animator.SetTrigger("running");
        StartCoroutine(Movement());
    }

    public void Init(Transform pos)
    {
        initialPos = pos;
        gameStarted = false;
        moving = false;
        transform.position = initialPos.position;
        transform.rotation = initialPos.rotation;
    }

    public void StartGame()
    {
        gameStarted = true;
    }
    public void EndGame()
    {
        gameStarted = false;
    }
    private void GetPos()
    {
        aux = (Vector3.forward * mov);
        newPos = new Vector3(transform.position.x + aux.x, transform.position.y + aux.y, transform.position.z + aux.z);
    }

    IEnumerator Movement()
    {
        mov = Random.Range(0.05f, DDM.GetValue(0));//cambiar esto a la velocdidad más que a la posición porque ahora no tiene sentido
        aux = (Vector3.forward * mov);
        newPos = new Vector3(transform.position.x + aux.x, transform.position.y + aux.y, transform.position.z + aux.z);
        while (transform.position != newPos)
        {
            float step = Random.Range(0.20f, 0.24f/*0.05f, 0.08f*/) * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, newPos, step);
            yield return new WaitForFixedUpdate();
        }
        moving = false;
        yield return null;

    }
}
