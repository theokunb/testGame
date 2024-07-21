public abstract class Buff
{
    public abstract void Accept(IBuffCreatorVisitor visitor);
}

public class HasteBuff : Buff
{
    public override void Accept(IBuffCreatorVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class InvincibleBuff : Buff
{
    public override void Accept(IBuffCreatorVisitor visitor)
    {
        visitor.Visit(this);
    }
}
