using UnityEngine;
using Zenject;

public class CameraFollow : MonoBehaviour
{
    [Inject] private IMapService _mapService;
    [Inject] private Player _player;

    private float _cameraWidth;
    private float _cameraHeight;

    private void Start()
    {
        var camera = Camera.main;

        _cameraHeight = 2f * camera.orthographicSize;
        _cameraWidth = _cameraHeight * camera.aspect;
    }

    private void FixedUpdate()
    {
        var minX = _mapService.MapBounds.Point1.x + _cameraWidth / 2f;
        var maxX = _mapService.MapBounds.Point2.x - _cameraWidth / 2f;
        var minZ = _mapService.MapBounds.Point1.z + _cameraHeight / 2f;
        var maxZ = _mapService.MapBounds.Point2.z - _cameraHeight / 2f;

        float targetX = _player.transform.position.x;
        float targetZ = _player.transform.position.z;

        if(_player.transform.position.x < minX)
        {
            targetX = minX;
        }
        else if(_player.transform.position.x > maxX)
        {
            targetX = maxX;
        }

        if(_player.transform.position.z < minZ)
        {
            targetZ = minZ;
        }
        else if (_player.transform.position.z > maxZ)
        {
            targetZ = maxZ;
        }

        transform.position = new Vector3(targetX, transform.position.y, targetZ);
    }
}
