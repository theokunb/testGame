using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public abstract class BaseEnemy : MonoBehaviour
{
    [Inject] private Player _player;

    private NavMeshAgent _agent;
    private Animator _animator;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_player == null)
            return;

        _agent.SetDestination(_player.transform.position);
        _animator?.SetFloat(Constants.Animation.Speed, _agent.velocity.magnitude / _agent.speed);
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    public abstract void Accept(IEnemyVisitor visitor);
}

public interface IEnemyVisitor
{
    void Visit(EnemySoldier enemySoldier);
    void Visit(EnemyNimble enemyNimble);
    void Visit(EnemyProtected enemyProtected);
}
