using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField] 
    private float dumpTime = 0.3f;
    
    private Vector3 _offset = Vector3.zero;
    private Vector3 _velocity;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _offset = player.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = player.position - _offset;
        
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, dumpTime);
    }
}
