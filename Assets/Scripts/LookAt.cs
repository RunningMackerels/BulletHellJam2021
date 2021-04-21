using UnityEngine;

public class LookAt : MonoBehaviour
{
    private Transform _target = null;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target == null)
        {
            return;
        }

        transform.LookAt(_target);
    }
}
