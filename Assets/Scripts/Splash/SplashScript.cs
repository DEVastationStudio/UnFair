using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScript : MonoBehaviour
{
    public Image logo;
    public AudioSource sound;
    public AnimationCurve curve;
    IEnumerator Start()
    {
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
            tempColor.a = curve.Evaluate(elapsedTime/audioLength);
            logo.color = tempColor;

            //scale = Mathf.Lerp(0.75f, 1.25f, elapsedTime/fadeLength);
            scale = tempColor.a*0.5f + 0.75f;

            logo.transform.localScale = new Vector3(scale, scale, scale);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        while (sound.isPlaying)
        {
            yield return null;
        }
        elapsedTime = 0;
        while (elapsedTime < 1)
        {
            tempColor = logo.color;
            tempColor.a = Mathf.Lerp(1, 0, elapsedTime);
            logo.color = tempColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        FadeController.Fade("Feria");

    }
    
}
