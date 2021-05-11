using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class MovePlayer : MonoBehaviour
{
    private const float SMALL_NUMBER = 0.01f;
    
    [FormerlySerializedAs("velocity")] [SerializeField] 
    private float _velocity = 2f;

    [SerializeField] 
    private float rampUpTime = 0.3f;
    
    private Vector3 _direction = Vector3.zero;

    private Animator _animator;
    private static readonly int Running = Animator.StringToHash("Running");

    private Vector3 _rampUpDirection = Vector2.zero;
    private Vector3 _smoothingDirection;

    private Vector3 _facing = Vector3.zero;

    private CharacterController _playerController = null;
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerController = GetComponent<CharacterController>();
    }

    private void OnMove(InputValue value)
    {
        var movement = value.Get<Vector2>();
        _direction = new Vector3(movement.x, 0f, movement.y);
        if (_direction.sqrMagnitude > 0)
        {
            _facing = _direction;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rampUpDirection = Vector3.SmoothDamp(_rampUpDirection, _direction, ref _smoothingDirection, rampUpTime);
        _animator.SetBool(Running, _rampUpDirection.sqrMagnitude > SMALL_NUMBER);
        
        _playerController.Move(_rampUpDirection * (_velocity * TimeLord.Instance.DeltaTime));
        
        float angle = _facing.x * 90.0f;
        angle -= _facing.z < 0f ? 180.0f : 0f;
        transform.rotation = Quaternion.Euler(Vector3.up * (angle));
    }
}
