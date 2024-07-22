using UnityEngine;

public class SlowZone : MonoBehaviour
{
    [SerializeField] private float _slowFactor;

    public float SlowFactor => _slowFactor;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Movement movement))
        {
            movement.TakeSlow(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Movement movement))
        {
            movement.RemoveSlow(this);
        }
    }
}
