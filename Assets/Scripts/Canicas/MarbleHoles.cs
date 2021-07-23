using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleHoles : MonoBehaviour
{
    [SerializeField] private int points;
    [SerializeField] private HUD_Marbles hudMarbles;
    [SerializeField] private Thrower thrower;
    [SerializeField] private VFXManager vfxManager;
    private DynamicDifficultyManager DDM;

    void Start()
    {
        DDM = FindObjectOfType<DynamicDifficultyManager>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Canica"))
        {
            DDM.SetValue(2, 0.0f);
            hudMarbles.AddScore(this.points);
            //Spawnear +15 --->
            vfxManager.InstantiateVFX(0, 
                new Vector3( 
                    other.gameObject.transform.position.x, 
                    other.gameObject.transform.position.y + 0.4f, 
                    other.gameObject.transform.position.z)
                );
            //<--- EndSpawn
            Destroy(other.gameObject);
            if (thrower.GetBallsLeft() <= 0)
            {
                StopCoroutine(FinishGame());
                StartCoroutine(FinishGame());
            }
            //thrower.SetCanThrow();
        }
    }

    private IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(0.35f);
        hudMarbles.EndGame();        
        StopCoroutine(FinishGame());        
    }
}
