using UnityEngine;
using Zenject;

public class ScoreStorage
{
    private readonly PlayerScore _playerScore;

    [Inject]
    public ScoreStorage(IEnemyVisitor enemyVisitor)
    {
        _playerScore = enemyVisitor as PlayerScore;
    }

    public string GetResult()
    {
        var storedScore = PlayerPrefs.GetInt(Constants.Prefs.Highscore, 0);

        if(storedScore < _playerScore.CurrentScore)
        {
            PlayerPrefs.SetInt(Constants.Prefs.Highscore, _playerScore.CurrentScore);
            return $"поздравляем!!!\nновый рекорд {_playerScore.CurrentScore}";
        }
        else
        {
            return $"вы набрали {_playerScore.CurrentScore}\nтекущий рекорд {storedScore}";
        }
    }
}
