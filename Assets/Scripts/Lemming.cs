using UnityEngine;

public class Lemming : MonoBehaviour
{
    public bool IsInFire;
    public bool _isDead;
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

    private float _moveSpeed = 150f;
    private float _dropSpeed = 2000f;
    private float _maxSpeed = 2;
    private float _rotationSpeed = 1000;

    public bool _isHangedUp;
    public bool _isGrounded;

    private RandomDirection _randomDirection;
    private Vector3 _direction;

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
            if(!_isDead) 
                ChangeDirection();
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
        if(collision.gameObject.GetComponent<CircularSaw>() && !_isDead)
        {
            //SawDeath();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Bonfire>() || IsInFire)
        {
            SetFire();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.GetComponent<Lemming>())
        {
            Debug.Log("collision");
            ChangeDirection();
        }
        if (collision.gameObject.GetComponent<Ground>())
        {
            _isGrounded = true;
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
        _isDead = true;
        _collider.enabled = false;
        _meshObject.SetActive(false);
    }

    private void Update()
    {
        if (!_isDead && !_isHangedUp)
        {
           if(Rigidbody.velocity.magnitude < 0.1)
           {
               // ChangeDirection();
           }
        }
        LifeTime += Time.deltaTime;
        Rotate();
    }

    public void HangUp()
    {
        _isGrounded = false;
        _isHangedUp = true;
    }

    public void Drop()
    {
        if(_isHangedUp && transform.position == _hangPosition)
        _isHangedUp = false;
    }

    public void SetHangPosition(Vector3 position)
    {
        _hangPosition = position;
        if(_isHangedUp) 
        {
            transform.position = _hangPosition;
        }
    }

    private void FixedUpdate()
    {
        if (_isGrounded)
        {
            Move();
            LimitVelocity();
        }
    }

    private void Move()
    {
        if (!_isDead && !_isHangedUp)
        {
            var target = transform.TransformDirection(Vector3.forward) * _moveSpeed;
            Rigidbody.AddForce(target, ForceMode.Force);
            if(IsInFire) 
            {
                 target = transform.TransformDirection(Vector3.forward) * _moveSpeed * 5;
                Rigidbody.AddForce(target, ForceMode.Force);
            }
            if (!_isGrounded)
            {
                var target_down = transform.TransformDirection(Vector3.down) * _dropSpeed;
                Rigidbody.AddForce(target_down, ForceMode.Force);
            }
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
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(_direction), _rotationSpeed * Time.deltaTime);
    }
}
