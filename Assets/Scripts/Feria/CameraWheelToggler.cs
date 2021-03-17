using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWheelToggler : MonoBehaviour
{
    public GameObject vc1, vc2;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            vc1.SetActive(false);
            vc2.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {
            vc1.SetActive(true);
            vc2.SetActive(false);
        }
    }
}
