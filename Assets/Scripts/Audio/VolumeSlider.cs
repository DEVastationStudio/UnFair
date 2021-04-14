using UnityEngine;
public class VolumeSlider : MonoBehaviour {
    public UnityEngine.UI.Slider slider;
    public UnityEngine.Audio.AudioMixer mixer;
    public string parameterName;
    private float savedVol;
     
    void Awake(){
        float savedVol = PlayerPrefs.GetFloat(parameterName, slider.maxValue);
        SetVolume(savedVol); 
        slider.value = savedVol;
        slider.onValueChanged.AddListener((float _) => SetVolume(_)); 
    }
     
    void SetVolume(float _value){
        mixer.SetFloat(parameterName, ConvertToDecibel(_value/slider.maxValue)); 
        PlayerPrefs.SetFloat(parameterName, _value);
        savedVol = _value;
    }
     
    public float ConvertToDecibel(float _value){
        return Mathf.Log10(Mathf.Max(_value, 0.0001f))*20f;
    }
 }