using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve _pressAnimationCurve;

    [SerializeField]
    private float pressTime = 2f;

    [SerializeField]
    private float delta = 0.2f;

    private float _currentTime = 0f;

    private Vector3 _direction = Vector3.down;
    private Vector3 _initialPosition = Vector3.zero;

    private void Awake()
    {
        _initialPosition = transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += TimeLord.Instance.DeltaTime;
        if(_currentTime >= pressTime)
        {
            _currentTime -= pressTime;
        }

        float translationY = _pressAnimationCurve.Evaluate(_currentTime * (1.0f/pressTime)) * delta;
        transform.position = _initialPosition + _direction * translationY;
    }
}
