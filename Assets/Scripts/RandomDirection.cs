using UnityEngine;

public class RandomDirection 
{
    private int[] _directionsAngles;
    public RandomDirection()
    {
        _directionsAngles = new int[] { 0, 45, 90, 135, 180, 225, 270, 315, 360 };
    }

    public Vector3 GetDirection()
    {
        Vector3 direction = new Vector3(0, _directionsAngles[Random.Range(0, _directionsAngles.Length)], 0);
        return direction;
    }
}
