using UnityEngine;

public class Wood : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    public bool IsPileDestroyed;
    public bool IsRolling;

    private void FixedUpdate()
    {
        if (IsPileDestroyed && !IsRolling)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(new Vector3(150,0, 0), ForceMode.Impulse);
            IsRolling = true;
        }
        //Debug.Log(_rigidbody.velocity.magnitude);
        if (_rigidbody.velocity.magnitude < 0.01 && IsPileDestroyed) 
        {
            IsRolling=false;
        }
    }

    public void StartMoving()
    {
        IsPileDestroyed = true;
    }
}
