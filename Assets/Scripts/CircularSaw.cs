using UnityEngine;
using EzySlice;

public class CircularSaw : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10;
    [SerializeField]
    private Material _crossSectionWoodMaterial;
    [SerializeField]
    private Material _crossSectionLemmingMaterial;
    [SerializeField]
    private ParticleSystem _bloodParticles;


    private Material _crossSectionMaterial;

    private Vector3 _sawRotation = new Vector3(0,0,1);
    private GameObject[] _slicedObjects;
    private GameObject _slicedObject;
    private SlicedLogsHandler _slicedLogsHandler;

    private void Start()
    {
        _slicedLogsHandler = new SlicedLogsHandler();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.TryGetComponent<Log>(out Log log))
        {
            if (log.LifeTime >= 2)
            {
                _slicedObject = log.gameObject;
                _crossSectionMaterial = _crossSectionWoodMaterial;
                Slice();
            }
        }
        if(collision.gameObject.TryGetComponent<Lemming>(out Lemming lemming))
        {
            _bloodParticles.transform.SetParent(null);
            _bloodParticles.Play();
            lemming.gameObject.transform.position = new Vector3(lemming.gameObject.transform.position.x, lemming.gameObject.transform.position.y, transform.position.z);
            _slicedObject =  lemming.gameObject;
            _crossSectionMaterial = _crossSectionLemmingMaterial;
            Slice();
        }
    }

    private void Slice()
    {
        _slicedObjects = Slice(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), new Vector3(0, 0, 1), new TextureRegion());
        Destroy(_slicedObject);
        _slicedLogsHandler.HandleSlicedObject(_slicedObjects[0], _slicedObjects[1]);
    }

    public GameObject[] Slice(Vector3 planeWorldPosition, Vector3 planeWorldDirection, TextureRegion region)
    {
        return _slicedObject.SliceInstantiate(planeWorldPosition, planeWorldDirection, region, _crossSectionMaterial);
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
