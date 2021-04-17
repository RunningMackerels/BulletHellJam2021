using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LinearBulletWave", menuName = "Scriptable Objects/Linear Bullet Wave")]
public class LinearBulletWave : BulletsWaveSettings
{
    [SerializeField]
    private float _FirePeriod = 1f;

    [SerializeField]
    private float _BulletsSpeed = 1f;

    [SerializeField]
    private int _NumberOfBullets = 20;

    [SerializeField]
    private Bullet _BulletPrefab = null;

    private float _lastTimeStampFired = 0f;

    private void OnEnable()
    {
        _lastTimeStampFired = 0f;
    }

    public override List<BulletData> GetBullets()
    {
        if (TimeLord.Instance.Now - _lastTimeStampFired < _FirePeriod * TimeLord.Instance.SpeedMultiplier)
        {
            return new List<BulletData>();
        }

        List<BulletData> bullets = new List<BulletData>();

        for (int i = 0; i < _NumberOfBullets; i++)
        {
            BulletData newBullet = new BulletData()
            {
                Speed = _BulletsSpeed,
                Angle = i * 360f / _NumberOfBullets,
                Prefab = _BulletPrefab
            };

            bullets.Add(newBullet);
        }

        _lastTimeStampFired = TimeLord.Instance.Now;

        return bullets;
    }
}
