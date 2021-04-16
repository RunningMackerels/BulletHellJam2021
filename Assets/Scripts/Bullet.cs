using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 1f;

    public void Initialize(float speed)
    {
        _speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * _speed * TimeLord.Instance.DeltaTime, Space.Self);
    }
}
