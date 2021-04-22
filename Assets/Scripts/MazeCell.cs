using UnityEngine;

public class MazeCell : MonoBehaviour
{
    public enum CellType
    {
        Wall,
        Empty
    }

    [SerializeField]
    private CellType _cellType;

    public CellType TheCellType => _cellType;

    public void Init()
    {
        Maze maze = FindObjectOfType<Maze>();
        transform.GetComponentInParent<MazeFiller>().Register(transform.position * maze.WorldScale, this);


        if(_cellType == CellType.Wall)
        {
            Maze.Direction direction = transform.parent.GetComponent<CubeDirection>().Direction;
            switch (direction)
            {
                case (Maze.Direction.East | Maze.Direction.West):
                    transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    break;
                case (Maze.Direction.North | Maze.Direction.South):
                    transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                    break;

            }

        }
    }
}
