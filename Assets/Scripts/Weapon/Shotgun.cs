using System.Linq;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private int _bunchSize;
    [SerializeField] private float _spread;

    protected override void CreateMissile(Vector3 shootDirection)
    {
        var missiles = Pool.Where(element => element.activeSelf == false).Take(_bunchSize);

        if (missiles.Count() == 0)
        {
            return;
        }

        foreach(var missile in missiles)
        {
            var randSpread = Random.Range(-_spread, _spread);
            
            shootDirection = Quaternion.AngleAxis(randSpread, missile.transform.up) * shootDirection;
            missile.transform.position = ShootPoint.position;
            missile.transform.rotation = Quaternion.LookRotation(shootDirection);

            if (missile.TryGetComponent(out Missile component))
            {
                component.SetupDamage(Damage)
                    .SetupLifeTime(BulletLifeTime)
                    .SetupSpeed(BulletSpeed)
                    .SetupDirection(shootDirection);
            }

            missile.SetActive(true);
        }
    }

    protected override GameObject LoadMissilePrefab() => 
        Resources.Load(Constants.Prefabs.Missile.Bullet) as GameObject;

    public override void Accept(IWeaponVisitor visitor) => 
        visitor.Visit(this);
}