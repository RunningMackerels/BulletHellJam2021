using UnityEngine;

/// <summary>
/// Resposible for trigger the start of a new level;
/// </summary>
public class StartLevel : MonoBehaviour
{
    private void Awake()
    {
        GameState.Instance.StartNewLevel();
    }
}
