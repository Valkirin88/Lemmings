using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField]
    private List<Lemming> _lemmings;
    [SerializeField]
    private GameObject _groundObject;
    [SerializeField]
    private LayerMask _hangLayerMask;
    [SerializeField]
    private LayerMask _lemmingLayerMask;

    private InputHandler _inputHandler;
    private LemmingsController _lemmingsController;

    private void Awake()
    {
        _inputHandler = new InputHandler(_hangLayerMask, _lemmingLayerMask);
        _lemmingsController = new LemmingsController(_inputHandler, _lemmings);
    }

    private void Update()
    {
        _inputHandler.Update();
    }
}
