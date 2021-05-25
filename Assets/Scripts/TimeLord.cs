using System;
using RM;
using UnityEngine;

public class TimeLord : Singleton<TimeLord>
{
    //serialized only for debug
    [SerializeField]
    private float _speedMultiplier = 1;

    /// <summary>
    /// Same concept of Unity deltaTime but converted according to the current speed multiplier
    /// </summary>
    public float DeltaTime => Time.deltaTime * _speedMultiplier;
    
    public float RawDeltaTime => Time.deltaTime;

    public float Now => Time.time;

    public float SpeedMultiplier
    {
        get
        {
            return _speedMultiplier;
        }
        set
        {
            _speedMultiplier = value;
        }
    }

    public float LevelTime => _levelTime;

    private float _levelTime = 0f;
    
    public void Update()
    {
        _levelTime -= DeltaTime;

        if (_levelTime < 0f)
        {
            GameState.Instance.GameOver();
        }
    }

    public void ChangeLevelTime(float delta)
    {
        _levelTime += delta;
    }

    public void SetLevelTime(float time)
    {
        _levelTime = time;
    }
}
