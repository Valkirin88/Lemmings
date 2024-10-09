using UnityEngine;

public class Wood : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;

    public bool IsDangerous;
    public bool IsPileDestroyed;
    
    private bool IsRolling;
    
    private void FixedUpdate()
    {
        if (IsPileDestroyed && !IsRolling)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(new Vector3(200,0, 0), ForceMode.Impulse);
            IsRolling = true;
        }
        
        if (_rigidbody.velocity.magnitude < 2 && IsPileDestroyed) 
        {
            IsDangerous = false;
        }
        else if (_rigidbody.velocity.magnitude > 2)
        {
            IsDangerous = true;
        }
    }

    public void StartMoving()
    {
        IsPileDestroyed = true;
    }
}
