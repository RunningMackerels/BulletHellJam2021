using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    #region Singleton
    private static GameState _instance;

    public static GameState Instance => _instance;
    #endregion //Singleton

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(this);
    }

    public void CubingerGrabbed()
    {
        Debug.LogError("You win, go to next level now");
    }
}
