using UnityEngine;

public class SlicedLogsHandler
{
    private GameObject _gameObject1;
    private GameObject _gameObject2;
    private Rigidbody _rigidbody1;
    private Rigidbody _rigidbody2;
    private Log _log1;
    private Log _log2;

    public void HandleSlicedLogs(GameObject gameObject1, GameObject gameObject2)
    {
        _gameObject1 = gameObject1;
        _gameObject2 = gameObject2;
        AddCapsuleColliders();
        AddRigidbodies();
        AddLogComponent();
        AdjustLogs();
        AdjustRigidboies();
    }

    private void AddCapsuleColliders()
    {
        CapsuleCollider capsuleCollider = _gameObject1.AddComponent<CapsuleCollider>();
        CapsuleCollider capsuleCollider2 = _gameObject2.AddComponent<CapsuleCollider>();
    }

    private void AddRigidbodies()
    {
        _rigidbody1 = _gameObject1.AddComponent<Rigidbody>();
        _rigidbody2 = _gameObject2.AddComponent<Rigidbody>();
    }

    private void AddLogComponent()
    {
        _log1 = _gameObject1.AddComponent<Log>();
        _log2 = _gameObject2.AddComponent<Log>();
    }

    private void AdjustLogs()
    {
        _log1.Rigidbody = _rigidbody1;
        _log1.IsPileDestroyed = true;
        _log2.IsPileDestroyed = true;
        _log2.Rigidbody = _rigidbody2;
    }

    private void AdjustRigidboies()
    {
        _rigidbody1.isKinematic = false;
        _rigidbody2.isKinematic = false;
        _rigidbody1.mass = 10;
        _rigidbody2.mass = 10;
        _rigidbody1.AddForce(new Vector3(300, 0, 20), ForceMode.Impulse);
        _rigidbody2.AddForce(new Vector3(300, 0, -20), ForceMode.Impulse);
    }
}
