using UnityEngine;

public class TerrainMap : MonoBehaviour, IMapService
{
    [SerializeField] private Terrain _terrain;

    public Map MapBounds { get; private set;}

    private void Awake()
    {
        var point1 = _terrain.transform.position;
        var point2 = new Vector3(_terrain.transform.position.x + _terrain.terrainData.size.x, _terrain.transform.position.y, _terrain.transform.position.z);
        var point3 = new Vector3(_terrain.transform.position.x + _terrain.terrainData.size.x, _terrain.transform.position.y, _terrain.transform.position.z + _terrain.terrainData.size.z);
        var point4 = new Vector3(_terrain.transform.position.x, _terrain.transform.position.y, _terrain.transform.position.z + _terrain.terrainData.size.z);

        MapBounds = new Map(point1, point2, point3, point4);
    }
}

public interface IMapService
{
    Map MapBounds { get; }
}

public struct Map
{
    //point4  point3
    //point1  point2
    public Map(Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4)
    {
        Point1 = point1;
        Point2 = point2;
        Point3 = point3;
        Point4 = point4;
    }

    public Vector3 Point1 { get; private set; }
    public Vector3 Point2 { get; private set; }
    public Vector3 Point3 { get; private set; }
    public Vector3 Point4 { get; private set; }
}
