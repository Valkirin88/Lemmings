using UnityEngine;

public class Lemming : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 10f;
    [SerializeField]
    private float _moveSpeed = 3000f;
    [SerializeField]
    private Rigidbody _rigidbody;


    private float _betweenCollisionTime = 1;

    private Vector3 _direction;
    private RandomDirection _randomDirection;



    private Vector3 _lastPosition;
    private float _afterLastPositionTime;
    private float _tillNewPositionTime = 1;
    private float _beforeRotationDistance = 0.5f;
    private bool _isWait;

    private void Start()
    {
        _randomDirection = new RandomDirection();
        ChangeDirection();
    }

  
   

    private void Update()
    {

        
        //Rotate();


        _afterLastPositionTime += Time.deltaTime; 
        if(_afterLastPositionTime <= _betweenCollisionTime)
        {

        }
        else
        {
            _afterLastPositionTime = 0;
            if (Vector3.Distance(transform.position, _lastPosition) < _beforeRotationDistance)
            {
                ChangeDirection();
                Debug.Log(Vector3.Distance(transform.position, _lastPosition));
            }
            _lastPosition = transform.position;
            Debug.Log(Vector3.Distance(transform.position, _lastPosition));
        }
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {

        _rigidbody.AddForce(Vector3.forward * Time.deltaTime * _moveSpeed, ForceMode.Force);

    }

  

    private void ChangeDirection() 
    {
        transform.eulerAngles =_randomDirection.GetDirection();

    }


}
