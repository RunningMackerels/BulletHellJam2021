using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "Scriptable Objects/Level Settings")]
public class LevelSettings : ScriptableObject
{
    [Serializable]
    public class Level
    {
        public int LevelID = 0;
        public float TimeToAdd = 10f;
    }

    [SerializeField]
    private List<Level> _mLevels = new List<Level>();

    [SerializeField]
    private int _maxLevel = 20;

    private Dictionary<int, Level> _levelsMap = new Dictionary<int, Level>();
    
    
    private void OnEnable()
    {
        _levelsMap = _mLevels.ToDictionary(level => level.LevelID, level => level);

        for (int i = 1; i <= _maxLevel; i++)
        {
            if (!_levelsMap.ContainsKey(i))
            {
                _levelsMap[i] = _levelsMap[i - 1];
            }
        }
    }

    public Level GetLevel(int id)
    {
        return id >= _maxLevel ? _levelsMap[_maxLevel] : _levelsMap[id];
    }
}
