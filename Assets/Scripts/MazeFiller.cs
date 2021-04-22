using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeFiller : MonoBehaviour
{
    [SerializeField]
    private float _diameterOfSpawn = 5f;

    [Header("Walls")]
    [SerializeField]
    private GameObject[] _wallObjectsPrefabs;

    [SerializeField]
    private float _percentageOfWall = 0.7f;

    [SerializeField]
    private Transform _wallsRoot;

    [Header("Turrets")]
    [SerializeField]
    private GameObject[] _turretPrefabs;

    [SerializeField]
    private int _minNumberOfTurrets = 2;

    [SerializeField]
    private int _maxNumberOfTurrets = 4;

    [SerializeField]
    private Transform _turretRoot;

    [Header("Albert")]
    [SerializeField]
    private GameObject _albertSpawnPoint;

    [SerializeField]
    private GameObject _albertPrefab;
  

    [Header("Cubinger")]
    [SerializeField]
    private GameObject _cubingerSpawnPoint;

    [SerializeField]
    private GameObject _cubingerPrefab;

    [Header("PowerUps")]
    [SerializeField]
    private GameObject[] _powerUpsPrefabs;

    [SerializeField]
    private Transform _powerUpsRoot;

    [SerializeField]
    private int _minNumberOfPowerUps = 8;
    [SerializeField]
    private int _maxNumberOfPowerUps = 12;

    private Dictionary<Vector3, MazeCell.CellType> _mazeCells = new Dictionary<Vector3, MazeCell.CellType>();

    public void Fill()
    {
        SplitTypes(out List<Vector3> walls, out List<Vector3> empties);

        //solid walls
        List<Vector3> solidWalls = GetRandomPartOfList(ref walls, Mathf.RoundToInt(_percentageOfWall * walls.Count), true);
        solidWalls.ForEach(cell => Instantiate(_wallObjectsPrefabs[Random.Range(0, _wallObjectsPrefabs.Length)], cell, Quaternion.identity, _wallsRoot));

        //turrets
        int numberOfTurrets = Mathf.RoundToInt(Random.Range(_minNumberOfTurrets, _maxNumberOfTurrets));
        List<Vector3> turretPositions = GetRandomPartOfList(ref walls, numberOfTurrets, true);
        turretPositions.ForEach(cell => Instantiate(_turretPrefabs[Random.Range(0, _turretPrefabs.Length)], cell, Quaternion.identity, _turretRoot));

        //albert
        Vector2 position = Random.insideUnitCircle * _diameterOfSpawn;
        GameObject albert =  Instantiate(_albertPrefab, _albertSpawnPoint.transform.position + new Vector3(position.x, 0f, position.y), Quaternion.identity, _albertSpawnPoint.transform);

        //cubinger
        position = Random.insideUnitCircle * _diameterOfSpawn;
        GameObject go = Instantiate(_cubingerPrefab, _cubingerSpawnPoint.transform.position + new Vector3(position.x, 0f, position.y), Quaternion.identity, _cubingerSpawnPoint.transform);
        go.AddComponent<LookAt>().SetTarget(albert.transform);

        //power ups
        int numberOfPowerUps = Mathf.RoundToInt(Random.Range(_minNumberOfPowerUps, _maxNumberOfPowerUps));
        List<Vector3> powerUpsPositions = GetRandomPartOfList(ref empties, numberOfPowerUps, true);
        powerUpsPositions.ForEach(cell => Instantiate(_powerUpsPrefabs[Random.Range(0, _powerUpsPrefabs.Length)], cell, Quaternion.identity, _powerUpsRoot));
    }

    internal void Register(Vector3 position, MazeCell.CellType type)
    {
        _mazeCells[position] = type;
    }

    private void SplitTypes(out List<Vector3> walls, out List<Vector3> empties)
    {
        walls = new List<Vector3>();
        empties = new List<Vector3>();
        foreach (KeyValuePair<Vector3, MazeCell.CellType> cell in _mazeCells)
        {
            switch(cell.Value)
            {
                case MazeCell.CellType.Empty:
                    empties.Add(cell.Key);
                    break;
                case MazeCell.CellType.Wall:
                    walls.Add(cell.Key);
                    break;
            }
        }
    }

    private List<Vector3> GetRandomPartOfList(ref List<Vector3> source, int numberOfElements, bool removeFromSource)
    {
        List<Vector3> output = new List<Vector3>();
        while (output.Count < numberOfElements)
        {
            int id = Random.Range(0, source.Count);
            output.Add(source[id]);
            if (removeFromSource)
            {
                source.RemoveAt(id);
            }
        }
        return output;
    }
}
