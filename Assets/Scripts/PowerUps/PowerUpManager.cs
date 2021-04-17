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
    }
}