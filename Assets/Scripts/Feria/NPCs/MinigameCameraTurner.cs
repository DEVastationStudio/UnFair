using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameCameraTurner : MonoBehaviour
{
    [SerializeField] private GameObject _targetCamera;
    
    public void SwitchCamera(bool enabled)
    {
        _targetCamera.SetActive(enabled);
    }
    
}
