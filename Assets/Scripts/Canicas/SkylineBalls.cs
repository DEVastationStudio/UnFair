using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkylineBalls : MonoBehaviour
{
    private TotalBallsCounter _totalBallsCounter;
    private HUD_Marbles hud;
    private Thrower thrower;
    private DynamicDifficultyManager DDM;
    void Start()
    {
        DDM = FindObjectOfType<DynamicDifficultyManager>();
        hud = FindObjectOfType<HUD_Marbles>();
        thrower = FindObjectOfType<Thrower>();
        _totalBallsCounter = FindObjectOfType<TotalBallsCounter>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Canica"))
        {
            //DDM.SetValue(2, 1.0f);
            Destroy(other.gameObject);
            thrower.missedMarbles++;
            //hud.SetFailBall();
            _totalBallsCounter.ReduceBalls();
            if (_totalBallsCounter.GetBalls() <= 0/*thrower.GetBallsLeft() <= 0*/)
            {
                StopCoroutine(FinishGame());
                StartCoroutine(FinishGame());
            }
            //thrower.SetCanThrow();
        }
    }

    private IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(0.35f);
        hud.EndGame();
        StopCoroutine(FinishGame());
    }
}
