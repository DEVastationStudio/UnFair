using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public NpcSpriteManager npcSpriteManager;
    public int Progression = 0;
    public void SetProgression()
    {
        print("Set Progression to " + Progression);
        PlayerPrefs.SetInt("Progression", Progression);
        if (Application.isPlaying) npcSpriteManager.UpdateSprites();
    }
    public int ProgAlt1 = 0;
    public void SetProgAlt1()
    {
        print("Set ProgAlt1 to " + ProgAlt1);
        PlayerPrefs.SetInt("ProgAlt1", ProgAlt1);
        if (Application.isPlaying) npcSpriteManager.UpdateSprites();
    }
    public int ProgAlt2= 0;
    public void SetProgAlt2()
    {
        print("Set ProgAlt2 to " + ProgAlt2);
        PlayerPrefs.SetInt("ProgAlt2", ProgAlt2);
        if (Application.isPlaying) npcSpriteManager.UpdateSprites();
    }
    public int Stars1 = 0;
    public int Stars2 = 0;
    public int Stars3 = 0;
    public int Stars4 = 0;
    public void SetStars(int minigame, int stars)
    {
        if (stars < 0 || stars > 3)
        {
            Debug.LogError("Error: Stars must be between 0 and 3.");
        }
        else
        {
            PlayerPrefs.SetInt("Stars-" + minigame, stars);
            Debug.Log("Set stars of minigame " + minigame + " to " + stars + ".");
        }
    } 
}
