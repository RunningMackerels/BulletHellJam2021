using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PowerUps
{
    public class LHCPowerUp : MonoBehaviour, IPowerUp
    {
        [SerializeField]
        private float _powerUpDurantion = 2f;

        [SerializeField]
        private float _maxSpeed = 2f;

        [SerializeField]
        private AnimationCurve _damping;

        [SerializeField]
        private int _score = 10;

        public void ActivatePowerUp(Player player)
        {
            PowerUpManager.Instance.TriggerLHC(_powerUpDurantion, _maxSpeed, _damping, player);
            GameState.Instance.AddScore(_score);
            Destroy(gameObject);
        }
    }
}