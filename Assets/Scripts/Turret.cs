using PowerUps;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private BulletsWaveSettings _WaveSettings = null;
    private BulletsWaveSettings _WaveSettingsInstance = null;

    private void Awake()
    {
        _WaveSettingsInstance = Instantiate(_WaveSettings);
    }

    private void Update()
    {
        Fire(_WaveSettingsInstance.GetBullets());
    }

    private void Fire(List<BulletData> bullets)
    {
        if (bullets.Count == 0)
        {
            return;
        }

        foreach(BulletData bullet in bullets)
        {
            Bullet instance = Instantiate(bullet.Prefab, transform.position, Quaternion.Euler(0f, bullet.Angle, 0f), transform);

            instance.Initialize(bullet.Speed, _WaveSettingsInstance.Damage, PowerUpManager.Instance.LightBulletsActive);
        }
    }
}
