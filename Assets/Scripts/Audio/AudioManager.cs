using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSrc;
    public AudioClip[] audioClips;
    
    private int curSong = -1;
    public static AudioManager instance;
    public UnityEngine.Audio.AudioMixer mixer;
    private bool _fadingIn;

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            /*audioSrc.clip = audioClips[0];
            audioSrc.Play();*/
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        mixer.SetFloat("Audio", ConvertToDecibel(PlayerPrefs.GetFloat("Audio", 1))); 
        mixer.SetFloat("Music", ConvertToDecibel(PlayerPrefs.GetFloat("Music", 0.5f))); 
        mixer.SetFloat("Sounds", ConvertToDecibel(PlayerPrefs.GetFloat("Sounds", 1))); 
    }
    public float ConvertToDecibel(float _value){
        return Mathf.Log10(Mathf.Max(_value, 0.0001f))*20f;
    }

    public void changeTheme(int index)
    {
        if (index != -1 && index != curSong)
        {
            curSong = index;
            StartCoroutine(AudioFade(audioClips[index], 0.2f, 0.2f, audioSrc));
        }
        else if (index == -1)
        {
            StartCoroutine(AudioFadeOut(audioSrc, 0.2f));
        }
    }
    private static IEnumerator AudioFade(AudioClip newClip, float speedOut, float speedIn, AudioSource auSrc)
    {
        yield return AudioFadeOut(auSrc, speedOut);
        
        yield return AudioFadeIn(auSrc, newClip, speedIn);
    }
    public void FadeOut(int index, float speed)
    {
        if (index != curSong)
        {
            StartCoroutine(AudioFadeOut(audioSrc, speed));
        }
    }
    public void FadeIn(int index, float speed)
    {
        if (index != curSong)
        {
            curSong = index;
            StartCoroutine(AudioFadeIn(audioSrc, audioClips[index], speed));
        }
    }
    private static IEnumerator AudioFadeOut(AudioSource auSrc, float speed) 
    {
        if (auSrc.clip == null) yield break;
        while (auSrc.volume > 0)
        {
            if (instance._fadingIn) yield break;
            auSrc.volume -= speed;
            yield return new WaitForSeconds(0.1f);
        }
        auSrc.Stop();
    }

    private static IEnumerator AudioFadeIn(AudioSource auSrc, AudioClip clip, float speed)
    {
        auSrc.clip = clip;
        if (clip == null) yield break;

        auSrc.Play();
        instance._fadingIn = true;
        while (auSrc.volume < 1)
        {
            auSrc.volume += speed;
            yield return new WaitForSeconds(0.1f);
        }
        instance._fadingIn = false;

        auSrc.volume = 1;
    }

    public void TempAudioFade(bool audioPlays)
    {
        StartCoroutine(TempFade(audioPlays));
    }

    public IEnumerator TempFade(bool audioPlays)
    {
        int vol1 = audioPlays ? 0 : 1;
        int vol2 = audioPlays ? 1 : 0;

        float time = 0;

        while (time < 1)
        {
            audioSrc.volume = Mathf.Lerp(vol1, vol2, time);print(audioSrc.volume);
            time += Time.deltaTime;
            yield return null;
        }
        audioSrc.volume = vol2;
    }

}
