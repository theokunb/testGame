using UnityEngine;

public class TerrainMap : MonoBehaviour, IMapService
{
    [SerializeField] private Terrain _terrain;

    public Map MapBounds { get; private set;}

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

    bool GetRandomPoint(Vector3 startPoint, float minRadius, float maxRadius, out Vector3 point)
    {
        int tries = 10;

        while (tries > 0)
        {
            tries--;
            int randAngle = Random.Range(0, 360);
            float randRadius = Random.Range(minRadius, maxRadius);

            var xPos = startPoint.x + randRadius * Mathf.Cos(randAngle * Mathf.Deg2Rad);
            var zPos = startPoint.z + randRadius * Mathf.Sin(randAngle * Mathf.Deg2Rad);

            point = new Vector3(xPos, 0, zPos);

            if (MapBounds.IsInner(point))
            {
                return true;
            }
        }

        point = Vector3.zero;
        return false;
    }
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

    public bool IsInner(Vector3 point)
    {
        var xPos = point.x > Point1.x && point.x < Point2.x;
        var zPos = point.z > Point1.z && point.z < Point2.z;

        return xPos && zPos;
    }
}
