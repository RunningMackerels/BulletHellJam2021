using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentAudio : MonoBehaviour
{
    #region Singleton
    private static EnvironmentAudio _instance;

    public static EnvironmentAudio Instance => _instance;
    #endregion //Singleton

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
}
