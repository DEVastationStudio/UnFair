using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class StarsManager : MonoBehaviour
{
    void Start() 
    {
        int numStars = 0;
        numStars += GameProgress.GetStars(1);
        numStars += GameProgress.GetStars(2);
        numStars += GameProgress.GetStars(3);
        numStars += GameProgress.GetStars(4);

        DialogueLua.SetVariable("_stars", numStars);

        if(GameProgress.GetStars(1) > 0 && GameProgress.GetStars(2) > 0 && GameProgress.GetStars(3) > 0 && GameProgress.GetStars(4) > 0) 
            DialogueLua.SetVariable("_allPlayed", true);
        else
            DialogueLua.SetVariable("_allPlayed", false);

        Debug.Log("[-----------Progress Update-----------]");
        Debug.Log("Stars: " + DialogueLua.GetVariable("_stars").asString);
        Debug.Log("All minigames played?: " + DialogueLua.GetVariable("_allPlayed").asString);
        Debug.Log("[-----------Progress Update-----------]");
    }
}
