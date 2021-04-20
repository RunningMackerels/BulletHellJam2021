using UnityEngine;

namespace PowerUps
{
    public class TimeWarpPowerUp : PowerUp
    {
        [SerializeField]
        private float _MinTimeWarp = 2f;
        [SerializeField]
        private float _MaxTimeWarp = 2f;

        public override void ActivatePowerUp(Player player)
        {
            float warpTime = Random.Range(_MinTimeWarp, _MaxTimeWarp);
            float sign = Random.value > 0.5f ? 1f : -1;

            TimeLord.Instance.ChangeLevelTime(warpTime * sign);

            base.ActivatePowerUp(player);
        }
    }
}