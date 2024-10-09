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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<Wood>(out Wood wood))
        {
            Debug.Log("Wood collided");
            if (wood.LifeTime >= 2)
            {
                _woodObject = wood.gameObject;
                _slicedObjects = Slice(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), new Vector3(0, 0, 1), new TextureRegion());
                var leftPart = _slicedObjects[0];
                var rightPart = _slicedObjects[1];
                Destroy(_woodObject);
                CapsuleCollider capsuleCollider = leftPart.AddComponent<CapsuleCollider>();
                CapsuleCollider capsuleCollider2 = rightPart.AddComponent<CapsuleCollider>();
                Rigidbody rigidbody1 = leftPart.AddComponent<Rigidbody>();
                Rigidbody rigidbody2 = rightPart.AddComponent<Rigidbody>();
                Wood wood1 = leftPart.AddComponent<Wood>();
                Wood wood2 = rightPart.AddComponent<Wood>();
                wood1.Rigidbody = rigidbody1;
                wood2.Rigidbody = rigidbody2;
                rigidbody1.isKinematic = false;
                rigidbody2.isKinematic = false;
                rigidbody1.AddForce(new Vector3(50, 0, 0), ForceMode.Impulse);
                rigidbody2.AddForce(new Vector3(50, 0, 0), ForceMode.Impulse);
            }
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
