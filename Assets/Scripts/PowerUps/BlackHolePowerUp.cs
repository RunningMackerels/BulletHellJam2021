using UnityEngine;

namespace PowerUps
{
    public class BlackHolePowerUp : PowerUp
    {
        [SerializeField]
        private float _PowerUpDurantion = 2f;

        [SerializeField]
        private float _SlowDownMultiplier = 2f;

        public override void ActivatePowerUp(Player player)
        {
            PowerUpManager.Instance.TriggerBlackHole(_SlowDownMultiplier, _PowerUpDurantion);

            base.ActivatePowerUp(player);
        }
    }
}