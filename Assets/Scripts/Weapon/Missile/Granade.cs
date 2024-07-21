using UnityEngine;
using UnityEngine.InputSystem;

public class Granade : Missile
{
    [SerializeField] private GameObject _explosionEffect;

    private float _explosionRadius;
    private float _explodeError = 1f;
    private Vector3 _mousePoint;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        var missilePos = new Vector2(transform.position.x, transform.position.z);
        var mousePos = new Vector2(_mousePoint.x, _mousePoint.z);
        var distance = Vector3.Distance(missilePos, mousePos);

        if(distance < _explodeError)
        {
            Explode();

            gameObject.SetActive(false);
        }
    }

    private void Explode()
    {
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);

        var colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach(var collider in colliders)
        {
            if(collider.TryGetComponent(out Health health))
            {
                health.TakeDamage(Damage);
            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
    }

    public Granade SetMousePoint()
    {
        var camera = Camera.main;
        var mouse = Mouse.current;

        _mousePoint = camera.ScreenToWorldPoint(mouse.position.value);

        return this;
    }

    public Granade SetExplosionRadius(float radius)
    {
        _explosionRadius = radius;
        return this;
    }
}
