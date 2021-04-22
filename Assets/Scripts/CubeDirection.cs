using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDirection : MonoBehaviour
{
    public Maze.Direction _direction = Maze.Direction.None; //Not according to standards

    public Maze.Direction Direction => _direction;
}
