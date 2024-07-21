using TMPro;
using UnityEngine;
using Zenject;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    [Inject] private IEnemyVisitor _visitor;

    private PlayerScore _playerScore;

    private void Awake()
    {
        _playerScore = _visitor as PlayerScore;
        _text.text = _playerScore.CurrentScore.ToString();
    }

    private void OnEnable()
    {
        _playerScore.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _playerScore.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged()
    {
        _text.text = _playerScore.CurrentScore.ToString();
    }
}
