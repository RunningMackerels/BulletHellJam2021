using PowerUps;
using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 1f;

    private int _damage = 1;

    public void Initialize(float speed, int damage)
    {
        _speed = speed;
        _damage = damage;

        PowerUpManager.Instance.RegisterBullet(this);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * _speed * TimeLord.Instance.DeltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.Damage(_damage);
        }

        PowerUpManager.Instance.UnregisterBullet(this);
        Destroy(gameObject);
    }
}
