using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    void Start()
    {
        StartCoroutine(Disapear());
    }

    IEnumerator Disapear() 
    {
        yield return new WaitForSeconds(1);
        Color aux = _sprite.color;
        float count = 0;
        while (count < 1) 
        {
            yield return new WaitForSeconds(0.05f);
            aux.a -= 0.05f;
            count += 0.05f;
            _sprite.color = aux;
        }
        Destroy(gameObject);
    }
}
