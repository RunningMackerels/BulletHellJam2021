using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{

    [SerializeField] 
    private float velocity = 2f;

    private Vector3 _direction = Vector3.zero;

    private Animator _animator;
    private static readonly int Running = Animator.StringToHash("Running");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void OnMove(InputValue value)
    {
        var movement = value.Get<Vector2>();
        _direction = new Vector3(movement.x, 0f, movement.y);
        _animator.SetBool(Running, movement.sqrMagnitude > 0f);
    }

    private void FixedUpdate()
    {
        transform.position += _direction * (velocity * TimeLord.Instance.DeltaTime);
    }
}
