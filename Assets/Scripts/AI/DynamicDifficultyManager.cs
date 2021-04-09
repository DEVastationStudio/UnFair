using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDifficultyManager : MonoBehaviour
{
    public string minigame;
    [SerializeField] private float _skillLevel;
    public AnimationCurve[] curves;
    [Range(0.0f, 1.0f)] public float[] weights;
    [SerializeField] private float[] _values;
    [SerializeField] private int[] _storedValues;
    
    void Start()
    {
        _values = new float[weights.Length];
        _storedValues = new int[weights.Length];
        LoadParameters();
        ComputeSkillLevel();
    }

    public void SetValue(int index, float value)
    {
        float oldValue = _values[index];
        int numberOfValues = _storedValues[index];
        _values[index] = (oldValue * numberOfValues / (numberOfValues + 1)) + (value / (numberOfValues + 1));
        if (numberOfValues < 10) _storedValues[index]++;
        ComputeSkillLevel();
    }

    public float GetValue(int curve)
    {
        return curves[curve].Evaluate(_skillLevel);
    }

    private void ComputeSkillLevel()
    {
        _skillLevel = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            _skillLevel += (_values[i]*weights[i]);
        }
        print(_skillLevel);
    }

    private void LoadParameters()
    {
        for (int i = 0; i < weights.Length; i++)
        {
            _values[i] = PlayerPrefs.GetFloat("DD" + minigame + "v" + i, 0.5f);
            _storedValues[i] = PlayerPrefs.GetInt("DD" + minigame + "q" + i, 1);
        }
    }

    public void SaveParameters()
    {
        for (int i = 0; i < weights.Length; i++)
        {
            PlayerPrefs.SetFloat("DD" + minigame + "v" + i, _values[i]);
            PlayerPrefs.SetInt("DD" + minigame + "q" + i, _storedValues[i]);
        }
    }
}
