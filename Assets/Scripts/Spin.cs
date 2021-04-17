using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField]
    private float _SpinSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * _SpinSpeed * TimeLord.Instance.DeltaTime);
    }
}
