using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PowerUps
{
    public class LHCPowerUp : PowerUp
    {
        [SerializeField]
        private float _powerUpDurantion = 2f;

        [SerializeField]
        private float _maxSpeed = 2f;

        [SerializeField]
        private AnimationCurve _damping;

        public override void ActivatePowerUp(Player player)
        {
            PowerUpManager.Instance.TriggerLHC(_powerUpDurantion, _maxSpeed, _damping, player);

            base.ActivatePowerUp(player);
        }
    }
}