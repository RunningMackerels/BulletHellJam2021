using PowerUps;
using UnityEngine;
using UnityEngine.AI;

public class ObstacleRegisterer : MonoBehaviour
{
    [SerializeField]
    private Collider _ObstacleCollider = null;

    private void OnEnable()
    {
        if (_ObstacleCollider == null)
        {
            _ObstacleCollider = GetComponent<Collider>();
        }
        PowerUpManager.Instance.RegisterObstacle(_ObstacleCollider);
    }

    private void OnDisable()
    {
        if (_ObstacleCollider == null)
        {
            return;
        }
        PowerUpManager.Instance.UnregisterObstacle(_ObstacleCollider);
    }
}
