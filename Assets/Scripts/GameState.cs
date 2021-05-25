using RM;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : Singleton<GameState>
{
    [SerializeField]
    private int _cubingerScore = 100;

    [SerializeField]
    private LevelSettings _levelSettings;
    
    public int Score { private set; get; } = 0;

    private int _currentLevel = 0;
    private float _timeFromLastLevel = 0f;
    
    public void StartNewLevel()
    {
        _currentLevel++;
        TimeLord.Instance.SetLevelTime(_timeFromLastLevel + _levelSettings.GetLevel(_currentLevel).TimeToAdd);
    }
    
    public void CubingerGrabbed()
    {
        Score += _cubingerScore;
        _timeFromLastLevel = TimeLord.Instance.LevelTime;
        SceneManager.LoadScene("PreMain");
    }

    public void AddScore(int newScore)
    {
        Score += newScore;
    }

    internal void GameOver()
    {
        SceneManager.LoadScene("Start");
        _timeFromLastLevel = 0f;
        Score = 0;
        _currentLevel = 0;
    }
}
