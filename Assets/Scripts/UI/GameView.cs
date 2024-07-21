using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using DG.Tweening;
using TMPro;

public class GameView : MonoBehaviour
{
    [SerializeField] private GameObject _gameoverView;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _text;
 
    [Inject] private EndGame _endGame;
    [Inject] private ScoreStorage _scoreStorage;

    private void OnEnable()
    {
        _endGame.GameEnded += OnGameEnded;
    }

    private void OnDisable()
    {
        _endGame.GameEnded -= OnGameEnded;
    }

    private void OnGameEnded()
    {
        Time.timeScale = 0f;

        _gameoverView.SetActive(true);
        _gameoverView.transform.position = new Vector3(Screen.width / 2, -Screen.height, 0);
        _gameoverView.transform.DOMoveY(Screen.height / 2, 0.5f).SetUpdate(true);
        _canvasGroup.DOFade(0.7f, 0.5f).SetUpdate(true);

        _text.text = _scoreStorage.GetResult();
    }

    public void Exit()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(Constants.SceneIndex.MenuScene);
    }

    public void Replay()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(Constants.SceneIndex.GameScene);
    }
}
