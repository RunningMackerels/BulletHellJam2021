using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurveBulletWave", menuName = "Scriptable Objects/Curve Bullet Wave")]
public class CurveBulletWave : BulletsWaveSettings
{
    [SerializeField]
    private AnimationCurve _RadialMaskPattern;

    [SerializeField]
    private AnimationCurve _RadialTimePattern;

    [SerializeField]
    private AnimationCurve _RadialSpeedPattern;

    [SerializeField]
    private float _FirePeriod = 1f;

    [SerializeField]
    private float _BulletsBaseSpeed = 1f;

    [SerializeField]
    private Bullet _BulletPrefab = null;

    private float _lastPeriodStartedTimeStamp = 0f;

    private void OnEnable()
    {
        _lastPeriodStartedTimeStamp = 0f;
    }

    public override List<BulletData> GetBullets()
    {
        if (TimeLord.Instance.Now - _lastPeriodStartedTimeStamp > _FirePeriod)
        {
            _lastPeriodStartedTimeStamp = TimeLord.Instance.Now;
        }

        float rationInPeriod = (TimeLord.Instance.Now - _lastPeriodStartedTimeStamp) / _FirePeriod;

        List<BulletData> bullets = new List<BulletData>();

        for (int i = 0; i < _NumberOfBullets; i++)
        {
            float degree = i * 360f / _NumberOfBullets;
            float rationDegree = degree / 360f;

            if (_RadialMaskPattern.Evaluate(rationDegree) < 0.5f)
            {
                continue;
            }

            if (Mathf.Abs(_RadialTimePattern.Evaluate(rationDegree) - rationInPeriod) < 0.01f)
            {
                BulletData newBullet = new BulletData()
                {
                    Speed = _BulletsBaseSpeed * _RadialSpeedPattern.Evaluate(rationDegree),
                    Angle = i * 360f / _NumberOfBullets,
                    Prefab = _BulletPrefab
                };

                bullets.Add(newBullet);
            }
        }

        return bullets;
    }
}
