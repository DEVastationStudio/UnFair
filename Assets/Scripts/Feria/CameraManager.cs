using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _virtualCameras;
    // Start is called before the first frame update
    void Start()
    {
        _virtualCameras[0].SetActive(true);
    }
}
