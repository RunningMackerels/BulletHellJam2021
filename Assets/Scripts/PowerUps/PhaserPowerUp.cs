using UnityEngine;

namespace PowerUps
{
    public class PhaserPowerUp : PowerUp
    {
        [SerializeField]
        [Range(0f, 180f)]
        private float _MaxTurning = 45f;

        public override void ActivatePowerUp(Player player)
        {
            PowerUpManager.Instance.TriggerPhaser(_MaxTurning);

            base.ActivatePowerUp(player);
        }
    }
}