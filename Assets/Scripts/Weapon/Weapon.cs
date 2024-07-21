using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string _id;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private int _poolSize;
    [SerializeField] private float _delay;
    [SerializeField] private float _damage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletLifeTime;

    private float _elapsedTime;
    private List<GameObject> _pool;

    protected bool CanShoot { get; private set; }
    protected Transform ShootPoint => _shootPoint;
    protected IEnumerable<GameObject> Pool => _pool;
    protected float Damage => _damage;
    protected float BulletSpeed => _bulletSpeed;
    protected float BulletLifeTime => _bulletLifeTime;

    public string Id => _id;

    protected virtual void Awake()
    {
        _elapsedTime = 0;
        _pool = new List<GameObject>();
        CreateMissilePool();
    }

    protected virtual void FixedUpdate()
    {
        _elapsedTime -= Time.fixedDeltaTime;

        if (_elapsedTime < 0)
        {
            _elapsedTime = 0;
        }

        CanShoot = _elapsedTime <= 0f;
    }

    public bool Shoot(Vector3 shootDirection)
    {
        if (!CanShoot)
        {
            return false;
        }

        CreateMissile(shootDirection);

        _elapsedTime = _delay;
        return true;
    }
    private void CreateMissilePool()
    {
        var prefab = LoadMissilePrefab();

        for (int i = 0; i < _poolSize; i++)
        {
            var instance = Instantiate(prefab);
            instance.SetActive(false);

            _pool.Add(instance);
        }
    }

    protected abstract void CreateMissile(Vector3 shootDirection);
    protected abstract GameObject LoadMissilePrefab();

    public abstract void Accept(IWeaponVisitor visitor);
}
