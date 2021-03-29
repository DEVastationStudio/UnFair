using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleHoles : MonoBehaviour
{
    [SerializeField] private int points;
    [SerializeField] private HUD_Marbles hudMarbles;
    [SerializeField] private Thrower thrower;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Canica"))
        {
            hudMarbles.AddScore(this.points);
            Destroy(other.gameObject);
            if (thrower.GetBallsLeft() <= 0)
            {
                StopCoroutine(FinishGame());
                StartCoroutine(FinishGame());
            }
        }
    }

    private IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(0.35f);
        hudMarbles.EndGame();        
        StopCoroutine(FinishGame());        
    }
}
