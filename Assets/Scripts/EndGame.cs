using System;

public class EndGame
{
    public event Action GameEnded;

    public void PlayerDied()
    {
        GameEnded?.Invoke();
    }
}
