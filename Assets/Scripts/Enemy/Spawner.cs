using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour, IResetable
{
    [SerializeField] private float _baseSpawnDelay;
    [SerializeField] private float _stepDelay;
    [SerializeField] private float _frquesncyStep;
    [SerializeField] private float _minSpawnDelay;

    [Inject] private Player _player;
    [Inject] private IMapService _mapService;
    [Inject] private EnemyFactory _enemyFactory;

    private float _width;
    private float _height;
    private float _currentspawnDelay;
    private float _elapsedTime;
    private float _minRadius;
    private float _maxRadius;
    private Camera _camera;
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

        _enemyList = new List<BaseEnemy>();
    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.fixedDeltaTime;

        if (_elapsedTime >= _currentspawnDelay)
        {
            _elapsedTime = 0;

            CreateEnemy();
        }
    }

    private void CreateEnemy()
    {
        if (_mapService.GetRandomPoint(_camera.transform.position, _minRadius, _maxRadius, out Vector3 position))
        {
            var enemy = _enemyFactory.Create(position);
            if(enemy == null)
            {
                return;
            }

            _enemyList.Add(enemy);
        }
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

    private void ResetPlayerPosition()
    {
        var center = new Vector3(_mapService.MapBounds.Point1.x + _width / 2, _mapService.MapBounds.Point1.y, _mapService.MapBounds.Point1.z + _height / 2);
        _player.transform.position = center;
    }

    public void ResetStatus()
    {
        ResetPlayerPosition();

        foreach(var element in _enemyList)
        {
            Destroy(element.gameObject);
        }
    }
}