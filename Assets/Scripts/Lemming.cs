using UnityEngine;

public class Lemming : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 10f;
    [SerializeField]
    private float _moveSpeed = 3f;
    [SerializeField]
    private Rigidbody _rigidbody;

    private bool _isCollidable = true;
    private float _betweenCollisionTime = 1;
    private float _afterCollisionTime;
    private Vector3 _direction;
    private RandomDirection _randomDirection;
    private Quaternion _targetQuaternion;
    private Quaternion _transformQuaternion;

    private void Start()
    {
        _randomDirection = new RandomDirection();
        ChangeDirection();
    }

    private void OnCollisionStay(Collision collision)
    {
        ChangeDirection();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollidable)
        {
            _isCollidable = false;
            _afterCollisionTime = 0;
            ChangeDirection();
        }
    }

    private void Update()
    {
        CollisionTimer();
        Move();
        //Rotate();
    }

    private void Move()
    {
        _rigidbody.AddForce( _direction * Time.deltaTime * _moveSpeed);
        //transform.Translate(_moveSpeed * Time.deltaTime * Vector3.forward, Space.Self);
    }

    //private void Rotate()
    //{
    //    _transformQuaternion = transform.rotation;
    //    transform.rotation = Quaternion.Slerp(_transformQuaternion, _targetQuaternion, _rotationSpeed * Time.deltaTime);
    //}

    private void ChangeDirection() 
    {
        transform.eulerAngles =_randomDirection.GetDirection();
        //_targetQuaternion = Quaternion.Euler(_randomDirection.GetDirection());
    }

    private void CollisionTimer()
    {
        _afterCollisionTime += Time.deltaTime;
        if (_afterCollisionTime >= _betweenCollisionTime)
            _isCollidable = true;
    }
}
