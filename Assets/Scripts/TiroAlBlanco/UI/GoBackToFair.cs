using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackToFair : MonoBehaviour
{
    public void GoToFair(bool inFair = false) 
    {
        if (inFair)
        {
            FadeController.instance.player = null;
            FadeController.instance.storedPlayerPosition = false;
        }
        FadeController.Fade("Feria");
    }
}
