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
    public int creditsSong;
    void Start()
    {
        creditsSong = 21;
        if (cutscene != null) {
           FadeController.FinishLoad();
           cutscene.StartConversation();
        }
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

    public void SetCreditsSong(int song)
    {
        creditsSong = song;
    }

    private IEnumerator Credits()
    {
        float time = 0;
        credits.SetActive(true);
        RectTransform rt = credits.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(rt.anchorMin.x, 1);
        rt.anchorMax = new Vector2(rt.anchorMax.x, 1);
        rt.pivot = new Vector2(rt.pivot.x, 0);
        Vector2 originalPos = new Vector2(rt.anchoredPosition.x, -rt.sizeDelta.y - 1080);
        Vector2 endPos = new Vector2(originalPos.x, 0);
        rt.anchoredPosition = originalPos;

        if (AudioManager.instance != null) AudioManager.instance.changeTheme(creditsSong);
        
        while (time < 60)
        {
            rt.anchoredPosition = Vector2.Lerp(originalPos, endPos, time/60);
            time += Time.deltaTime;
            yield return null;
        }
        rt.anchoredPosition = endPos;
    }
}
