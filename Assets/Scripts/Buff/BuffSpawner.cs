using UnityEngine;
using Zenject;

public class BuffSpawner : BonusSpawner
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _buffDuration;

    [Inject] private IBonusVisitor _bonusVisitor;

    protected override void CreateBonus(Vector3 position)
    {
        var bonus = new BuffBonus();
        bonus.Position = position;
        bonus.LifeTime = _lifeTime;
        bonus.Duration = _buffDuration;

        bonus.Accept(_bonusVisitor);
    }
}
