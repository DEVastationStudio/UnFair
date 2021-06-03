using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpriteManager : MonoBehaviour
{
    public NpcSprite[] sprites;

    void Start()
    {
        ConversationHelper.npcSpriteManager = this;
        UpdateSprites();
    }


    public void UpdateSprites()
    {
        int prog = PlayerPrefs.GetInt("Progression", 0);
        switch (prog)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                FindObjectOfType<SunCycle>().SetLight(0);
                break;
            case 6:
            case 7:
            case 8:
                FindObjectOfType<SunCycle>().SetLight(1);
                break;
            case 9:
            case 10:
            case 11:
            default:
                FindObjectOfType<SunCycle>().SetLight(2);
                break;
        }

        foreach (NpcSprite n in sprites)
        {
            if (n.checkMinigameStars)
            {
                int stars = 0;
                stars += PlayerPrefs.GetInt("Stars-1", 0);
                stars += PlayerPrefs.GetInt("Stars-2", 0);
                stars += PlayerPrefs.GetInt("Stars-3", 0);
                stars += PlayerPrefs.GetInt("Stars-4", 0);
                if (stars < 8)
                {
                    n.npcObject.SetActive(false);
                    continue;
                }
            }
            foreach (SpriteValuePair s in n.spriteValuePairs)
            {
                if (prog >= s.progressionValue)
                {
                    if (n.npc != null) n.npc.sprite = s.sprite;
                    n.npcObject.SetActive(!s.disableGameObject);
                }
                else
                {
                    break;
                }
            }
        }
    }
    void OnDestroy()
    {
        ConversationHelper.npcSpriteManager = null;
    }
}

[Serializable]
public class NpcSprite
{
    public GameObject npcObject;
    public SpriteRenderer npc;
    public SpriteValuePair[] spriteValuePairs;
    public bool checkMinigameStars;
}

[Serializable]
public class SpriteValuePair
{
    public Sprite sprite;
    public int progressionValue;
    public bool disableGameObject;
}