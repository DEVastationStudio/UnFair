using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HauntedHouseManager : MonoBehaviour
{
    public ConversationHelper cutscene;
    public Image fade;
    void Start()
    {
        FadeController.FinishLoad();
        cutscene.StartConversation();
    }

    public void FadeOut()
    {
        StartCoroutine(ActivateFade());
    }

    public void FadeIn()
    {
        StartCoroutine(DeactivateFade());
    }

    private IEnumerator ActivateFade()
    {
        fade.enabled = true;
        float time = 0;
        while (time < 2)
        {
            fade.color = Color.Lerp(fade.color, Color.black, time/2);
            time += Time.deltaTime;
            yield return null;
        }
        fade.color = Color.black;
    }
    private IEnumerator DeactivateFade()
    {
        float time = 0;
        while (time < 2)
        {
            fade.color = Color.Lerp(fade.color, Color.clear, time/2);
            time += Time.deltaTime;
            yield return null;
        }
        fade.color = Color.clear;
        fade.enabled = false;
    }
}
