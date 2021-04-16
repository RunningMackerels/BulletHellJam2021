using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "BulletWave", menuName = "Scriptable Objects/Bullet Wave")]
public abstract class BulletsWaveSettings : ScriptableObject
{
    public abstract List<BulletData> GetBullets();
}
