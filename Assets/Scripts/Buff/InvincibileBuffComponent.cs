using UnityEngine;

public class InvincibileBuffComponent : BuffComponent
{
    protected override void TakeAction(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            player.TakeBuff(this);
            Destroy(gameObject);
        }
    }
}
