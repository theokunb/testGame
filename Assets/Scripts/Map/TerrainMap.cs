using UnityEngine;

public class TerrainMap : MonoBehaviour, IMapService
{
    [SerializeField] private Terrain _terrain;

    public Map MapBounds { get; private set;}

    public bool IsInner(Vector3 point)
    {
        var xPos = point.x > MapBounds.Point1.x && point.x < MapBounds.Point2.x;
        var zPos = point.z > MapBounds.Point1.z && point.z < MapBounds.Point2.z;

        return xPos && zPos;
    }

    private void Awake()
    {
        var point1 = _terrain.transform.position;
        var point2 = new Vector3(_terrain.transform.position.x + _terrain.terrainData.size.x, _terrain.transform.position.y, _terrain.transform.position.z + _terrain.terrainData.size.z);
        
        MapBounds = new Map(point1, point2);
    }
}

public interface IMapService
{
    Map MapBounds { get; }

    bool IsInner(Vector3 point);
}

public struct Map
{
    //   *     point2
    //point1     *
    public Map(Vector3 point1, Vector3 point2)
    {
        Point1 = point1;
        Point2 = point2;
    }

    public Vector3 Point1 { get; private set; }
    public Vector3 Point2 { get; private set; }
}
