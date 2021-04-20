using UnityEngine;

namespace PowerUps
{
    public class LightBulletsPowerUp : PowerUp
    {
        [SerializeField]
        private float _powerUpDurantion = 2f;


        public override void ActivatePowerUp(Player player)
        {
            PowerUpManager.Instance.TriggerLightBullets(_powerUpDurantion);

            base.ActivatePowerUp(player);
        }
    }
}