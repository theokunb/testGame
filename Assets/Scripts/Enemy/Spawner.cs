using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _baseSpawnDelay;
    [SerializeField] private float _stepDelay;
    [SerializeField] private float _frquesncyStep;
    [SerializeField] private float _minSpawnDelay;

    [Inject] private Player _player;
    [Inject] private IMapService _mapService;
    [Inject] private IEnemyVisitor _enemyVisitor;

    private float _width;
    private float _height;
    private float _currentspawnDelay;
    private float _elapsedTime;
    private float _minRadius;
    private float _maxRadius;
    private Camera _camera;
    private EnemyCOntainer _enemyContainer;
    private List<BaseEnemy> _enemyList;

    private void Start()
    {
        _camera = Camera.main;
        var cameraHeight = 2f * _camera.orthographicSize;
        var cameraWidth = cameraHeight * _camera.aspect;
        var diametr = Mathf.Sqrt(Mathf.Pow(cameraHeight, 2) + Mathf.Pow(cameraWidth, 2));
        _minRadius = diametr / 2f;

        _width = _mapService.MapBounds.Point2.x - _mapService.MapBounds.Point1.x;
        _height = _mapService.MapBounds.Point2.z - _mapService.MapBounds.Point1.z;
        diametr = Mathf.Sqrt(Mathf.Pow(_height, 2) + Mathf.Pow(_width, 2));
        _maxRadius = diametr / 2f;

        _currentspawnDelay = _baseSpawnDelay;
        _elapsedTime = 0;

        ResetPlayerPosition();
        StartCoroutine(ReduceSpawnDelay());

        _enemyContainer = Resources.Load(Constants.Prefabs.EnemyContainer) as EnemyCOntainer;
        _enemyList = new List<BaseEnemy>();
    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.fixedDeltaTime;

        if (_elapsedTime >= _currentspawnDelay)
        {
            _elapsedTime = 0;

            if (RandomPoint(out Vector3 position))
            {
                var prefab = _enemyContainer.GetEnemy();

                if (prefab == null)
                {
                    return;
                }

                var instance = Instantiate(prefab, position, Quaternion.identity);
                instance.SetPlayer(_player);

                if (instance.TryGetComponent(out PlayrKiller killer))
                {
                    killer.SetPlayer(_player);
                }
                if (instance.TryGetComponent(out Health health))
                {
                    health.SetEnemyVisitor(_enemyVisitor);
                }

                _enemyList.Add(instance);
            }
        }
    }

    private bool RandomPoint(out Vector3 point)
    {
        int tries = 10;

        while (tries > 0)
        {
            tries--;
            int randAngle = Random.Range(0, 360);
            float randRadius = Random.Range(_minRadius, _maxRadius);

            var xPos = _camera.transform.position.x + randRadius * Mathf.Cos(randAngle * Mathf.Deg2Rad);
            var zPos = _camera.transform.position.z + randRadius * Mathf.Sin(randAngle * Mathf.Deg2Rad);

            point = new Vector3(xPos, 0, zPos);

            if (_mapService.IsInner(point))
            {
                return true;
            }
        }

        point = Vector3.zero;
        return false;
    }

    private IEnumerator ReduceSpawnDelay()
    {
        var elapsedTime = 0f;

        while (_currentspawnDelay != _minSpawnDelay)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= _stepDelay)
            {
                _currentspawnDelay -= _frquesncyStep;
                elapsedTime = 0f;
            }

            yield return null;
        }
    }

    public void ResetPlayerPosition()
    {
        var center = new Vector3(_mapService.MapBounds.Point1.x + _width / 2, _mapService.MapBounds.Point1.y, _mapService.MapBounds.Point1.z + _height / 2);
        _player.transform.position = center;
    }
}