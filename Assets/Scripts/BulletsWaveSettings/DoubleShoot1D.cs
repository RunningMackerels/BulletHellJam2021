using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleShoot1DWave", menuName = "Scriptable Objects/DoubleShoot1DWave")]
public class DoubleShoot1D : BulletsWaveSettings
{
    [SerializeField]
    private float _FirePeriod = 1f;

    [SerializeField]
    private float _BulletsBaseSpeed = 1f;

    [SerializeField]
    private Bullet _BulletPrefab = null;

    [SerializeField]
    private Vector3 offset = new Vector3(0f, 0.2f, 0f);

    private float _lastPeriodStartedTimeStamp = 0f;

    public override List<BulletData> GetBullets()
    {
        List<BulletData> bullets = new List<BulletData>();

        if (TimeLord.Instance.Now - _lastPeriodStartedTimeStamp > _FirePeriod)
        {
            _lastPeriodStartedTimeStamp = TimeLord.Instance.Now;
        }
        else
        {
            return bullets;
        }

        bullets.Add(new BulletData()
        {
            Speed = _BulletsBaseSpeed,
            Angle = 0f,
            Prefab = _BulletPrefab,
            LocalOffset = offset * 0.5f
        });

        bullets.Add(new BulletData()
        {
            Speed = _BulletsBaseSpeed,
            Angle = 0f,
            Prefab = _BulletPrefab,
            LocalOffset = offset * -0.5f
        });

        return bullets;
    }
}
