using System;

public class PlayerScore : IEnemyVisitor
{
    public PlayerScore()
    {
        CurrentScore = 0;
    }

    public int CurrentScore { get; private set; }

    public event Action ScoreChanged;

    public void Visit(EnemySoldier enemySoldier)
    {
        CurrentScore += 7;
        ScoreChanged?.Invoke();
    }

    public void Visit(EnemyNimble enemyNimble)
    {
        CurrentScore += 12;
        ScoreChanged?.Invoke();
    }

    public void Visit(EnemyProtected enemyProtected)
    {
        CurrentScore += 30;
        ScoreChanged?.Invoke();
    }
}