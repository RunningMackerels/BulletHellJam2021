using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    #region Singleton
    private static GameState _instance;

    public static GameState Instance => _instance;
    #endregion //Singleton

    private int _score = 0;

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

    public void CubingerGrabbed()
    {
        Debug.LogError("You win, go to next level now");
    }

    public void AddScore(int newScore)
    {
        _score += newScore;
    }

    internal void GameOver()
    {
        //it should be the start scene when we have it
        Debug.LogError("GAME OVER");
        SceneManager.LoadSceneAsync("Main");
    }
}
