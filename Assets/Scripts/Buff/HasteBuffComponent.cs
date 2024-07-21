using UnityEngine;

public class HasteBuffComponent : BuffComponent
{
    [SerializeField] private float _hasteBonus;

    public float HasteBonus => _hasteBonus;

    protected override void TakeAction(Collider other)
    {
        if (other.TryGetComponent(out Movement movement))
        {
            movement.TakeBuff(this);
            Destroy(gameObject);
        }
    }
}