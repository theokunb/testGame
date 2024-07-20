public class EnemySoldier : BaseEnemy
{
    public override void Accept(IEnemyVisitor visitor)
    {
        visitor.Visit(this);
    }
}