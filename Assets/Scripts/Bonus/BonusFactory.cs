using System.Collections.Generic;
using UnityEngine;

public class BonusFactory : IBonusVisitor, IResetable
{
    private readonly IWeaponCreatorVisitor _weaponCreator;

    public BonusFactory()
    {
        _weaponCreator = new BonusWeaponCreator();
    }

    public void ResetStatus()
    {
        _weaponCreator.Reset();
    }

    public void Visit(WeaponBonus weaponBonus)
    {
        _weaponCreator.Position = weaponBonus.Position;
        _weaponCreator.LifeTime = weaponBonus.LifeTime;
        weaponBonus.Weapon.Accept(_weaponCreator);
    }

    public void Visit(BuffBonus buffBonus)
    {

    }
}

public interface IBonusVisitor
{
    void Visit(WeaponBonus weaponBonus);
    void Visit(BuffBonus buffBonus);
}

public interface IWeaponCreatorVisitor : IWeaponVisitor
{
    Vector3 Position { get; set; }
    float LifeTime { get; set; }

    void Reset();
}

public class BonusWeaponCreator : IWeaponCreatorVisitor
{
    private List<GameObject> _bonuses = new List<GameObject>();

    public Vector3 Position { get; set; }
    public float LifeTime { get ; set ; }

    private void Create(GameObject prefab)
    {
        var instance = Object.Instantiate(prefab, Position, Quaternion.identity);
        _bonuses.Add(instance);

        if(instance.TryGetComponent(out BonusComponent component))
        {
            component.LifeTime = LifeTime;
        }
    }

    public void Visit(Pistol pistol)
    {
        var prefab = Resources.Load(Constants.Prefabs.Bonus.Pistol) as GameObject;

        Create(prefab);
    }
    public void Visit(AutoGun autoGun)
    {
        var prefab = Resources.Load(Constants.Prefabs.Bonus.AutoGun) as GameObject;

        Create(prefab);
    }
    public void Visit(Shotgun shotgun)
    {
        var prefab = Resources.Load(Constants.Prefabs.Bonus.Shotgun) as GameObject;

        Create(prefab);
    }

    public void Reset()
    {
        foreach (var element in _bonuses)
        {
            Object.Destroy(element.gameObject);
        }
    }
}