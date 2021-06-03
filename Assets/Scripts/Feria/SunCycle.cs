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
    private ColorGrading _colorGrading;

    public void SetLight(int idx)
    {
        float temp = 0;
        switch (idx)
        {
            case 0:
                temp = -5;
                break;
            case 1:
                temp = -5;
                break;
            case 2:
                temp = -15;
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
        transform.LookAt(Vector3.zero);
    }
}
