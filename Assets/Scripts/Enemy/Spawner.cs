using System.Collections;
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

    private float _width;
    private float _height;
    private float _currentspawnDelay;
    private float _elapsedTime;

    private void Start()
    {
        _width = _mapService.MapBounds.Point2.x - _mapService.MapBounds.Point1.x;
        _height = _mapService.MapBounds.Point2.z - _mapService.MapBounds.Point1.z;
        _currentspawnDelay = _baseSpawnDelay;
        _elapsedTime = 0;

        ResetPlayerPosition();
        StartCoroutine(ReduceSpawnDelay());
    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.fixedDeltaTime;

        if(_elapsedTime >= _currentspawnDelay)
        {
            _elapsedTime = 0;

            Debug.Log("spawn");
        }
    }

    private IEnumerator ReduceSpawnDelay()
    {
        var elapsedTime = 0f;

        while(_currentspawnDelay != _minSpawnDelay)
        {
            elapsedTime += Time.deltaTime;

            if(elapsedTime >= _stepDelay)
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
