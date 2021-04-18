using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PowerUps
{
    public class PowerUpManager : MonoBehaviour
    {
        #region Singleton
        private static PowerUpManager _instance;

        public static PowerUpManager Instance => _instance;
        #endregion //Singleton

        private List<Bullet> _activeBullets = new List<Bullet>();

        #region Light Bullets Members
        private IEnumerator _delayedLightBulletsStop = null;

        public bool LightBulletsActive => _delayedLightBulletsStop != null;
        #endregion

        #region Black Hole Members
        private IEnumerator _delayedBlackHoleStop = null;
        public bool BlackHoleActive => _delayedBlackHoleStop != null;
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
    }
}