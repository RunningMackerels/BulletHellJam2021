using System;
using System.Collections;
using System.Collections.Generic;
using RM;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : Singleton<GameState>
{
    public int Score { private set; get; } = 0;

    [SerializeField]
    private int _CubbingerScore = 100;

    public void CubingerGrabbed()
    {
        Score += _CubbingerScore;
        SceneManager.LoadScene("PreMain");
    }

    public void AddScore(int newScore)
    {
        Score += newScore;
    }

    internal void GameOver()
    {
        SceneManager.LoadScene("Start");
        Score = 0;
    }
}
