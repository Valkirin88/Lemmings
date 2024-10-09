using Unity.VisualScripting;
using UnityEngine;

public class Lemming : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private ParticleSystem _bloodSpray;
    [SerializeField]
    private ParticleSystem _bloodSpill;
    [SerializeField]
    private MeshRenderer _meshRenderer;
    [SerializeField]
    private Collider _collider;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _audioClip;

    private float _moveSpeed = 50f;
    private float _maxSpeed = 2;

    private bool _isDead;
    private bool _isHangedUp;

    private RandomDirection _randomDirection;
    private Vector3 _direction;


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
        if (collision.gameObject.TryGetComponent<Wood>(out Wood wood))
        {
            if (wood.IsDangerous)
            {
                Dead();
                ShowDeath();
            }

        }
        if(collision.gameObject.GetComponent<CircularSaw>() && !_isDead)
        {
            SawDeath();
        }
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
        _rigidbody.isKinematic = true;
        _isDead = true;
        _collider.enabled = false;
        _meshRenderer.enabled = false;
    }

    private void Update()
    {
        if (!_isDead && !_isHangedUp)
        {
            transform.eulerAngles = _direction;
           if(_rigidbody.velocity.magnitude < 0.01)
           {
                ChangeDirection();
           }
        }
    }

    public void HangUp()
    {
        _isHangedUp = true;
    }

    public void Drop()
    {
        _isHangedUp = false;
    }

    public void SetHangPosition(Vector3 position)
    {
        if(_isHangedUp) 
        {
            transform.position = position;
        }
    }

    private void FixedUpdate()
    {
        Move();
        LimitVelocity();
    }

    private void Move()
    {
        if (!_isDead && !_isHangedUp)
        {
            var target = transform.TransformDirection(Vector3.forward) * _moveSpeed;
            _rigidbody.AddForce(target, ForceMode.Force);
        }
    }

    private void LimitVelocity()
    {
        if (_rigidbody.velocity.magnitude > _maxSpeed)
        {

            Vector3 reduction = (_rigidbody.velocity.normalized * (_rigidbody.velocity.magnitude - _maxSpeed));
            _rigidbody.AddForce(-reduction, ForceMode.VelocityChange);
        }
    }
    public void ChangeDirection() 
    {
        _direction = _randomDirection.GetDirection();
        transform.eulerAngles = _direction;
    }
}
