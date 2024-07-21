using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class WeaponBonusSpawner : BonusSpawner
{
    [SerializeField] private float _lifeTime;

    [Inject] private WeaponService _weaponService;
    [Inject] private INewWeaponVisitor _visitor;
    [Inject] private IBonusVisitor _bonusVisitor;

    protected override void CreateBonus(Vector3 position)
    {
        _weaponService.CurrentWeapon.Accept(_visitor);
        var prefab = _visitor.Weapons.GetRandom();

        var bonus = new WeaponBonus(prefab);
        bonus.Position = position;
        bonus.LifeTime = _lifeTime;
        bonus.Accept(_bonusVisitor);
    }
}

public interface INewWeaponVisitor : IWeaponVisitor
{
    IEnumerable<Weapon> Weapons { get; }
}

public class NewWeaponVisitor : INewWeaponVisitor
{
    private WeaponContainer _weaponContainer;

    public IEnumerable<Weapon> Weapons { get; private set; }

    private WeaponContainer WeaponContainer
    {
        get
        {
            if (_weaponContainer == null)
            {
                _weaponContainer = Resources.Load(Constants.Prefabs.WeaponContainer) as WeaponContainer;
            }

            return _weaponContainer;
        }
    }

    public void Visit(Pistol pistol)
    {
        Weapons = WeaponContainer.Weapons.Where(element => element.GetType() != pistol.GetType());
    }

    public void Visit(AutoGun autoGun)
    {
        Weapons = WeaponContainer.Weapons.Where(element => element.GetType() != autoGun.GetType());
    }

    public void Visit(Shotgun shotgun)
    {
        Weapons = WeaponContainer.Weapons.Where(element => element.GetType() != shotgun.GetType());
    }

    public void Visit(GranadeLauncher granadeLauncher)
    {
        Weapons = WeaponContainer.Weapons.Where(element => element.GetType() != granadeLauncher.GetType());
    }
}

public static class EnumerableExtension
{
    public static T GetRandom<T>(this IEnumerable<T> collection)
    {
        if (!collection.Any())
        {
            return default(T);
        }

        var random = Random.Range(0, collection.Count());

        return collection.ElementAt(random);
    }
}