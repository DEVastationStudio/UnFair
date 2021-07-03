using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _sprite;
    private int _shotType;

    void Start()
    {
        _shotType = Random.Range(0, _sprite.Length-1);
        _sprite[_shotType].gameObject.SetActive(true);
        StartCoroutine(Disapear());
    }

    IEnumerator Disapear() 
    {
        yield return new WaitForSeconds(1);
        Color aux = _sprite[_shotType].color;
        float count = 0;
        while (count < 1) 
        {
            yield return new WaitForSeconds(0.05f);
            aux.a -= 0.05f;
            count += 0.05f;
            _sprite[_shotType].color = aux;
        }
        Destroy(gameObject);
    }
}
