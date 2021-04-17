using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private LayerMask _CubingerLayer;

    [SerializeField]
    private LayerMask _PoweUpsLayer;

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
}
