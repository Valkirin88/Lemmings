using UnityEngine;

public class Lemming : MonoBehaviour
{
    public bool IsInFire;
    public bool IsDead;
    public float LifeTime;

    public Rigidbody Rigidbody;
    [SerializeField]
    private ParticleSystem _bloodSpray;
    [SerializeField]
    private ParticleSystem _bloodSpill;
    [SerializeField]
    private GameObject _meshObject;
    [SerializeField]
    private Collider _collider;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _audioClip;
    [SerializeField]
    private GameObject _fireObject;

    private Vector3 _hangPosition;

    private float _moveSpeed = 200f;
    private float _dropSpeed = 2000f;
    private float _maxSpeed = 2;
    private float _rotationSpeed = 500;
    private float _afterLastPositionTime;
    private float _betweenCollisionTime = 1f;

    private Vector3 _lastPosition;
    private float _beforeRotationDistance = 1f;

    public bool IsHangedUp;
    public bool IsGrounded;
    public bool IsInTargetPosition;

    private RandomDirection _randomDirection;
    private Vector3 _direction;
    private bool _isDirectianSetManually;

    private void Start()
    {
        _randomDirection = new RandomDirection();
        ChangeDirection();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<Lemming>(out Lemming lemming)) 
        {
            if(IsInFire)
            {
                lemming.SetFire();
            }
            if(lemming.IsInFire)
            {
                IsInFire = true;
            }
            //if(!IsDead) 
            //    ChangeDirection();
        }
        if (collision.gameObject.TryGetComponent<Log>(out Log wood))
        {
            
            if (wood.IsDangerous)
            {
                Debug.Log("wood collided");
                Dead();
                ShowDeath();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Bonfire>() || IsInFire)
        {
            SetFire();
        }
        if(other.gameObject.GetComponent<Target>())
        {
            IsInTargetPosition = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //if(collision.gameObject.GetComponent<Lemming>())
        //{
        //    Debug.Log("collision");
        //    ChangeDirection();
        //}
        if (collision.gameObject.GetComponent<Ground>())
        {
            IsGrounded = true;
        }
    }

    public void SetFire()
    {
        IsInFire = true;
        _fireObject.SetActive(true);
        Invoke("Dead", 3);
    }

    private void SawDeath()
    {
        ShowDeath();
        Dead();

        _audioSource.PlayOneShot(_audioClip);
    }

    private void ShowDeath()
    {
        _bloodSpill.Play();
        _bloodSpray.Play();
    }

    public void Dead()
    {
        _fireObject.SetActive(false);
        IsInFire = false;
        Rigidbody.isKinematic = true;
        IsDead = true;
        _collider.enabled = false;
        _meshObject.SetActive(false);
    }

    private void Update()
    {
        if (!IsDead && !IsHangedUp && !IsInTargetPosition)
        {
            Rotate();
            RotateIflongTimeNoMove();
        }
        LifeTime += Time.deltaTime;

    }

    public void SetDirection(Vector3 position)
    {
        _isDirectianSetManually = true;
        Vector3 direction = new Vector3(position.x, transform.position.y, position.z);
        transform.LookAt(direction);
    }

    private void RotateIflongTimeNoMove()
    {
            _afterLastPositionTime += Time.deltaTime;
            if (_afterLastPositionTime > _betweenCollisionTime)
            {
                _afterLastPositionTime = 0;
                if (Vector3.Distance(transform.position, _lastPosition) < _beforeRotationDistance)
                {
                    ChangeDirection();
                }
                _lastPosition = transform.position;
                Debug.Log(Vector3.Distance(transform.position, _lastPosition));
            }
        
    }

    public void HangUp()
    {
        IsGrounded = false;
        IsHangedUp = true;
    }

    public void Drop()
    {
        if(IsHangedUp && transform.position == _hangPosition)
        IsHangedUp = false;
    }

    public void SetHangPosition(Vector3 position)
    {
        _hangPosition = position;
        if(IsHangedUp) 
        {
            transform.position = _hangPosition;
        }
    }

    private void FixedUpdate()
    {
        if (IsGrounded && !IsInTargetPosition)
        {
            Move();
            LimitVelocity();
        }
    }

    private void Move()
    {
            var target = transform.TransformDirection(Vector3.forward) * _moveSpeed;
            Rigidbody.AddForce(target, ForceMode.Force);
            if(IsInFire) 
            {
                 target = transform.TransformDirection(Vector3.forward) * _moveSpeed * 5;
                Rigidbody.AddForce(target, ForceMode.Force);
            }
            if (!IsGrounded)
            {
                var target_down = transform.TransformDirection(Vector3.down) * _dropSpeed;
                Rigidbody.AddForce(target_down, ForceMode.Force);
            }
    }

    private void LimitVelocity()
    {
        if (Rigidbody.velocity.magnitude > _maxSpeed)
        {
            Vector3 reduction = (Rigidbody.velocity.normalized * (Rigidbody.velocity.magnitude - _maxSpeed));
            Rigidbody.AddForce(-reduction, ForceMode.VelocityChange);
        }
    }
    public void ChangeDirection() 
    {

        _direction = _randomDirection.GetDirection();
        _isDirectianSetManually = false;
    }

    private void Rotate()
    {
        if (!_isDirectianSetManually)
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(_direction), _rotationSpeed * Time.deltaTime);
    }
}
