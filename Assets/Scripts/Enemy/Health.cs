using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(Animator))]
public class Health : MonoBehaviour
{
    [SerializeField] private float _health;

    [Inject] private IEnemyVisitor _enemyVisitor;

    private float _currentHealth;
    private Animator _animator;

    private void Start()
    {
        _currentHealth = _health;
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
        {
            return;
        }

        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            if (TryGetComponent(out NavMeshAgent agent))
            {
                agent.isStopped = true;
            }

            if(TryGetComponent(out BaseEnemy enemy))
            {
                enemy.Accept(_enemyVisitor);
            }

            if(TryGetComponent(out Collider collider))
            {
                collider.enabled = false;
            }

            int rand = Random.Range(0, 2);
            _animator?.SetTrigger($"{Constants.Animation.Die}{rand}");
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
