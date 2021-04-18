using UnityEngine;

namespace PowerUps
{
    public class PhaserPowerUp : MonoBehaviour, IPowerUp
    {
        [SerializeField]
        [Range(0f, 180f)]
        private float _MaxTurning = 45f;

        [SerializeField]
        private float _PowerUpDurantion = 2f;

        [SerializeField]
        private int _score = 10;

        public void ActivatePowerUp(Player player)
        {
            PowerUpManager.Instance.TriggerPhaser(_MaxTurning);
            GameState.Instance.AddScore(_score);
            Destroy(gameObject);
        }
    }
}