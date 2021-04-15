using UnityEngine;
public class VolumeSlider : MonoBehaviour {
    public UnityEngine.UI.Slider slider;
    public UnityEngine.Audio.AudioMixer mixer;
    public string parameterName;
    private float savedVol;
     
    void Awake(){
        float savedVol = PlayerPrefs.GetFloat(parameterName, 1);
        SetVolume(savedVol*slider.maxValue); 
        slider.value = savedVol*slider.maxValue;
        slider.onValueChanged.AddListener((float _) => SetVolume(_)); 
    }
     
    void SetVolume(float _value){
        mixer.SetFloat(parameterName, ConvertToDecibel(_value/slider.maxValue)); 
        PlayerPrefs.SetFloat(parameterName, _value/slider.maxValue);
        savedVol = _value/slider.maxValue;
    }
     
    public float ConvertToDecibel(float _value){
        return Mathf.Log10(Mathf.Max(_value, 0.0001f))*20f;
    }
 }