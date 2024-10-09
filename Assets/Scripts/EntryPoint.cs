using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField]
    private List<Lemming> _lemmings;
    [SerializeField]
    private GameObject _groundObject;
    [SerializeField]
    private LayerMask _layerMask;

    private InputHandler _inputHandler;
    private LemmingsController _lemmingsController;

    private void Awake()
    {
        _inputHandler = new InputHandler(_groundObject,_layerMask);
        _lemmingsController = new LemmingsController(_inputHandler, _lemmings);
    }

    private void Update()
    {
        _inputHandler.Update();
    }
}
