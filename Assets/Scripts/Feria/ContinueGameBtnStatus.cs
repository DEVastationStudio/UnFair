using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueGameBtnStatus : MonoBehaviour
{
    public Transform[] btns = new Transform[4];
    public Transform[] height = new Transform[3];
    
    private void Start()
    {
        if (PlayerPrefs.GetInt("PartidaEmpezada") == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                btns[i].position = height[i].position;
            }
            
            btns[3].gameObject.SetActive(false);
            btns[0].GetComponent<Button>().Select();
        }
        else
        {
            btns[3].GetComponent<Button>().Select();
        }
    }
}
