using PowerUps;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private LayerMask _CubingerLayer;

    [SerializeField]
    private LayerMask _PoweUpsLayer;

    private Collider _collider = null;

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
}
