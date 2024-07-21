using UnityEngine;

public class BonusWeaponComponent : BonusComponent
{
    [SerializeField] private Weapon _weapon;

    protected override void TakeAction(Collider other)
    {
        if (other.TryGetComponent(out Shoot shoot))
        {
            shoot.Equip(_weapon);
            Destroy(gameObject);
        }
    }
}
