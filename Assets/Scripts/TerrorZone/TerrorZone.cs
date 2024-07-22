using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TerrorZone : MonoBehaviour
{
    [SerializeField] private int _slowZonesCount;
    [SerializeField] private int _slowZoneSize;
    [SerializeField] private GameObject _slowZonePrefab;
    [SerializeField] private int _deathZonesCount;
    [SerializeField] private int _deathZoneSize;
    [SerializeField] private GameObject _deathZonePrefab;
    [SerializeField] private int _offSet;

    [Inject] private TerrorZoneService _terrorZoneService;
    [Inject] private IMapService _mapService;

    private void Start()
    {
        _terrorZoneService.Make();

        CreateZone(_slowZonesCount, _slowZoneSize, _slowZonePrefab);
        CreateZone(_deathZonesCount, _deathZoneSize, _deathZonePrefab);
    }

    private void CreateZone(int count, int size, GameObject prefab)
    {
        List<Vector2?> points = new List<Vector2?>();

        for (int i = 0; i < count; i++)
        {
            points.Add(_terrorZoneService.Put(size, _offSet));
        }

        foreach (var point in points)
        {
            if(point == null) 
                continue;

            var zone = Instantiate(prefab, new Vector3(point.Value.x, 0, point.Value.y), Quaternion.identity);
            zone.transform.localScale = Vector3.one * size;
            zone.transform.position += _mapService.MapBounds.Point1;
        }
    }
}
