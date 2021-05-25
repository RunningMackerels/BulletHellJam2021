using System;
using System.Collections;
using System.Collections.Generic;
using RM;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Maze
{
    public class Maze
    {
        protected Grid<Cell> _grid;

        public Maze(int width, int height)
        {
            _grid = new Grid<Cell>(width, height);
        }

        public Maze(Vector2Int size)
        {
            _grid = new Grid<Cell>(size.x, size.y);
        }

        public void Resize(int width, int height)
        {
            _grid = null;
            _grid = new Grid<Cell>(width, height);
        }

        public void Resize(Vector2Int size)
        {
            Resize(size.x, size.y);
        }

        public bool FloodFill()
        {
            foreach (Cell cell in _grid)
            {
                cell.Visited = false;
            }
            
            Queue<Vector2Int> cellsToVisit = new Queue<Vector2Int>();
            cellsToVisit.Enqueue(Vector2Int.zero);

            int visitedCells = 0;
            Vector2Int[] directions = {Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left}; 
            while (cellsToVisit.Count > 0)
            {
                Vector2Int position = cellsToVisit.Dequeue();
                ++visitedCells;
                _grid[position].Visited = true;
                
                foreach (Vector2Int direction in directions)
                {
                    Vector2Int test = position + direction;
                    if (test.x < 0 
                        || test.y < 0 
                        || test.x > _grid.GetLength(0) 
                        || test.y > _grid.GetLength(1))
                    {
                        continue;
                    }

                    Cell cell = _grid[test];
                    if (cell.Visited)
                    {
                        continue;
                    }

                    if (direction.x > 0 && (cell.Walls & Cell.Wall.East) == 0)
                    {
                        cellsToVisit.Enqueue(test);
                    }

                    if (direction.x < 0 && (cell.Walls & Cell.Wall.West) == 0)
                    {
                        cellsToVisit.Enqueue(test);
                    }

                    if (direction.y > 0 && (cell.Walls & Cell.Wall.North) == 0)
                    {
                        cellsToVisit.Enqueue(test);
                    }

                    if (direction.y < 0 && (cell.Walls & Cell.Wall.South) == 0)
                    {
                        cellsToVisit.Enqueue(test);
                    }
                }
            }

            return visitedCells == _grid.Count;
        }
    }
}