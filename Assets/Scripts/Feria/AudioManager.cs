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

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            audioSrc.clip = audioClips[0];
            audioSrc.Play();
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        mixer.SetFloat("Audio", ConvertToDecibel(PlayerPrefs.GetFloat("Audio", 0.5f))); 
        mixer.SetFloat("Sounds", ConvertToDecibel(PlayerPrefs.GetFloat("Sounds", 1))); 
    }
    public float ConvertToDecibel(float _value){
        return Mathf.Log10(Mathf.Max(_value, 0.0001f))*20f;
    }

    public void changeTheme(int index)
    {
        if (index != curSong)
        {
            curSong = index;
            StartCoroutine(AudioFade(audioClips[index], 0.75f, audioSrc));
        }
    }
    static IEnumerator AudioFade(AudioClip newClip, float speed, AudioSource auSrc)
    {
        if (auSrc.clip != null)
        {
            while (AudioListener.volume > 0)
            {
                AudioListener.volume -= speed;
                yield return new WaitForSeconds(0.1f);
            }
            auSrc.Stop();
        }
        auSrc.clip = newClip;
        if (newClip == null) yield break;
        auSrc.Play();
        while (AudioListener.volume < 1)
        {
            AudioListener.volume += speed;
            yield return new WaitForSeconds(0.1f);
        }
        AudioListener.volume = 1;
    }
}
