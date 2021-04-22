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

            AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position);
        }
    }
}