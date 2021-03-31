using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkylineBalls : MonoBehaviour
{
    private HUD_Marbles hud;
    private Thrower thrower;
    void Start()
    {
        hud = FindObjectOfType<HUD_Marbles>();
        thrower = FindObjectOfType<Thrower>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Canica"))
        {
            Destroy(other.gameObject);
            hud.SetFailBall();
             if (thrower.GetBallsLeft() <= 0)
            {
                StopCoroutine(FinishGame());
                StartCoroutine(FinishGame());
            }
            thrower.SetCanThrow();
        }
    }
    
    private IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(0.35f);
        hud.EndGame();        
        StopCoroutine(FinishGame());        
    }
}
