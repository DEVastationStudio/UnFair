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
    //Set Stars
}
