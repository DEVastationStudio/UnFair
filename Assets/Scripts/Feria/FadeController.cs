using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public Image fade;
    private Color _fullColor = new Color(0,0,0,1);
    private Color _emptyColor = new Color(0,0,0,0);
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void Fade(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    private IEnumerator FadeOut(string scene)
    {
        fade.enabled = true;
        float elapsedTime = 0;
        while (elapsedTime < 0.5f)
        {
            fade.color = Color.Lerp(_emptyColor, _fullColor, elapsedTime*2);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fade.color = _fullColor;
        SceneManager.LoadScene(scene);
    }

    private IEnumerator FadeIn()
    {
        fade.enabled = true;
        float elapsedTime = 0;
        while (elapsedTime < 0.5f)
        {
            fade.color = Color.Lerp(_fullColor, _emptyColor, elapsedTime*2);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fade.enabled = false;
    }
}
