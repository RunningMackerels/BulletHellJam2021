using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "BulletWave", menuName = "Scriptable Objects/Bullet Wave")]
public abstract class BulletsWaveSettings : ScriptableObject
{
    public abstract List<BulletData> GetBullets();

    public int Damage = 1;

    [SerializeField]
    protected int _NumberOfBullets = 20;
}
