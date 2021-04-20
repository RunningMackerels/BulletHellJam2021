using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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


    [SerializeField]
    Vector2Int size = Vector2Int.zero;

    private Direction[,] _grid;

    private Direction[] _directions = { Direction.East, Direction.West, Direction.North, Direction.South };

    private void Awake() => DoMaze();

    private void DoMaze()
    {
        InitGrid();
        List<Vector2Int> visitedCells = new List<Vector2Int>();

        Vector2Int initialCell = Vector2Int.zero;
        initialCell.y = Random.Range(0, size.y);

        visitedCells.Add(initialCell);

        while(visitedCells.Count > 0)
        {
            int nextIndex = NextCell(visitedCells.Count);
            Vector2Int position = visitedCells[nextIndex];

            foreach(Direction dir in _directions.OrderBy(x => Random.value).ToList())
            {
                Vector2Int next = position + Delta(dir);
                if(next.x >= 0 && next.y >= 0 && next.x < size.x && next.y < size.y)
                {
                    _grid[next]
                }
            }
        }
    }

    private int NextCell(int index)
    {
        return index - 1;
    }

    private void InitGrid()
    {
        _grid = new Direction[size.x, size.y];


        for (int i = 0; i < size.x; ++i)
        {
            for (int j = 0; j < size.y; ++j)
            {
                _grid[i, j] = Direction.None;
            }
        }
    }

}
