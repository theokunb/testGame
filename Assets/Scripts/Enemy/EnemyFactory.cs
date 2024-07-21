using UnityEngine;
using Zenject;

public class EnemyFactory
{
    [Inject] private IEnemyVisitor _enemyVisitor;
    [Inject] private Player _player;

    private EnemyContainer _enemyContainer;

    private EnemyContainer EnemyContainer
    {
        get
        {
            if(_enemyContainer == null)
            {
                _enemyContainer = Resources.Load(Constants.Prefabs.EnemyContainer) as EnemyContainer;
            }

            return _enemyContainer;
        }
    }

    public BaseEnemy Create(Vector3 position)
    {
        var prefab = EnemyContainer.GetRandomEnemy();

        if(prefab == null)
        {
            return null;
        }

        var instance = Object.Instantiate(prefab, position, Quaternion.identity);
        instance.SetPlayer(_player);

        if (instance.TryGetComponent(out PlayerKiller killer))
        {
            killer.SetPlayer(_player);
        }

        if (instance.TryGetComponent(out Health health))
        {
            health.SetEnemyVisitor(_enemyVisitor);
        }

        return instance;
    }
}
