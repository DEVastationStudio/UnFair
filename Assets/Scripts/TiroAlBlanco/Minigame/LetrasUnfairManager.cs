using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetrasUnfairManager : MonoBehaviour
{
    public List<GameObject> Letras;
    public List<GameObject> Guiones;
    [SerializeField] private Color _goldColor;
    [SerializeField] private Color _whiteColor;
    [SerializeField] private ShootingMinigameManager _gameManager;
    [SerializeField] private Slider _barraLetras;

    public void ShowLetter(int i) 
    {
        Letras[i].SetActive(true);
        Guiones[i].SetActive(false);
    }

    public void HideLetter(int i)
    {
        Letras[i].SetActive(false);
        Guiones[i].SetActive(true);
    }

    public void GoldRush() 
    {
        StartCoroutine(WinningUnfair());
    }

    IEnumerator WinningUnfair() 
    {
        foreach (GameObject g in Letras) 
        {
            SpriteRenderer s = g.GetComponent<SpriteRenderer>();
            Color c = s.color;
            c = _goldColor;
            s.color = c;
        }
        _gameManager._spawnerDianas._isOnGoldRush = true;
        _gameManager._spawnerDianas._activeLetter = true;
        _barraLetras.value = 100;
        _barraLetras.gameObject.SetActive(true);
        float tBase = 0.01f;
        float tMax = 100f;
        while (_barraLetras.value > 0) 
        {
            yield return new WaitForSeconds(tBase);
            _barraLetras.value -= Mathf.Clamp((tMax/4)*tBase, 0, 100);
        }
        _barraLetras.gameObject.SetActive(false);
        _barraLetras.value = 100;
        _gameManager._spawnerDianas._isOnGoldRush = false;
        _gameManager._spawnerDianas._activeLetter = false;
        int count = 0;
        foreach (GameObject g in Letras)
        {
            SpriteRenderer s = g.GetComponent<SpriteRenderer>();
            Color c = s.color;
            c = _whiteColor;
            s.color = c;
            HideLetter(count);
            count++;
        }
        _gameManager._spawnerDianas._currentLetter = 0;
    }

}
