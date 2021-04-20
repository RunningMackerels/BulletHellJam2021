using UnityEngine;

namespace PowerUps
{
    public class QuantumStatePowerUp : PowerUp
    {
        [SerializeField]
        private float _powerUpDurantion = 2f;

        public override void ActivatePowerUp(Player player)
        {
            PowerUpManager.Instance.TriggerQuantumState(_powerUpDurantion, player);

            base.ActivatePowerUp(player);
        }
    }
}