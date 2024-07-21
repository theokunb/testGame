using UnityEngine;

public class Missile : MonoBehaviour
{
    protected float LifeTime;
    protected float Speed;
    protected float Damage;
    protected Vector3 Direction;
    protected Rigidbody RigidBody;
    protected float ElapsedTime;

    protected virtual void Awake()
    {
        RigidBody = GetComponent<Rigidbody>();
        ElapsedTime = 0;
    }

    protected virtual void OnEnable()
    {
        RigidBody.velocity = Direction.normalized * Speed;
    }

    protected virtual void FixedUpdate()
    {
        ElapsedTime += Time.fixedDeltaTime;

        if (ElapsedTime >= LifeTime)
        {
            RigidBody.velocity = Vector3.zero;
            ElapsedTime = 0;
            gameObject.SetActive(false);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Health health))
        {
            health.TakeDamage(Damage);

            gameObject.SetActive(false);
        }
    }

    public Missile SetupSpeed(float speed)
    {
        Speed = speed;
        return this;
    }

    public Missile SetupLifeTime(float lifeTime)
    {
        LifeTime = lifeTime;
        return this;
    }

    public Missile SetupDamage(float damage)
    {
        Damage = damage;
        return this;
    }

    public Missile SetupDirection(Vector3 direction)
    {
        Direction = direction;
        return this;
    }
}