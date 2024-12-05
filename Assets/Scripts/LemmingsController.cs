using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        // _inputHandler.OnLemmingUnclicked += Drop;
        //_inputHandler.OnMousebuttonDownStayed += SetHangPosition;
        _inputHandler.OnLemmingUnclicked += SetDirection;
    }

    private void SetDirection(Vector3 vector)
    {
        _lemming.SetDirection(vector);
    }

    private void ChangeDirection(Lemming lemming, Vector3 pos)
    {
        _lemming = lemming;
        _lemming.ChangeDirection();
        
        _lemming.HangUp();
        SetHangPosition(pos);
    }
    private void Drop(Lemming lemming)
    {
        _lemming= lemming;
        _lemming.Drop();
    }
    private void SetHangPosition(Vector3 position)
    {
        _lemming.SetHangPosition(position);
        Drop(_lemming);
    }

    public void Dispose() 
    {
        _inputHandler.OnLemmingClicked -= ChangeDirection;
        _inputHandler.OnMousebuttonDownStayed -= SetHangPosition;
    }

}
