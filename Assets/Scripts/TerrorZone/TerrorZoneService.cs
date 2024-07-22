using UnityEngine;
using Zenject;

public class TerrorZoneService
{
    private readonly IMapService _mapService;
    private int[,] _map;
    private int _width;
    private int _height;

    [Inject]
    public TerrorZoneService(IMapService mapService)
    {
        _mapService = mapService;
    }

    public void Make()
    {
        _width = (int)(_mapService.MapBounds.Point2.x - _mapService.MapBounds.Point1.x);
        _height = (int)(_mapService.MapBounds.Point2.z - _mapService.MapBounds.Point1.z);

        _map = new int[_height, _width];

        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                _map[i, j] = 0;
            }
        }
    }

    public Vector2? Put(int size, int offset)
    {
        Vector2? res = null;
        bool canPut;
        int attempts = 20;

        do
        {
            int randX = Random.Range(offset, _width - offset);
            int randY = Random.Range(offset, _height - offset);
            bool checkStatus = true;
            attempts--;

            for (int i = randY - size - offset; i < randY + size + offset; i++)
            {
                if (i < 0 || i >= _map.GetLength(0))
                {
                    checkStatus = false;
                    break;
                }

                for (int j = randX - size - offset; j < randX + size + offset; j++)
                {
                    if (j < 0 || j >= _map.GetLength(1))
                    {
                        checkStatus = false;
                        break;
                    }

                    if (_map[i, j] != 0)
                    {
                        checkStatus = false;
                        break;
                    }
                }
            }

            canPut = checkStatus;

            if (canPut)
            {
                res = new Vector2(randX, randY);

                for (int i = randY - size; i < randY + size; i++)
                {
                    for (int j = randX - size; j < randX + size; j++)
                    {
                        _map[i, j] = 1;
                    }
                }
            }
        } while (canPut == false && attempts > 0);

        return res;
    }
}
