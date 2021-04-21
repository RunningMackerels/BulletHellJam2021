using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private Transform _player;

    [SerializeField] 
    private float dumpTime = 0.3f;
    
    private Vector3 _offset = Vector3.zero;
    private Vector3 _velocity;


    // Start is called before the first frame update
    private void Start()
    {
        _offset = _player.position - transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_player == null)
        {
            return;
        }

        Vector3 targetPosition = _player.position - _offset;
        
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, dumpTime);
    }

    public void RegisterPlayer(Transform player)
    {
        _player = player;
    }
}
