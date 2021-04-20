using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PowerUps
{
    public abstract class PowerUp : MonoBehaviour, IPowerUp
    {
        [SerializeField]
        protected int _score = 10;

        public virtual void ActivatePowerUp(Player player)
        {
            GameState.Instance.AddScore(_score);

            Destroy(GetComponent<Collider>());
            Destroy(transform.GetChild(0).gameObject);

            GetComponent<AudioSource>().Play();
            Invoke("SelfDestruct", GetComponent<AudioSource>().clip.length);
        }

        protected void SelfDestruct()
        {
            Destroy(gameObject);
        }
    }
}