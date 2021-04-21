using PowerUps;
using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private LayerMask _CubingerLayer;

    [SerializeField]
    private LayerMask _PoweUpsLayer;

    private Collider _collider = null;

    public Action OnLHCFinished;

    [SerializeField]
    private int _InitialHealth = 100;
    private int _health;

    [Header("Audio")]
    [SerializeField]
    private AudioSource _HitAudio;
    [SerializeField]
    private AudioSource _GrabPowerUpAudio;
    [SerializeField]
    private AudioSource _GrabCubingerAudio;

    [Header("Quantum Effect")]
    [SerializeField]
    private Transform _Visuals = null;
    [SerializeField]
    private float _QuantumEffectMagnitude = 0.01f;
    private bool _quantumEffectActive = false;
    private Vector3 _defaultVisualsLocalPosition = Vector3.zero;

    public float HPPercentage => (float)_health / (float)_InitialHealth;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _health = _InitialHealth;

        FindObjectOfType<MoveCamera>().RegisterPlayer(transform);

        _defaultVisualsLocalPosition = _Visuals.localPosition;
    }

    private void Update()
    {
        if (_quantumEffectActive)
        {
            _Visuals.localPosition = UnityEngine.Random.insideUnitSphere * _QuantumEffectMagnitude;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int otherLayermask = 1 << other.gameObject.layer;
        if ((_CubingerLayer.value & otherLayermask) == otherLayermask)
        {
            _GrabCubingerAudio.Play();
            GameState.Instance.CubingerGrabbed();
            return;
        }

        if ((_PoweUpsLayer.value & otherLayermask) == otherLayermask)
        {
            _GrabPowerUpAudio.Play();
            other.gameObject.GetComponent<IPowerUp>().ActivatePowerUp(this);
        }
    }

    internal void Damage(int damage)
    {
        _HitAudio.Play();

        _health -= damage;

        if (_health <= 0)
        {
            GameState.Instance.GameOver();
        }
    }

    public void ToggleQuantumState(bool state)
    {
        _collider.enabled = !state;

        _quantumEffectActive = state;

        if (!state)
        {
            _Visuals.localPosition = _defaultVisualsLocalPosition;
        }
    }

    public void TriggerLHC(float duration, float maxSpeed, AnimationCurve damping)
    {
        StartCoroutine(LHCAnimation(duration, maxSpeed, damping));
    }

    private IEnumerator LHCAnimation(float duration, float maxSpeed, AnimationCurve damping)
    {
        ToggleQuantumState(true);
        MovePlayer move = GetComponent<MovePlayer>();
        move.enabled = false;

        float alpha = 0f;
        float initialTime = TimeLord.Instance.Now;

        Vector3 center = GameState.Instance.transform.position;

        Vector3 pos = (center - transform.position).normalized;
        float angle = Mathf.Atan2(pos.z, pos.x) + Mathf.PI;
        float radius = (transform.position - center).magnitude;
        while (alpha < 1f)
        {
            angle += TimeLord.Instance.DeltaTime * maxSpeed * damping.Evaluate(alpha);

            transform.position = center + new Vector3(radius * Mathf.Cos(angle),
                                                        0f,
                                                        radius * Mathf.Sin(angle));

            alpha = (TimeLord.Instance.Now - initialTime) / duration;

            yield return new WaitForEndOfFrame();
        }

        ToggleQuantumState(false);
        move.enabled = true;

        OnLHCFinished?.Invoke();
    }
}
