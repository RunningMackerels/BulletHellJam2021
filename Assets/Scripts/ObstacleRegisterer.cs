using PowerUps;
using UnityEngine;
using UnityEngine.AI;

public class ObstacleRegisterer : MonoBehaviour
{
    [SerializeField]
    private Collider _ObstacleCollider = null;

    private void OnEnable()
    {
        PowerUpManager.Instance.RegisterObstacle(_ObstacleCollider);
    }

    private void OnDisable()
    {
        PowerUpManager.Instance.UnregisterObstacle(_ObstacleCollider);
    }
}
