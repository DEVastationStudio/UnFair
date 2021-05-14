using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorse : MonoBehaviour
{
    private float mov;
    private Vector3 newPos;
    private Vector3 aux;
    private bool gameStarted;
    [SerializeField] private DynamicDifficultyManager DDM;
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        gameStarted = false;
    }


    void Update()
    {
        if (!gameStarted) { return; }
        mov = Random.Range(0.05f, DDM.GetValue(0));
        GetPos();
    }

    private void FixedUpdate()
    {
        if (!gameStarted) { return; }
        float rand = Random.Range(0.0f, 15.0f);
        if (rand < 14.25f)
        {
            return;
        }
        animator.SetTrigger("running");
        transform.position = Vector3.MoveTowards(transform.position, newPos, Random.Range(0.05f, 0.08f));
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
}
