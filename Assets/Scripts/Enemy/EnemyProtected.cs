public class EnemyProtected : BaseEnemy
{
    public override void Accept(IEnemyVisitor visitor)
    {
        visitor.Visit(this);
    }
}
