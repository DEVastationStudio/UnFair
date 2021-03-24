using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public static FadeController instance;
    public Image fade;
    private Color _fullColor = new Color(0,0,0,1);
    private Color _emptyColor = new Color(0,0,0,0);
    public Image loading;
    private int _loadingStage;

    public Vector3 lastPlayerPosition;
    public Vector2 lastPlayerDirection;

    public PlayerController player;
    public bool storedPlayerPosition;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static void Fade(string scene)
    {
        instance.StartCoroutine(instance.FadeOut(scene));
    }
    public static void FinishLoad(bool skipLoading = false)
    {
        instance.StartCoroutine(instance.FadeIn(skipLoading));
    }

    private IEnumerator FadeOut(string scene)
    {
        
        if (scene != "Feria" && player != null)
        {
            lastPlayerPosition = player.gameObject.transform.position;
            lastPlayerDirection = player.lastDir;
            storedPlayerPosition = true;
        }

        yield return FadeImageOut(fade);
        yield return FadeImageOut(loading);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(scene);
    }

    private IEnumerator FadeIn(bool skipLoading = false)
    {
        yield return new WaitForSeconds(0.5f);
        if (!skipLoading) yield return FadeImageIn(loading);
        yield return FadeImageIn(fade);
    }
    private IEnumerator FadeImageOut(Image image)
    {
        image.enabled = true;
        float elapsedTime = 0;
        Color tempColor;
        while (elapsedTime < 0.5f)
        {
            //image.color = Color.Lerp(_emptyColor, _fullColor, elapsedTime*2);
            tempColor = image.color;
            tempColor.a = Mathf.Lerp(0, 1, elapsedTime*2);
            image.color = tempColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        tempColor = image.color;
        tempColor.a = 1;
        image.color = tempColor;
    }

    private IEnumerator FadeImageIn(Image image)
    {
        image.enabled = true;
        float elapsedTime = 0;
        Color tempColor;
        while (elapsedTime < 0.5f)
        {
            //image.color = Color.Lerp(_fullColor, _emptyColor, elapsedTime*2);
            tempColor = image.color;
            tempColor.a = Mathf.Lerp(1, 0, elapsedTime*2);
            image.color = tempColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        image.enabled = false;
    }
}
