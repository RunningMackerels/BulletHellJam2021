using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class Maze : MonoBehaviour
{
    [Flags]
    public enum Direction
    {
        None  = 0,
        North = 1 << 0,
        South = 1 << 1,
        East  = 1 << 2,
        West  = 1 << 3
    }


    private enum Tile
    {
        None = -1,
        EastWest = 0,
        NorthSouth
    }



    private Direction Opposite(Direction dir)
    {
        switch(dir)
        {
            case Direction.North:
                return Direction.South;
            case Direction.South:
                return Direction.North;
            case Direction.East:
                return Direction.West;
            case Direction.West:
                return Direction.East;
            default:
                return Direction.North;
        }
    }

    private Vector2Int Delta(Direction dir)
    {
        switch (dir)
        {
            case Direction.North:
                return Vector2Int.down;
            case Direction.South:
                return Vector2Int.up;
            case Direction.East:
                return Vector2Int.right;
            case Direction.West:
                return Vector2Int.left;
            default:
                return Vector2Int.zero;
        }
    }



    [Header("Maze Config")]
    [SerializeField]
    private Vector2Int _size = Vector2Int.zero;
    [SerializeField]
    private Vector2Int _blockSize = Vector2Int.zero;

    [SerializeField]
    private Vector3 _offset = Vector3.zero;

    [SerializeField]
    private int _seed = 0;

    [SerializeField]
    private float _worldScale = 2f;

    [Header("Blocks")]
    [SerializeField]
    private GameObject[] _blocks;

    private Grid<Direction> _grid;

    private Direction[] _directions = { Direction.East, Direction.West, Direction.North, Direction.South };

    [SerializeField]
    private bool _drawDebugCubes = false;

    public float WorldScale => _worldScale;


    private void Awake() => DoMaze();



    [ContextMenu("New Maze")]
    private void DoMaze()
    {
        InitGrid();
        InitialMaze();
        DrawMaze();
        GetComponent<MazeFiller>().Fill();
    }

    private int NextCell(int index)
    {

        if(UnityEngine.Random.value < 0.25)
        {
            return index - 1;
        }
        return UnityEngine.Random.Range(0, index);
    }

    private void InitGrid()
    {
        _grid = new Grid<Direction>(_size.x, _size.y);


        for (int x = 0; x < _size.x; ++x)
        {
            for (int y = 0; y < _size.y; ++y)
            {
                _grid[x, y] = Direction.None;
            }
        }
    }

    private void InitialMaze()
    {
        if(_seed > 0)
        {
            UnityEngine.Random.seed = _seed;
        }

        List<Vector2Int> visitedCells = new List<Vector2Int>();

        Vector2Int initialCell = Vector2Int.zero;
        initialCell.x = UnityEngine.Random.Range(0, _size.x);

        visitedCells.Add(initialCell);

        while (visitedCells.Count > 0)
        {
            int nextIndex = NextCell(visitedCells.Count);
            Vector2Int position = visitedCells[nextIndex];

            foreach (Direction dir in _directions.OrderBy(x => UnityEngine.Random.value).ToList())
            {
                Vector2Int next = position + Delta(dir);
                if (next.x >= 0 && next.y >= 0 && next.x < _size.x && next.y < _size.y && _grid[next] == Direction.None)
                {
                    _grid[next] |= dir;
                    _grid[next] |= Opposite(dir);
                    visitedCells.Add(next);
                    nextIndex = -1;
                }
            }
            if (nextIndex >= 0)
            {
                visitedCells.RemoveAt(nextIndex);
            }
        }
    }

    private void DrawMaze()
    {

        Assert.AreNotEqual(0, _blocks.Length);


        int count = 0;
        for (int x = 0; x < _size.x; ++x)
        {
            for (int y = 0; y < _size.y; ++y)
            {
                Tile id = Tile.None;
                Vector3 position;
                GameObject go;

                id = ((_grid[x, y] & Direction.South) != 0) ? Tile.None : Tile.NorthSouth; //South


                if (id >= 0)
                {
                    position = (_offset + new Vector3(x, 0, y)) * _blockSize.y;
                    go = Instantiate(_blocks[(int)id], position, Quaternion.identity);
                    go.transform.parent = transform;
                    go.name = $"Cube.{count++}";
                    go.GetComponent<CubeDirection>()._direction = _grid[x, y];

                    go.GetComponentsInChildren<MazeCell>().ToList().ForEach(item => item.Init());

                    if (_drawDebugCubes)
                    {
                        go.GetComponentsInChildren<Renderer>().ToList().ForEach(item => item.enabled = true);
                    }

                }

                if ((_grid[x, y] & Direction.East) != 0) //East
                {

                    id = ((x + 1 < _size.x && ((_grid[x, y] | _grid[x + 1, y]) & Direction.South) != 0)) ? Tile.None : Tile.NorthSouth;
                } else
                {
                    id = Tile.EastWest;
                }

                if (id >= 0)
                {
                    position = (_offset + new Vector3(x, 0, y)) * _blockSize.y;
                    go = Instantiate(_blocks[(int)id], position, Quaternion.identity);
                    go.transform.parent = transform;
                    go.name = $"Cube.{count++}";
                    go.GetComponent<CubeDirection>()._direction = _grid[x, y];

                    go.GetComponentsInChildren<MazeCell>().ToList().ForEach(item => item.Init());

                    if (_drawDebugCubes)
                    {
                        go.GetComponentsInChildren<Renderer>().ToList().ForEach(item => item.enabled = true);
                    }
                }
            }
        }

        transform.localScale = Vector3.one * WorldScale;
    }

}
