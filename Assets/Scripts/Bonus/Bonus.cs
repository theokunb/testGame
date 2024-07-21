using UnityEngine;

public abstract class Bonus
{
    public Vector3 Position { get; set; }
    public float LifeTime { get; set; }

    public abstract void Accept(IBonusVisitor visitor);
}

public class WeaponBonus : Bonus
{
    public Weapon Weapon { get; private set; }

    public WeaponBonus(Weapon weapon)
    {
        Weapon = weapon;
    }

    public override void Accept(IBonusVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class BuffBonus : Bonus
{
    public float Duration { get; set; }

    public override void Accept(IBonusVisitor visitor)
    {
        visitor.Visit(this);
    }
}