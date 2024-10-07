using UnityEngine;

public class LogsPile : MonoBehaviour
{
    [SerializeField]
    private Wood[] _woods;

    private bool _isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Lemming>() && _isActive)
        {
            _isActive = false;
            foreach 
                (var wood in _woods) 
            {
                if(!wood.IsPileDestroyed)
                wood.StartMoving();
            }
        }
    }

}
