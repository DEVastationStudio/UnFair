using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalBallsCounter : MonoBehaviour
{
    private int throwerBalls;

    public void SetThrowerBalls(int balls)
    {
        throwerBalls = balls;
    }

    public void ReduceBalls()
    {
        throwerBalls--;
    }

    public int GetBalls()
    {
        return throwerBalls;
    }
}
