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

    public void Init()
    { 
        transform.GetComponentInParent<MazeFiller>().Register(transform.position * 2f, _cellType);
    }
}
