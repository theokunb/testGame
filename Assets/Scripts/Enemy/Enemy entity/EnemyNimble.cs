public class EnemyNimble : BaseEnemy
{
    public override void Accept(IEnemyVisitor visitor)
    {
        visitor.Visit(this);
    }
}
