using UnityEngine;

public class RandomDirection 
{
    private int[] _directionsAngles;
    private Vector3 _previousDirection;
    private Vector3 _direction;
    public RandomDirection()
    {
        _directionsAngles = new int[] { 0,45, 90, 135, 180, 225, 270, 315};
    }

    public Vector3 GetDirection()
    {
        while (_previousDirection == _direction)
        {
            _direction = new Vector3(0, _directionsAngles[Random.Range(0, _directionsAngles.Length)], 0);
        }
        _previousDirection = _direction;
        return _direction;
    }
}
