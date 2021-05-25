using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDirection : MonoBehaviour
{
    public MazeOld.Direction _direction = MazeOld.Direction.None; //Not according to standards

    public MazeOld.Direction Direction => _direction;
}
