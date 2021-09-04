using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerScript : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody; 
    private Vector3 _dir;
    [SerializeField] private float _velocity;
    [SerializeField] private GameObject _player;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Animator _animator;
    public Vector3 lastDir;
    private Queue<Vector3> positions;
    
    void Start()
    {
        positions = new Queue<Vector3>();
        lastDir = new Vector3(0,0,1);

        FadeController.instance.follower = this;

        if (FadeController.instance.storedPlayerPosition)
        {
            transform.position = FadeController.instance.lastFollowerPosition;
            lastDir = FadeController.instance.lastFollowerDirection;
        }
    }

    void FixedUpdate()
    {
        positions.Enqueue(_player.transform.position);
        if (positions.Count >= 10) positions.Dequeue();

        _dir = /*positions.Peek()*/ _player.transform.position - transform.position;
        Debug.DrawRay(transform.position, _dir, Color.red);
        _dir.Normalize();
        lastDir = _dir;

        if (Vector3.Distance(transform.position, _player.transform.position) > 8)
        {
            if (Vector3.Distance(transform.position, positions.Peek()) < 0.01f)
            {
                positions.Dequeue();
            }
            _rigidbody.velocity = new Vector3(_dir.x * Time.deltaTime * _velocity, 0, _dir.z * Time.deltaTime * _velocity);
            _animator.SetBool("Moving", true);
        }
        else if (Vector3.Distance(transform.position, _player.transform.position) < 6)
        {
            _rigidbody.velocity = Vector3.zero;
            _animator.SetBool("Moving", false);
        }

        //Set Animator direction

        float angle = Vector3.SignedAngle(_mainCamera.transform.forward, Vector3.forward, Vector3.up);
        Vector3 rotatedLastDir = Quaternion.Euler(0,angle+90,0)*lastDir;
        
        float up = Mathf.Max(rotatedLastDir.x, 0);
        float down = -Mathf.Min(rotatedLastDir.x, 0);
        float left = Mathf.Max(rotatedLastDir.z, 0);
        float right = -Mathf.Min(rotatedLastDir.z, 0);

        if (up > 0.1f)
        {
            if (right > 0.1f)
            {
                _animator.SetLayerWeight(1,0);
                _animator.SetLayerWeight(2,1);
                _animator.SetLayerWeight(8,0);
            }
            else if (left > 0.1f)
            {
                _animator.SetLayerWeight(1,0);
                _animator.SetLayerWeight(2,0);
                _animator.SetLayerWeight(8,1);
            }
            else
            {
                _animator.SetLayerWeight(1,1);
                _animator.SetLayerWeight(2,0);
                _animator.SetLayerWeight(8,0);
            }
            _animator.SetLayerWeight(3,0);
            _animator.SetLayerWeight(4,0);
            _animator.SetLayerWeight(5,0);
            _animator.SetLayerWeight(6,0);
            _animator.SetLayerWeight(7,0);
        }
        else if (down > 0.1f)
        {
            if (right > 0.1f)
            {
                _animator.SetLayerWeight(5,0);
                _animator.SetLayerWeight(4,1);
                _animator.SetLayerWeight(6,0);
            }
            else if (left > 0.1f)
            {
                _animator.SetLayerWeight(5,0);
                _animator.SetLayerWeight(4,0);
                _animator.SetLayerWeight(6,1);
            }
            else
            {
                _animator.SetLayerWeight(5,1);
                _animator.SetLayerWeight(4,0);
                _animator.SetLayerWeight(6,0);
            }
            _animator.SetLayerWeight(1,0);
            _animator.SetLayerWeight(2,0);
            _animator.SetLayerWeight(3,0);
            _animator.SetLayerWeight(7,0);
            _animator.SetLayerWeight(8,0);
        }
        else if (right > 0.1f)
        {
            _animator.SetLayerWeight(1,0);
            _animator.SetLayerWeight(2,0);
            _animator.SetLayerWeight(3,1);
            _animator.SetLayerWeight(4,0);
            _animator.SetLayerWeight(5,0);
            _animator.SetLayerWeight(6,0);
            _animator.SetLayerWeight(7,0);
            _animator.SetLayerWeight(8,0);
        }
        else if (left > 0.1f)
        {
            _animator.SetLayerWeight(1,0);
            _animator.SetLayerWeight(2,0);
            _animator.SetLayerWeight(3,0);
            _animator.SetLayerWeight(4,0);
            _animator.SetLayerWeight(5,0);
            _animator.SetLayerWeight(6,0);
            _animator.SetLayerWeight(7,1);
            _animator.SetLayerWeight(8,0);
        }

    }
}
