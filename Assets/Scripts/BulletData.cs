using System;
using UnityEngine;

/// <summary>
/// Used to store the initial conditions of a Bullet
/// </summary>
[Serializable]
public class BulletData
{
    //Angle that the bullet will be fired from the the turret between 0 and 360 degrees
    [Range(0f, 360f)]
    public float Angle;

    public float Speed;

    public Bullet Prefab;

    public new string ToString()
    {
        return string.Format("Angle: {0} - Speed: {1}", Angle, Speed);
    }
}
