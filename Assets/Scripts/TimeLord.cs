using UnityEngine;

public class TimeLord : MonoBehaviour
{
    #region Singleton
    private static TimeLord _instance;

    public static TimeLord Instance => _instance;
    #endregion //Singleton

    //serialized only for debug
    [SerializeField]
    private float _speedMultiplier = 1;

    /// <summary>
    /// Same concept of Unity deltaTime but converted according to the current speed multiplier
    /// </summary>
    public float DeltaTime => Time.deltaTime * _speedMultiplier;
    
    public float RawDeltaTime => Time.deltaTime;

    public float Now => Time.time;

    public float SpeedMultiplier => _speedMultiplier;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void SetSpeed(float speed)
    {
        _speedMultiplier = speed;
    }
}
