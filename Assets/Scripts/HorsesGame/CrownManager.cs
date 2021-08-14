using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownManager : MonoBehaviour
{
    [SerializeField] private GameObject crown;
    void Start()
    {
    }

    void Update()
    {

    }
    public void ActivateCrown()
    {
        crown.SetActive(true);
    }

    public void DeactivateCrown()
    {
        crown.SetActive(false);
    }
}
