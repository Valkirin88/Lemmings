using UnityEngine;

public class Lemming : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;

    private float _moveSpeed = 500f;
    private float _maxSpeed = 2;
    private float _betweenCollisionTime = 1;


    private RandomDirection _randomDirection;


    private Vector3 _lastPosition;
    private float _afterLastPositionTime;
    private float _beforeRotationDistance = 0.5f;


    private void Start()
    {
        _randomDirection = new RandomDirection();
        ChangeDirection();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Lemming>()) 
        {
            ChangeDirection();
        }
        if (collision.gameObject.GetComponent<Wood>())
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        _afterLastPositionTime += Time.deltaTime; 
        if(_afterLastPositionTime >= _betweenCollisionTime)
        {
            _afterLastPositionTime = 0;
            if (Vector3.Distance(transform.position, _lastPosition) < _beforeRotationDistance)
            {
                ChangeDirection();
            }
            _lastPosition = transform.position;
        }
    }

    private void FixedUpdate()
    {
        Move();
        LimitVelocity();
    }

    private void Move()
    {
        var target = transform.TransformDirection(Vector3.forward) * _moveSpeed;
        _rigidbody.AddForce(target, ForceMode.Force);
    }

    private void LimitVelocity()
    {
        if (_rigidbody.velocity.magnitude > _maxSpeed)
        {

            Vector3 reduction = (_rigidbody.velocity.normalized * (_rigidbody.velocity.magnitude - _maxSpeed));
            _rigidbody.AddForce(-reduction, ForceMode.VelocityChange);
        }
    }
    private void ChangeDirection() 
    {
        transform.eulerAngles =_randomDirection.GetDirection();
    }
}
