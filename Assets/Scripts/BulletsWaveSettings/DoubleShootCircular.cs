using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleBulletWave", menuName = "Scriptable Objects/Double Curve Bullet Wave")]
public class DoubleShootCircular : CurveBulletWave
{
    [SerializeField]
    private Vector3 offset = new Vector3(0f, 0.2f, 0f);

    protected override void GenerateBulletsData(float speed, float angle, Bullet prefab, ref List<BulletData> bulletData)
    {
        bulletData.Add(new BulletData()
        {
            Speed = speed,
            Angle = angle,
            Prefab = prefab,
            LocalOffset = offset * 0.5f
        });

        bulletData.Add(new BulletData()
        {
            Speed = speed,
            Angle = angle,
            Prefab = prefab,
            LocalOffset = offset * -0.5f
        });
    }
}
