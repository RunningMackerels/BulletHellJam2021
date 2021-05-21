using System.Collections;
using System.Collections.Generic;
using RM;
using UnityEngine;
using UnityEngine.AI;

namespace PowerUps
{
    public class PowerUpManager : Singleton<PowerUpManager>
    {
        private List<Bullet> _activeBullets = new List<Bullet>();

        private List<Collider> _obstacles = new List<Collider>();

        #region Light Bullets Members
        private IEnumerator _delayedLightBulletsStop = null;

        public bool LightBulletsActive => _delayedLightBulletsStop != null;
        #endregion

        #region Black Hole Members
        private IEnumerator _delayedBlackHoleStop = null;
        public bool BlackHoleActive => _delayedBlackHoleStop != null;
        #endregion

        #region Quantum State Members
        private IEnumerator _delayedQuantumStateStop = null;
        public bool QuantumStateActive => _delayedQuantumStateStop != null;

        #endregion

        public void RegisterBullet(Bullet bullet)
        {
            _activeBullets.Add(bullet);
        }

        public void UnregisterBullet(Bullet bullet)
        {
            _activeBullets.Remove(bullet);
        }

        public void RegisterObstacle(Collider obstacle)
        {
            _obstacles.Add(obstacle);
        }

        public void UnregisterObstacle(Collider obstacle)
        {
            _obstacles.Remove(obstacle);
        }

        #region Light Bullets
        public void TriggerLightBullets(float delay)
        {
            ToggleLightBullets(true);

            if (_delayedLightBulletsStop != null)
            {
                StopCoroutine(_delayedLightBulletsStop);
            }
            _delayedLightBulletsStop = DelayStopLightBullets(delay);
            StartCoroutine(_delayedLightBulletsStop);
        }

        private void ToggleLightBullets(bool state)
        {
            _activeBullets.ForEach(bullet => bullet.ToggleLightBullet(state));
        }

        private IEnumerator DelayStopLightBullets(float delay)
        {
            yield return new WaitForSeconds(delay / TimeLord.Instance.SpeedMultiplier);

            ToggleLightBullets(false);

            _delayedLightBulletsStop = null;
        }
        #endregion

        #region Black Hole
        public void TriggerBlackHole(float slowDownMultiplier, float delay)
        {
            TimeLord.Instance.SpeedMultiplier /= slowDownMultiplier;
            
            if (_delayedBlackHoleStop != null)
            {
                StopCoroutine(_delayedBlackHoleStop);
            }
            _delayedBlackHoleStop = DelayStopBlackHole(slowDownMultiplier, delay);
            StartCoroutine(_delayedBlackHoleStop);
        }

        private IEnumerator DelayStopBlackHole(float slowDownMultiplier, float delay)
        {
            yield return new WaitForSeconds(delay / TimeLord.Instance.SpeedMultiplier);

            TimeLord.Instance.SpeedMultiplier *= slowDownMultiplier;

            _delayedBlackHoleStop = null;
        }
        #endregion

        #region Quantum State
        public void TriggerQuantumState(float delay, Player player)
        {
            player.ToggleQuantumState(true);
            _obstacles.ForEach(obstacle => obstacle.enabled = false);
            if (_delayedQuantumStateStop != null)
            {
                StopCoroutine(_delayedQuantumStateStop);
            }
            _delayedQuantumStateStop = DelayStopQuantumState(delay, player);
            StartCoroutine(_delayedQuantumStateStop);
        }

        private IEnumerator DelayStopQuantumState(float delay, Player player)
        {
            yield return new WaitForSeconds(delay / TimeLord.Instance.SpeedMultiplier);

            player.ToggleQuantumState(false);
            _obstacles.ForEach(obstacle => obstacle.enabled = true);

            _delayedQuantumStateStop = null;
        }
        #endregion

        #region Phaser
        public void TriggerPhaser(float maxTurning)
        {
            _activeBullets.ForEach(bullet => bullet.PhaseIt(maxTurning));
        }
        #endregion

        #region LHC
        public void TriggerLHC(float duration, float maxSpeed, AnimationCurve damping, Player player)
        {
            player.TriggerLHC(duration, maxSpeed, damping);
        }
        #endregion
    }
}