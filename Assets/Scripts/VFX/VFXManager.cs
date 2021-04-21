using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _VFX;
    [SerializeField] private List<float> _VFXtime;

    public void InstantiateVFX(int i, Vector3 pos) 
    {
        StartCoroutine(InstantiateVFXCoroutine(i, pos));
    }

    public IEnumerator InstantiateVFXCoroutine(int i, Vector3 pos) 
    {
        GameObject obj = Instantiate(_VFX[i], pos, Quaternion.identity);
        yield return new WaitForSeconds(_VFXtime[i]);
        Destroy(obj);
    }
}
