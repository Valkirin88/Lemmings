using System;
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
    private float _betweenCollisionTime = 1;

    private bool _isDead;


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
        if (collision.gameObject.TryGetComponent<Wood>(out Wood wood))
        {
            if(wood.IsDangerous)
            Dead();
        }
        if(collision.gameObject.GetComponent<CircularSaw>() && !_isDead)
        {
            SawDeath();
        }
      
    }

    private void SawDeath()
    {
        Dead();

        _audioSource.PlayOneShot(_audioClip);
    }

    private void Dead()
    {
        _rigidbody.isKinematic = true;
        _isDead = true;
        _collider.enabled = false;
        _meshRenderer.enabled = false;
        _bloodSpill.Play();
        _bloodSpray.Play();
    }

    private void Update()
    {
        if (!_isDead)
        {
            _afterLastPositionTime += Time.deltaTime;
            if (_afterLastPositionTime >= _betweenCollisionTime)
            {
                _afterLastPositionTime = 0;
                if (Vector3.Distance(transform.position, _lastPosition) < _beforeRotationDistance)
                {
                    ChangeDirection();
                }
                _lastPosition = transform.position;
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
        LimitVelocity();
    }

    private void Move()
    {
        if (!_isDead)
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
    private void ChangeDirection() 
    {
        transform.eulerAngles =_randomDirection.GetDirection();
    }
}
