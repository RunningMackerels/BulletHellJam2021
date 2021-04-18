using PowerUps;
using UnityEngine;
using UnityEngine.AI;

public class ObstacleRegisterer : MonoBehaviour
{
    [SerializeField]
    private NavMeshObstacle _Obstacle = null;

    private void OnEnable()
    {
        PowerUpManager.Instance.RegisterObstacle(_Obstacle);
    }

    private void OnDisable()
    {
        PowerUpManager.Instance.UnregisterObstacle(_Obstacle);
    }
}
