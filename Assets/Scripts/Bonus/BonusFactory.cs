using System.Collections.Generic;
using UnityEngine;

public class BonusFactory : IBonusVisitor
{
    private readonly IWeaponCreatorVisitor _weaponCreator;
    private readonly IBuffCreatorVisitor _buffCreator;

    public BonusFactory()
    {
        _weaponCreator = new BonusWeaponCreator();
        _buffCreator = new BuffCreatorVisitor();
    }

    public void Visit(WeaponBonus weaponBonus)
    {
        _weaponCreator.Position = weaponBonus.Position;
        _weaponCreator.LifeTime = weaponBonus.LifeTime;
        weaponBonus.Weapon.Accept(_weaponCreator);
    }

    public void Visit(BuffBonus buffBonus)
    {
        _buffCreator.Position = buffBonus.Position;
        _buffCreator.Duration = buffBonus.Duration;
        _buffCreator.LifeTime = buffBonus.LifeTime;

        int rand = Random.Range(0, 2);
        Buff buff;

        if(rand == 0)
        {
            buff = new HasteBuff();
        }
        else
        {
            buff = new InvincibleBuff();
        }

        buff.Accept(_buffCreator);
    }
}

public interface IBonusVisitor
{
    void Visit(WeaponBonus weaponBonus);
    void Visit(BuffBonus buffBonus);
}

public interface IBonusCreatorVisitor
{
    Vector3 Position { get; set; }
    float LifeTime { get; set; }
}

public class BuffCreatorVisitor : IBuffCreatorVisitor
{
    private List<GameObject> _bonuses = new List<GameObject>();

    public float Duration { get ; set ; }
    public Vector3 Position { get ; set ; }
    public float LifeTime { get; set; }

    private void Create(GameObject prefab)
    {
        var instance = Object.Instantiate(prefab, Position, Quaternion.identity);
        _bonuses.Add(instance);

        if (instance.TryGetComponent(out BuffComponent component))
        {
            component.LifeTime = LifeTime;
            component.Duration = Duration;
        }
    }

    public void Visit(HasteBuff buff)
    {
        var prefab = Resources.Load(Constants.Prefabs.Bonus.Haste) as GameObject;

        Create(prefab);
    }

    public void Visit(InvincibleBuff buff)
    {
        var prefab = Resources.Load(Constants.Prefabs.Bonus.Invincible) as GameObject;

        Create(prefab);
    }
}

public interface IBuffCreatorVisitor : IBonusCreatorVisitor
{
    public float Duration { get; set; } 

    void Visit(HasteBuff buff);
    void Visit(InvincibleBuff buff);
}

public interface IWeaponCreatorVisitor : IWeaponVisitor, IBonusCreatorVisitor
{
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

    public void Visit(GranadeLauncher granadeLauncher)
    {
        var prefab = Resources.Load(Constants.Prefabs.Bonus.GranadeLauncher) as GameObject;

        Create(prefab);
    }
}