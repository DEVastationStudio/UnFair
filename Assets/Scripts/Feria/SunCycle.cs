using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class SunCycle : MonoBehaviour
{
    [SerializeField] private Vector3[] _posDay;
    [SerializeField] private Vector3[] _rotDay;
    [SerializeField] private PostProcessVolume _postProcessVolume;
    [SerializeField] private Material _sky1;
    [SerializeField] private Material _sky2;
    [SerializeField] private Material _sky3;
    private ColorGrading _colorGrading;

    public void SetLight(int idx)
    {
        float temp = 0;
        switch (idx)
        {
            case 0:
                temp = -5;
                RenderSettings.skybox = _sky1;
                this.GetComponent<Light>().shadowStrength  = 0.277f;
                break;
            case 1:
                temp = -5;
                RenderSettings.skybox = _sky2;
                this.GetComponent<Light>().shadowStrength  = 0.277f;
                break;
            case 2:
                temp = -15;
                RenderSettings.skybox = _sky3;
                this.GetComponent<Light>().shadowStrength  = 0.0f;
                break;

        }
        ColorGrading tmp;
        if (_postProcessVolume.profile.TryGetSettings<ColorGrading>(out tmp))
        {
            _colorGrading = tmp;
        }
        _colorGrading.temperature.value = temp;
        transform.position = _posDay[idx];
        transform.rotation = Quaternion.Euler(_rotDay[idx]);
        Debug.Log("rot:" + _rotDay[idx]);
        //transform.LookAt(Vector3.zero);
    }
}
