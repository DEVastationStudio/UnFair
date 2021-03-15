using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodAiScript : MonoBehaviour
{
    public GameObject rodTip;
    public Vector3 positionOffset;
    private float _initialHeight;
    private const int _heightOffset = 1;
    private float _height;
    public BoxCollider magnetHitbox;
    public GameObject magnet;
    private Vector3 _targetPos;
    public DucksGameManager gameManager;
    public GameObject basket;
    public LayerMask ducksLayer;
    private Collider[] nearDuckColliders;
    private Duck[] nearDucks;
    private Vector3[] futurePositions;
    private float[] weights;
    private Vector3 futureMagnetPos;
    public Rigidbody magnetRB;
    void Start()
    {
        positionOffset = transform.position - rodTip.transform.position;
        positionOffset.y = 0;
        _height = 0;
        _initialHeight = transform.position.y;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawSphere(magnet.transform.position, 5);
    }

    void Update()
    {
        //Select Target Position
        if (magnet.tag == "Magnet")
        {
            //Get all eligible ducks in a small area
            nearDuckColliders = Physics.OverlapSphere(magnet.transform.position, 5, ducksLayer);

            if (nearDuckColliders.Length == 0)
            {
                _targetPos = Vector3.zero;
            }
            else
            {
                nearDucks = new Duck[nearDuckColliders.Length];
                futurePositions = new Vector3[nearDuckColliders.Length];
                weights = new float[nearDuckColliders.Length];

                for (int i = 0; i < nearDuckColliders.Length; i++)
                {
                    nearDucks[i] = nearDuckColliders[i].GetComponent<Duck>();
                }

                //Calculate future positions based on rigidbody speed
                for (int i = 0; i < nearDuckColliders.Length; i++)
                {
                    futurePositions[i] = nearDucks[i].transform.position + nearDucks[i].rigidBody.velocity * Time.deltaTime;
                }

                //Calculate future position of the magnet
                futureMagnetPos = magnet.transform.position + magnetRB.velocity * Time.deltaTime;

                //Calculate weight of all ducks (based on distance and duck type)
                float modifier = 1;
                for (int i = 0; i < nearDuckColliders.Length; i++)
                {
                    switch (nearDucks[i].type)
                    {
                        case Duck.Type.NORMAL:
                            modifier = 1;
                            break;
                        case Duck.Type.AI:
                            modifier = 0.5f;
                            break;
                        case Duck.Type.GOLD:
                            modifier = 0.1f;
                            break;
                        case Duck.Type.PLAYER:
                            modifier = 5f;
                            break;
                        case Duck.Type.BLACK:
                            modifier = 20;
                            break;
                    }

                    weights[i] = (Vector3.Distance(futurePositions[i], futureMagnetPos)) * modifier;
                }

                //Set target position to the future position of the best duck
                int chosenDuck = 0;
                float minWeight = 99999;
                for (int i = 0; i < nearDuckColliders.Length; i++)
                {
                    if (weights[i] < minWeight)
                    {
                        chosenDuck = i;
                        minWeight = weights[i];
                    }
                }

                _targetPos = nearDucks[chosenDuck].transform.position;
            }

        }
        else
        {
            //Set target position to the basket
            _targetPos = basket.transform.position;
        }


        //Up/down movement

        bool goDown = Vector3.Distance(magnet.transform.position, _targetPos) < 3;


        if (_height < 1 && magnet.tag == "Magnet" && goDown)
        {
            _height = Mathf.Min(_height + 1 * Time.deltaTime, 1);
        }
        else if (_height > 0 && magnet.tag != "Magnet" && goDown)
        {
            _height = Mathf.Max(_height - 1 * Time.deltaTime, 0);
        }

        magnetHitbox.enabled = (_height >= 1);

        //Move rod
        if (!gameManager.gameOver)
        {
            Vector3 newPos = new Vector3(_targetPos.x, _initialHeight - _height, _targetPos.z) + positionOffset;
            transform.position = Vector3.MoveTowards(transform.position, newPos, 8 * Time.deltaTime);
        }
    }
}
