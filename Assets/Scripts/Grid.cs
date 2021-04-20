using UnityEngine;

public class Grid<T>
{
    private T[,] _grid;

    public T this[Vector2Int pos]
    {
        get => _grid[pos.x, pos.y];
        set => _grid[pos.x, pos.y] = value;
    }

    public T this[int x, int y]
    {
        get => _grid[x, y];
        set => _grid[x, y] = value;
    }

    public Grid(int width, int height)
    {
        _grid = new T[width, height];
    }

    public int GetLength(int dimension)
    {
        return _grid.GetLength(dimension);
    }
}