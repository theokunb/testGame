using UnityEngine;
using Zenject;

public class PlayrKiller : MonoBehaviour
{
    [SerializeField] private float _killDistance;

    [Inject] private Player _player;

    private void FixedUpdate()
    {
        var distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance < _killDistance)
        {
            _player.Die();
        }
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }
}
