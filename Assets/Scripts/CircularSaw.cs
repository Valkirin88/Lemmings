using UnityEngine;
using EzySlice;

public class CircularSaw : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10;
    [SerializeField]
    private Material _crossSectionMaterial;

    private Vector3 _sawRotation = new Vector3(0,0,1);
    private GameObject[] _slicedObjects;
    private GameObject _woodObject;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.TryGetComponent<WoodLogCollider>(out WoodLogCollider wood))
        {
            Debug.Log("Wood");
            _woodObject = wood.WoodLog;
            _slicedObjects = Slice(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), new Vector3(0, 0, 1), new TextureRegion());
            var leftPart = _slicedObjects[0];
            var rightPart = _slicedObjects[1];
            Destroy(_woodObject);
            CapsuleCollider capsuleCollider = leftPart.AddComponent<CapsuleCollider>();
            CapsuleCollider capsuleCollider2 = rightPart.AddComponent<CapsuleCollider>();
            Rigidbody rigidbody = leftPart.AddComponent<Rigidbody>();
            Rigidbody rigidbody1 = rightPart.AddComponent<Rigidbody>();
            rigidbody1.isKinematic = false;
            rigidbody1.isKinematic = false;
        }
    }

    public GameObject[] Slice(Vector3 planeWorldPosition, Vector3 planeWorldDirection, TextureRegion region)
    {
        Debug.Log(_woodObject);
        return _woodObject.SliceInstantiate(planeWorldPosition, planeWorldDirection, region, _crossSectionMaterial);
    }

    private void Update()
    {
        RotateSaw();
    }

    private void RotateSaw()
    {
        transform.Rotate(_speed * Time.deltaTime * _sawRotation);
    }
}
