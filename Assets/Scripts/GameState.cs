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

    public int Score { private set; get; } = 0;

    [SerializeField]
    private int _CubbingerScore = 100;

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
        Score += _CubbingerScore;
        Debug.Log("WIN");
        SceneManager.LoadScene("Main");
    }

    public void AddScore(int newScore)
    {
        Score += newScore;
    }

    internal void GameOver()
    {
        //it should be the start scene when we have it
        Debug.Log("GAME OVER");
        SceneManager.LoadScene("Main");
    }
}
