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

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        int otherLayermask = 1 << other.gameObject.layer;
        if ((_CubingerLayer.value & otherLayermask) == otherLayermask)
        {
            GameState.Instance.CubingerGrabbed();
            return;
        }

        if ((_PoweUpsLayer.value & otherLayermask) == otherLayermask)
        {
            other.gameObject.GetComponent<IPowerUp>().ActivatePowerUp(this);
        }
    }

    internal void Damage(int damage)
    {
        Debug.LogError("Ouch, that hurt");
    }

    public void ToggleQuantumState(bool state)
    {
        _collider.enabled = !state;
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

        float angle = Vector3.Angle(GameState.Instance.transform.right, center - transform.position) * Mathf.Deg2Rad;
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
