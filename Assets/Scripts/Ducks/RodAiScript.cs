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
    private int[] weights;
    private Vector3 futureMagnetPos;
    public Rigidbody magnetRB;
    [SerializeField] private DynamicDifficultyManager _ddm;

    void Start()
    {
        positionOffset = transform.position - rodTip.transform.position;
        positionOffset.y = 0;
        _height = 0;
        _initialHeight = transform.position.y;
    }

    private void OnDrawGizmos() 
    {
        //Gizmos.DrawSphere(magnet.transform.position, 5);
    }

    void Update()
    {
        //Select Target Position
        if (magnet.tag == "Magnet")
        {
            //Get all eligible ducks in a small area
            nearDuckColliders = Physics.OverlapSphere(magnet.transform.position, _ddm.GetValue(0), ducksLayer);

            if (nearDuckColliders.Length == 0)
            {
                _targetPos = Vector3.zero;
            }
            else
            {
                nearDucks = new Duck[nearDuckColliders.Length];
                futurePositions = new Vector3[nearDuckColliders.Length];
                weights = new int[nearDuckColliders.Length];

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

                //Calculate weight of all ducks (based on duck type)
                for (int i = 0; i < nearDuckColliders.Length; i++)
                {Debug.DrawLine(magnet.transform.position, nearDucks[i].transform.position, Color.green);
                    switch (nearDucks[i].type)
                    {
                        case Duck.Type.NORMAL:
                            weights[i] = 3;
                            break;
                        case Duck.Type.AI:
                            weights[i] = 4;
                            break;
                        case Duck.Type.GOLD:
                            weights[i] = 1;
                            break;
                        case Duck.Type.PLAYER:
                            weights[i] = 2;
                            break;
                        case Duck.Type.BLACK:
                            weights[i] = 5;
                            break;
                    }
                }

                //Set target position to the future position of the best duck
                int chosenDuck = 0;
                int minWeight = 99999;
                float minDist = 99999;
                float randomRange = 0;
                for (int i = 0; i < nearDuckColliders.Length; i++)
                {
                    randomRange = Random.Range(0, _ddm.GetValue(1));
                    if (weights[i] < (minWeight + randomRange))
                    {
                        chosenDuck = i;
                        minWeight = weights[i];
                        minDist = (Vector3.Distance(futurePositions[i], futureMagnetPos));
                    }
                    else if (weights[1] == (minWeight + randomRange))
                    {
                        if ((Vector3.Distance(futurePositions[i], futureMagnetPos)) < minDist)
                        {
                            chosenDuck = i;
                            minWeight = weights[i];
                            minDist = (Vector3.Distance(futurePositions[i], futureMagnetPos)); 
                        }
                    }
                }

                //if (nearDucks[chosenDuck].type != Duck.Type.BLACK) //Not yet
                    _targetPos = nearDucks[chosenDuck].transform.position;Debug.DrawLine(magnet.transform.position, _targetPos, Color.magenta);
            }

        }
        else
        {
            //Set target position to the basket
            _targetPos = basket.transform.position;
        }


        //Up/down movement
        Vector3 targetLevel = new Vector3(_targetPos.x, magnet.transform.position.y, _targetPos.z);
        bool goDown = Vector3.Distance(magnet.transform.position, targetLevel) < 1;
        print(goDown);

        if (_height < 1 && magnet.tag == "Magnet" && goDown)
        {
            _height = Mathf.Min(_height + 1.5f * Time.deltaTime, 1);
        }
        else if (_height > 0 && magnet.tag != "Magnet")
        {
            _height = Mathf.Max(_height - 1.5f * Time.deltaTime, 0);
        }

        magnetHitbox.enabled = (_height >= 1);

        //Move rod
        if (!gameManager.gameOver)
        {
            Vector3 newPos = new Vector3(_targetPos.x, _initialHeight - _height, _targetPos.z) + positionOffset;
            transform.position = Vector3.MoveTowards(transform.position, newPos, 10 * Time.deltaTime);
        }
    }
}
