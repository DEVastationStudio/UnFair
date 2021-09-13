using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleHoles : MonoBehaviour
{
    [SerializeField] private bool isBasket = false;
    private int points = 15;
    [SerializeField] private HUD_Marbles hudMarbles;
    [SerializeField] private Thrower thrower;
    [SerializeField] private VFXManager vfxManager;
    [SerializeField] private TotalBallsCounter _totalBallsCounter;
    [SerializeField] private Transform _vfxSpawnPoint;
    private DynamicDifficultyManager DDM;
    private MarblesLogSystem _logSystem;
    [SerializeField] private AudioSource _goodSfx;

    void Start()
    {
        DDM = FindObjectOfType<DynamicDifficultyManager>();
        _logSystem = FindObjectOfType<MarblesLogSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Canica"))
        {
            _goodSfx.Play();
            if (isBasket)
            {
                hudMarbles.AddScore(this.points > 5 ? (this.points * 2) - 5 : (this.points * 2));
                hudMarbles.hitBasket = true;

                vfxManager.InstantiateVFX(1,
                    new Vector3(
                        _vfxSpawnPoint.position.x,
                        _vfxSpawnPoint.position.y + 0.4f,
                        _vfxSpawnPoint.position.z)
                /*other.gameObject.transform.position.x,
                other.gameObject.transform.position.y + 0.4f,
                other.gameObject.transform.position.z)*/
                );
                DDM.SetValue(0, 1.0f);
                _logSystem._BH += 1;
            }
            else
            {
                hudMarbles.AddScore(this.points);

                vfxManager.InstantiateVFX(0,
                    new Vector3(
                        _vfxSpawnPoint.position.x,
                        _vfxSpawnPoint.position.y + 0.4f,
                        _vfxSpawnPoint.position.z)
                /*other.gameObject.transform.position.x,
                other.gameObject.transform.position.y + 0.4f,
                other.gameObject.transform.position.z)*/
                );
                DDM.SetValue(1, 1.0f);
                _logSystem._NH += 1;
            }
            Destroy(other.gameObject);
            _totalBallsCounter.ReduceBalls();
            if (_totalBallsCounter.GetBalls() <= 0/*thrower.GetBallsLeft() <= 0*/)
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
