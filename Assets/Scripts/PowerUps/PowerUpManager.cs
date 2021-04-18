using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PowerUps
{
    public class PowerUpManager : MonoBehaviour
    {
        #region Singleton
        private static PowerUpManager _instance;

        public static PowerUpManager Instance => _instance;
        #endregion //Singleton

        private List<Bullet> _activeBullets = new List<Bullet>();

        private List<NavMeshObstacle> _obstacles = new List<NavMeshObstacle>();

        #region Light Bullets Members
        private IEnumerator _delayedLightBulletsStop = null;

        public bool LightBulletsActive => _delayedLightBulletsStop != null;
        #endregion

        #region Black Hole Members
        private IEnumerator _delayedBlackHoleStop = null;
        public bool BlackHoleActive => _delayedBlackHoleStop != null;
        #endregion

        #region Quantum State
        private IEnumerator _delayedQuantumStateStop = null;
        public bool QuantumStateActive => _delayedQuantumStateStop != null;

        #endregion

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                _instance = this;
            }

            DontDestroyOnLoad(this);
        }

        public void RegisterBullet(Bullet bullet)
        {
            _activeBullets.Add(bullet);
        }

        public void UnregisterBullet(Bullet bullet)
        {
            _activeBullets.Remove(bullet);
        }

        public void RegisterObstacle(NavMeshObstacle obstable)
        {
            _obstacles.Add(obstable);
        }

        public void UnregisterObstacle(NavMeshObstacle obstable)
        {
            _obstacles.Remove(obstable);
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
    }
}