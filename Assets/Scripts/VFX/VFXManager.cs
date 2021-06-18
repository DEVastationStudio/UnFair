using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private ShootingMinigameManager _gameManager;
    [SerializeField] private List<GameObject> _VFX;
    [SerializeField] private List<GameObject> _TRAILS;
    [SerializeField] private List<float> _VFXtime;

    public void InstantiateVFX(int i, Vector3 pos) 
    {
        StartCoroutine(InstantiateVFXCoroutine(i, pos));
    }

    public void InstantiateTrailFromAToB(Transform pA, Transform pB, int trail) 
    {
        StartCoroutine(InstantiateTrailFromAToBCoroutine(pA,pB,trail,1));
    }
    public IEnumerator InstantiateTrailFromAToBCoroutine(Transform pA, Transform pB, int trail, float time)
    {
        float timeStep = 0.016f;
        float actualT = 0;
        GameObject t = Instantiate(_TRAILS[trail], pA.position, pA.rotation);
        while (actualT <= 1) 
        {
            Debug.Log("Paso");
            t.transform.position = Vector3.Lerp(pA.position, pB.position, actualT);
            yield return new WaitForSeconds(timeStep);
            actualT += 0.016f*2;
        }
        _gameManager._letrasManager.CorrectLetterShoot();
        //Destroy(t);
        pA.GetComponent<Diana>()._activeLetter = -1;
    }

    public IEnumerator InstantiateVFXCoroutine(int i, Vector3 pos) 
    {
        GameObject obj = Instantiate(_VFX[i], pos, Quaternion.identity);
        yield return new WaitForSeconds(_VFXtime[i]);
        Destroy(obj);
    }
}
