using UnityEngine;

public class LogsPile : MonoBehaviour
{
    [SerializeField]
    private Log[] _logs;
    [SerializeField]
    private int _leftImpulse = -400;
    [SerializeField]
    private int _rightImpulse = 500;

    private bool _isActive = true;
    private int _numberLogsMovengLeft = 3;
    
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Lemming>() && _isActive)
        {
            Debug.Log("Pile destroy");
            _isActive = false;
            foreach 
                (var log in _logs) 
            {
                if (!log.IsPileDestroyed)
                {
                    log.IsPileDestroyed = true;
                    MoveLog(log);
                }
            }
        }
    }

    private void MoveLog(Log log)
    {
        if (_numberLogsMovengLeft > 0)
        {
            log.Rigidbody.isKinematic = false;
            log.Rigidbody.AddForce(new Vector3(_leftImpulse,0, 0), ForceMode.Impulse);
            _numberLogsMovengLeft--;
        }
        else
        {
            log.Rigidbody.isKinematic = false;
            log.Rigidbody.AddForce(new Vector3(_rightImpulse, 0, 0), ForceMode.Impulse);
            _numberLogsMovengLeft--;
        }

    }

}
