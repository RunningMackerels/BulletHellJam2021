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
    private GameObject[] _specialObjectsPrefabs;

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

    private Dictionary<Vector3, MazeCell> _mazeCells = new Dictionary<Vector3, MazeCell>();

    public void Fill()
    {
        SplitTypes(out List<Transform> walls, out List<Transform> empties);

        //solid walls
        List<Transform> solidWalls = GetRandomPartOfList(ref walls, Mathf.RoundToInt(_percentageOfWall * walls.Count), true);
        solidWalls.ForEach(cell => Instantiate(_wallObjectsPrefabs[Random.Range(0, _wallObjectsPrefabs.Length)], cell.position, cell.rotation, _wallsRoot));

        //speacial walls
        List<Transform> specialWalls = GetRandomPartOfList(ref walls, _specialObjectsPrefabs.Length, true);
        specialWalls.ForEach(cell => Instantiate(_specialObjectsPrefabs[Random.Range(0, _specialObjectsPrefabs.Length)], cell.position, cell.rotation, _wallsRoot));


        //turrets
        int numberOfTurrets = Random.Range(_minNumberOfTurrets, _maxNumberOfTurrets + 1);
        List<Transform> turretPositions = GetRandomPartOfList(ref walls, numberOfTurrets, true);
        turretPositions.ForEach(cell => Instantiate(_turretPrefabs[Random.Range(0, _turretPrefabs.Length)], cell.position, Quaternion.identity, _turretRoot));

        //albert
        Vector2 position = Random.insideUnitCircle * _diameterOfSpawn;
        GameObject albert =  Instantiate(_albertPrefab, _albertSpawnPoint.transform.position + new Vector3(position.x, 0f, position.y), Quaternion.identity, _albertSpawnPoint.transform);

        //cubinger
        position = Random.insideUnitCircle * _diameterOfSpawn;
        GameObject go = Instantiate(_cubingerPrefab, _cubingerSpawnPoint.transform.position + new Vector3(position.x, 0f, position.y), Quaternion.identity, _cubingerSpawnPoint.transform);
        go.AddComponent<LookAt>().SetTarget(albert.transform);

        //power ups
        int numberOfPowerUps = Random.Range(_minNumberOfPowerUps, _maxNumberOfPowerUps + 1);
        List<Transform> powerUpsPositions = GetRandomPartOfList(ref empties, numberOfPowerUps, true);
        powerUpsPositions.ForEach(cell => Instantiate(_powerUpsPrefabs[Random.Range(0, _powerUpsPrefabs.Length)], cell.position, Quaternion.identity, _powerUpsRoot));
    }

    internal void Register(Vector3 position, MazeCell type)
    {
        _mazeCells[position] = type;
    }

    private void SplitTypes(out List<Transform> walls, out List<Transform> empties)
    {
        walls = new List<Transform>();
        empties = new List<Transform>();
        foreach (KeyValuePair<Vector3, MazeCell> cell in _mazeCells)
        {
            switch(cell.Value.TheCellType)
            {
                case MazeCell.CellType.Empty:
                    empties.Add(cell.Value.transform);
                    break;
                case MazeCell.CellType.Wall:
                    walls.Add(cell.Value.transform);
                    break;
            }
        }
    }

    private List<Transform> GetRandomPartOfList(ref List<Transform> source, int numberOfElements, bool removeFromSource)
    {
        List<Transform> output = new List<Transform>();
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
