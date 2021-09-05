using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpriteManager : MonoBehaviour
{
    public NpcSprite[] sprites;
    [SerializeField] private Color[] spritesColor;
    public SpriteRenderer[] tintSprites;

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
                ChangeSpritesColors(0);//FindObjectOfType<SunCycle>().SetLight(0);
                break;
            case 4:
                if (PlayerPrefs.GetInt("Stars-1", 0) > 0)
                    ChangeSpritesColors(1);//FindObjectOfType<SunCycle>().SetLight(1);
                else
                    ChangeSpritesColors(0);//FindObjectOfType<SunCycle>().SetLight(0);
                break;
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
                if (PlayerPrefs.GetInt("Stars-2", 0) > 0)
                    ChangeSpritesColors(2);//FindObjectOfType<SunCycle>().SetLight(2);
                else
                    ChangeSpritesColors(1);//FindObjectOfType<SunCycle>().SetLight(1);
                break;
            case 10:
            case 11:
            default:
                ChangeSpritesColors(2);//FindObjectOfType<SunCycle>().SetLight(2);
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

    private void ChangeSpritesColors(int idx)
    {

        FindObjectOfType<SunCycle>().SetLight(idx);
        foreach (SpriteRenderer npc in tintSprites)
        {
            npc.color = spritesColor[idx];
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