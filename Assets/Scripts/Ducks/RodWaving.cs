using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodWaving : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.localPosition = new Vector3(Mathf.Cos(2 * Time.timeSinceLevelLoad)/4, transform.localPosition.y, Mathf.Cos(5 * Time.timeSinceLevelLoad)/6);
    }
}
