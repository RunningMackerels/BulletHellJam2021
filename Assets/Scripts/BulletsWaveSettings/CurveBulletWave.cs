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

        float ratioInPeriod = (TimeLord.Instance.Now - _lastPeriodStartedTimeStamp) / _FirePeriod;

        List<BulletData> bullets = new List<BulletData>();

        for (int i = 0; i < _NumberOfBullets; i++)
        {
            float degree = i * 360f / _NumberOfBullets;
            float ratioDegree = degree / 360f;

            if (_RadialMaskPattern.Evaluate(ratioDegree) < 0.5f)
            {
                continue;
            }

            if (Mathf.Abs(_RadialTimePattern.Evaluate(ratioDegree) - ratioInPeriod) < 0.01f)
            {
                GenerateBulletsData(_BulletsBaseSpeed * _RadialSpeedPattern.Evaluate(ratioDegree),
                                    i * 360f / _NumberOfBullets,
                                    _BulletPrefab,
                                    ref bullets);
            }
        }

        return bullets;
    }

    protected virtual void GenerateBulletsData(float speed, float angle, Bullet prefab, ref List<BulletData> bulletData)
    {
        BulletData newBullet = new BulletData()
        {
            Speed = speed,
            Angle = angle,
            Prefab = prefab
        };

        bulletData.Add(newBullet);
    }
}
