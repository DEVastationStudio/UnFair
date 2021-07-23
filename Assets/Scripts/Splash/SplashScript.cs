using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScript : MonoBehaviour
{
    public Image logo;
    public AudioSource sound;
    public AnimationCurve curve;
    public UnityEngine.Audio.AudioMixer mixer;
    private bool _canSkip;
    private bool finished;
    IEnumerator Start()
    {

        QualitySettings.vSyncCount = PlayerPrefs.GetInt("vSyncState");

        if (PlayerPrefs.GetInt("vSyncState") == 0)
        {
            if (PlayerPrefs.GetInt("fpsValue") == 0)
            {
                PlayerPrefs.SetFloat("Audio", 0.5f);
                PlayerPrefs.SetFloat("Sounds", 0.5f);
                PlayerPrefs.SetFloat("Music", 0.5f);
                mixer.SetFloat("Audio", ConvertToDecibel(PlayerPrefs.GetFloat("Audio")));
                mixer.SetFloat("Sounds", ConvertToDecibel(PlayerPrefs.GetFloat("Sounds")));
                mixer.SetFloat("Music", ConvertToDecibel(PlayerPrefs.GetFloat("Music")));
                Application.targetFrameRate = (int)Screen.currentResolution.refreshRate;
            }
            else if (PlayerPrefs.GetInt("fpsValue") == 241)
                Application.targetFrameRate = -1;
            else
                Application.targetFrameRate = PlayerPrefs.GetInt("fpsValue");
        }
        //Application.targetFrameRate = 60;

        _canSkip = (PlayerPrefs.GetInt("Progression", 0) != 0);


        float audioLength = sound.clip.length;
        //float fadeLength = audioLength*3/4;
        float elapsedTime = 0;
        float scale;
        Color tempColor;
        yield return new WaitForSeconds(1);
        sound.Play();
        while (elapsedTime < audioLength)
        {
            tempColor = logo.color;
            //tempColor.a = Mathf.Lerp(0, 1, elapsedTime/fadeLength);
            tempColor.a = curve.Evaluate(elapsedTime / audioLength);
            logo.color = tempColor;

            //scale = Mathf.Lerp(0.75f, 1.25f, elapsedTime/fadeLength);
            scale = tempColor.a * 0.5f + 0.75f;

            logo.transform.localScale = new Vector3(scale, scale, scale);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        while (sound.isPlaying)
        {
            yield return null;
        }
        elapsedTime = 0;
        if (!finished)
        {
            finished = true;
            while (elapsedTime < 1)
            {
                tempColor = logo.color;
                tempColor.a = Mathf.Lerp(1, 0, elapsedTime);
                logo.color = tempColor;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            FadeController.Fade("MinigameTestingMenu");
        }
    }

    private IEnumerator Skip()
    {
        if (finished || !_canSkip) yield break;
        finished = true;
        sound.Stop();
        Color tempColor;
        float elapsedTime = 0;

        while (elapsedTime < 1)
        {
            tempColor = logo.color;
            tempColor.a = Mathf.Lerp(1, 0, elapsedTime);
            logo.color = tempColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        FadeController.Fade("MinigameTestingMenu");
    }

    private void OnEscAction()
    {
        StartCoroutine(Skip());
    }
    private void OnSpaceAction()
    {
        StartCoroutine(Skip());
    }
    private void OnMouseLeftAction()
    {
        StartCoroutine(Skip());
    }
    public float ConvertToDecibel(float _value)
    {
        return Mathf.Log10(Mathf.Max(_value, 0.0001f)) * 20f;
    }
}
