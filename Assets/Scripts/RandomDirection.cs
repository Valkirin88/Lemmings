using UnityEngine;

public class RandomDirection 
{
    private int[] _directionsAngles;
    public RandomDirection()
    {
        _directionsAngles = new int[] { 0, 22, 45, 67, 90, 112, 135, 157, 180, -22, -45, -67, -90, -112, -135, -157};
    }

    public Vector3 GetDirection()
    {
        Vector3 direction = new Vector3(0, _directionsAngles[Random.Range(0, _directionsAngles.Length)], 0);
        return direction;
    }
}
