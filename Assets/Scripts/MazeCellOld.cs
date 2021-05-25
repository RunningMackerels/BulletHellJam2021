using UnityEngine;

public class MazeCellOld : MonoBehaviour
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
        MazeOld maze = FindObjectOfType<MazeOld>();
        transform.GetComponentInParent<MazeFiller>().Register(transform.position * maze.WorldScale, this);


        if(_cellType == CellType.Wall)
        {
            MazeOld.Direction direction = transform.parent.GetComponent<CubeDirection>().Direction;
            switch (direction)
            {
                case (MazeOld.Direction.East | MazeOld.Direction.West):
                    transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    break;
                case (MazeOld.Direction.North | MazeOld.Direction.South):
                    transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                    break;

            }

        }
    }
}
