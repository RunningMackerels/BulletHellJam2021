using UnityEngine;

namespace PowerUps
{
    public class TimeWarpPowerUp : MonoBehaviour, IPowerUp
    {
        [SerializeField]
        private float _MinTimeWarp = 2f;
        [SerializeField]
        private float _MaxTimeWarp = 2f;

        [SerializeField]
        private int _score = 10;

        public void ActivatePowerUp(Player player)
        {
            float warpTime = Random.Range(_MinTimeWarp, _MaxTimeWarp);
            float sign = Random.value > 0.5f ? 1f : -1;

            TimeLord.Instance.ChangeLevelTime(warpTime * sign);
            GameState.Instance.AddScore(_score);
            Destroy(gameObject);
        }
    }
}