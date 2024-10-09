using UnityEngine;

public class Wood : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public float LifeTime;

    public bool IsDangerous;
    public bool IsPileDestroyed;
    
    private bool IsRolling;

    
    
    private void FixedUpdate()
    {
        if (IsPileDestroyed && !IsRolling)
        {
            Rigidbody.isKinematic = false;
            Rigidbody.AddForce(new Vector3(200,0, 0), ForceMode.Impulse);
            IsRolling = true;
        }
        
        if (Rigidbody.velocity.magnitude < 2 && IsPileDestroyed) 
        {
            IsDangerous = false;
        }
        else if (Rigidbody.velocity.magnitude > 2)
        {
            IsDangerous = true;
        }
    }

    private void Update()
    {
        LifeTime += Time.deltaTime;
    }

    public void StartMoving()
    {
        IsPileDestroyed = true;
    }
}
