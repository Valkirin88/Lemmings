using System;
using System.Collections.Generic;
using UnityEngine;

public class LemmingsController : IDisposable
{ 
    private List<Lemming> _lemmings;
    private InputHandler _inputHandler;

    private Lemming _lemming;

    public LemmingsController(InputHandler inputHandler, List<Lemming> lemmings)
    {
        _inputHandler = inputHandler;
        _lemmings = lemmings;
        _inputHandler.OnLemmingClicked += ChangeDirection;
        _inputHandler.OnLemmingUnclicked += Drop;
        _inputHandler.OnMousebuttonDownStayed += SetHangPosition;
    }

    private void ChangeDirection(Lemming lemming)
    {
     //   lemming.ChangeDirection();
     _lemming = lemming;
        _lemming.HangUp();
    }
    private void Drop(Lemming lemming)
    {
        _lemming= lemming;
        _lemming.Drop();
    }
    private void SetHangPosition(Vector3 position)
    {
        _lemming.SetHangPosition(position);
    }

    public void Dispose() 
    {
        _inputHandler.OnLemmingClicked += ChangeDirection;
    }

}
