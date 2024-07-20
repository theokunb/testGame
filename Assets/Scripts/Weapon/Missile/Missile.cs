using UnityEngine;

public class Missile : MonoBehaviour
{
    private float _lifeTime;
    private float _speed;
    private float _damage;
    private Vector3 _direction;
    private Rigidbody _rigidBody;
    private float _elapsedTime;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _elapsedTime = 0;
    }

    private void OnEnable()
    {
        _rigidBody.velocity = _direction * _speed;
    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.fixedDeltaTime;

        if (_elapsedTime >= _lifeTime)
        {
            _rigidBody.velocity = Vector3.zero;
            _elapsedTime = 0;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);

            gameObject.SetActive(false);
        }
    }

    public Missile SetupSpeed(float speed)
    {
        _speed = speed;
        return this;
    }

    public Missile SetupLifeTime(float lifeTime)
    {
        _lifeTime = lifeTime;
        return this;
    }

    public Missile SetupDamage(float damage)
    {
        _damage = damage;
        return this;
    }

    public Missile SetupDirection(Vector3 direction)
    {
        _direction = direction;
        return this;
    }
}