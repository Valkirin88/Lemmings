using UnityEngine;

public class Wood : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    private bool _IsWorked;

    private void FixedUpdate()
    {
        if (!_rigidbody.isKinematic && !_IsWorked)
        {
            _rigidbody.AddForce(new Vector3(0,0, Random.Range(-30, 30)) * 20, ForceMode.Impulse);
            _IsWorked = true;
        }
    }
}
