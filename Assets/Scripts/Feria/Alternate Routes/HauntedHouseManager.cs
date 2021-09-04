using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HauntedHouseManager : MonoBehaviour
{
    public ConversationHelper cutscene;
    public Image fade;
    public SpriteRenderer sprite;
    public Vector3 creditsPos1, creditsPos2;
    public GameObject credits;
    void Start()
    {
        FadeController.FinishLoad();
        if (cutscene != null) cutscene.StartConversation();
    }

    public void FadeOut()
    {
        StartCoroutine(ActivateFade(2));
    }

    public void FadeIn()
    {
        StartCoroutine(DeactivateFade(2));
    }

    private IEnumerator ActivateFade(int duration)
    {
        fade.enabled = true;
        float time = 0;
        Color oldCol = fade.color;
        while (time < duration)
        {
            fade.color = Color.Lerp(oldCol, Color.black, time/duration);
            time += Time.deltaTime;
            yield return null;
        }
        fade.color = Color.black;
    }
    private IEnumerator DeactivateFade(int duration)
    {
        float time = 0;
        Color oldCol = fade.color;
        while (time < duration)
        {
            fade.color = Color.Lerp(oldCol, Color.clear, time/duration);
            time += Time.deltaTime;
            yield return null;
        }
        fade.color = Color.clear;
        fade.enabled = false;
    }

    public void InstantFadeOut()
    {
        fade.enabled = true;
        fade.color = Color.black;
    }

    public void SlowFadeOut()
    {
        StartCoroutine(ActivateFade(8));
    }

    public void FadeCharacter(int duration)
    {
        StartCoroutine(FadeCharacterCR(duration));
    }

    private IEnumerator FadeCharacterCR(int duration)
    {
        float time = 0;
        Color oldCol = sprite.color;
        while (time < duration)
        {
            sprite.color = Color.Lerp(oldCol, Color.clear, time/duration);
            time += Time.deltaTime;
            yield return null;
        }
        sprite.color = Color.clear;
        sprite.enabled = false;
    }

    public void StartCredits()
    {
        StartCoroutine(Credits());
    }

    private IEnumerator Credits()
    {
        float time = 0;
        credits.SetActive(true);
        Vector3 temp;
        
        while (time < 60)
        {
            temp = Vector3.Lerp(creditsPos1, creditsPos2, time/60);
            credits.transform.position = new Vector3(credits.transform.position.x, temp.y, credits.transform.position.z);
            time += Time.deltaTime;
            yield return null;
        }
        credits.transform.position = creditsPos2;
    }
}
