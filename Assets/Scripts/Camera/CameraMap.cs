using UnityEngine;

[RequireComponent (typeof(Camera))]
public class CameraMap : MonoBehaviour, IMapService
{
    private Camera _camera;
    private float _cameraHeight;
    private float _cameraWidth;

    public Map MapBounds { get; private set; }
    public float Radius => _cameraHeight;

    private void Awake()
    {
        _camera = GetComponent<Camera>();

        _cameraHeight = 2f * _camera.orthographicSize;
        _cameraWidth = _cameraHeight * _camera.aspect;
    }

    private void FixedUpdate()
    {
        var point1 = new Vector3(transform.position.x - _cameraWidth / 2, transform.position.y, transform.position.z - _cameraHeight / 2);
        var point2 = new Vector3(transform.position.x + _cameraWidth / 2, transform.position.y, transform.position.z + _cameraHeight / 2);

        MapBounds = new Map(point1, point2);
    }
}
