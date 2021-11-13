using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    /// <summary>List of minigames:
    /// <para>Minigame 1: Tiro al blanco</para> 
    /// <para>Minigame 2: Horses Minigame</para> 
    /// <para>Minigame 3: Ducks</para> 
    /// <para>Minigame 4: Canicas</para> 
    /// </summary>
    public static void SetStars(int minigame, int stars)
    {
        int prevStars = GetStars(1) + GetStars(2) + GetStars(3) + GetStars(4);
        int oldStars = GetStars(minigame);
        if (stars < 0 || stars > 3)
        {
            Debug.LogError("Error: Stars must be between 0 and 3.");
        }
        else if (stars < PlayerPrefs.GetInt("Stars-" + minigame, 0))
        {
            Debug.Log("Attempted to set stars of minigame " + minigame + " to " + stars + ", but a higher star count was already achieved.");
        }
        else
        {
            PlayerPrefs.SetInt("Stars-" + minigame, stars);
            Debug.Log("Set stars of minigame " + minigame + " to " + stars + ".");
        }

        //Check speedrun stats
        if (oldStars == 0 && stars > 0)
        {
            switch (minigame)
            {
                case 1:
                    SpeedrunTimer.CompleteCheck(1);
                    break;
                case 2:
                    SpeedrunTimer.CompleteCheck(3);
                    break;
                case 3:
                    SpeedrunTimer.CompleteCheck(2);
                    break;
                case 4:
                    SpeedrunTimer.CompleteCheck(0);
                    break;
            }
        }
        int totalStars = GetStars(1) + GetStars(2) + GetStars(3) + GetStars(4);
        if (totalStars >= 8 && prevStars < 8) SpeedrunTimer.CompleteCheck(4);
        if (totalStars >= 12 && prevStars < 12) SpeedrunTimer.CompleteCheck(5);
    } 

    /// <summary>List of minigames:
    /// <para>Minigame 1: Tiro al blanco</para> 
    /// <para>Minigame 2: Horses Minigame</para> 
    /// <para>Minigame 3: Ducks</para> 
    /// <para>Minigame 4: Canicas</para> 
    /// </summary>
    public static int GetStars(int minigame)
    {
        return PlayerPrefs.GetInt("Stars-" + minigame, 0);
    }
}
