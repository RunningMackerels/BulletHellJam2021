using UnityEngine;

namespace PowerUps
{
    public class BlackHolePowerUp : MonoBehaviour, IPowerUp
    {
        [SerializeField]
        private float _PowerUpDurantion = 2f;

        [SerializeField]
        private float _SlowDownMultiplier = 2f;

        [SerializeField]
        private int _score = 10;

        public void ActivatePowerUp(Player player)
        {
            PowerUpManager.Instance.TriggerBlackHole(_SlowDownMultiplier, _PowerUpDurantion);
            GameState.Instance.AddScore(_score);
            Destroy(gameObject);
        }
    }
}