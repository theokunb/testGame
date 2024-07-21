using UnityEngine;
using Zenject;

public abstract class BonusSpawner : MonoBehaviour
{
    [SerializeField] private float _delay;

    [Inject] private CameraMap _map;

    private float _elapsedTime;
    private IMapService _mapService => _map;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _elapsedTime = 0;
    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _delay)
        {
            if (_mapService.GetRandomPoint(_camera.transform.position, 0, _map.Radius, out Vector3 posiiton))
            {
                CreateBonus(posiiton);

                _elapsedTime = 0f;
            }
        }
    }

    protected abstract void CreateBonus(Vector3 position);
}