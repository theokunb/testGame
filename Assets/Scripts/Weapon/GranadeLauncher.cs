using System.Linq;
using UnityEngine;

public class GranadeLauncher : Weapon
{
    [SerializeField] private float _explosionRadius;

    protected override void CreateMissile(Vector3 shootDirection)
    {
        var missile = Pool.FirstOrDefault(element => element.activeSelf == false);

        if (missile == null)
        {
            return;
        }

        missile.transform.position = ShootPoint.position;
        missile.transform.rotation = Quaternion.LookRotation(shootDirection);

        if (missile.TryGetComponent(out Granade component))
        {
            component.SetMousePoint()
                .SetExplosionRadius(_explosionRadius)
                .SetupDamage(Damage)
                .SetupLifeTime(BulletLifeTime)
                .SetupSpeed(BulletSpeed)
                .SetupDirection(shootDirection);
        }

        missile.SetActive(true);
    }

    protected override GameObject LoadMissilePrefab() =>
        Resources.Load(Constants.Prefabs.Missile.Granade) as GameObject;
    public override void Accept(IWeaponVisitor visitor) =>
        visitor.Visit(this);
}
