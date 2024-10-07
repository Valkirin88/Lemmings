using UnityEngine;

public class CircularSaw : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10;

    private Vector3 _sawRotation = new Vector3(0,0,1);

    private void Update()
    {
        RotateSaw();
    }

    private void RotateSaw()
    {
        transform.Rotate(_speed * Time.deltaTime * _sawRotation);
    }
}
