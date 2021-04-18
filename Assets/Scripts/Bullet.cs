using PowerUps;
using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 1f;

    private int _damage = 1;

    [SerializeField]
    private Color _NormalColor = Color.red;
    [SerializeField]
    private Color _LightColor = Color.yellow;

    private Collider _collider = null;
    private Renderer _renderer = null;

    public void Initialize(float speed, int damage, bool lightBullet)
    {
        _speed = speed;
        _damage = damage;

        _collider = GetComponent<Collider>();
        _renderer = GetComponentInChildren<Renderer>();
        ToggleLightBullet(lightBullet);

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

    public void ToggleLightBullet(bool state)
    {
        _collider.enabled = !state;
        _renderer.material.color = state ? _LightColor : _NormalColor;
    }

    internal void PhaseIt(float maxTurning)
    {
        float rotationAngle = UnityEngine.Random.Range(-maxTurning, maxTurning);

        transform.Rotate(Vector3.up, rotationAngle);
    }
}
