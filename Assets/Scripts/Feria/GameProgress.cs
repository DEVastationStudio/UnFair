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
        }
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
