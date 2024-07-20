public class PlayerScore : IEnemyVisitor
{
    public PlayerScore()
    {
        CurrentScore = 0;
    }

    public int CurrentScore { get; private set; }

    public void Reset()
    {
        CurrentScore = 0;
    }

    public void Visit(EnemySoldier enemySoldier)
    {
        CurrentScore += 7;
    }

    public void Visit(EnemyNimble enemyNimble)
    {
        CurrentScore += 12;
    }

    public void Visit(EnemyProtected enemyProtected)
    {
        CurrentScore += 30;
    }
}