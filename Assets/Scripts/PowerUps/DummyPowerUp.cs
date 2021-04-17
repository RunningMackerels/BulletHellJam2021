using UnityEngine;

namespace PowerUps
{
    public class DummyPowerUp : MonoBehaviour, IPowerUp
    {
        public void ActivatePowerUp(Player player)
        {
            Debug.LogWarning("Power up grabber by " + player.gameObject.name);

            gameObject.SetActive(false);
        }
    }
}