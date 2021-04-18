using UnityEngine;

namespace PowerUps
{
    public class QuantumStatePowerUp : MonoBehaviour, IPowerUp
    {
        [SerializeField]
        private float _powerUpDurantion = 2f;

        [SerializeField]
        private int _score = 10;

        public void ActivatePowerUp(Player player)
        {
            PowerUpManager.Instance.TriggerQuantumState(_powerUpDurantion, player);
            GameState.Instance.AddScore(_score);
            Destroy(gameObject);
        }
    }
}