using System.Collections.Generic;
using UnityEngine;

public class LemmingsController
{ 
    private List<Lemming> _lemmings;
    private InputHandler _inputHandler;

    public LemmingsController(InputHandler inputHandler, List<Lemming> lemmings)
    {
        _inputHandler = inputHandler;
        _lemmings = lemmings;
        _inputHandler.OnLemmingClicked += ChangeDirection;
    }

    private void ChangeDirection(Lemming lemming)
    {
        lemming.ChangeDirection();
    }
}
