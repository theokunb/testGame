using UnityEngine;
using Zenject;

public class TestInstaller : MonoInstaller
{
    [SerializeField] private Player _player;
    [SerializeField] private MainHand _mainHand;
    [SerializeField] private WeaponHold _weaponHold;
    [SerializeField] private TerrainMap _terrainMap;

    public override void InstallBindings()
    {
        Container.Bind<MainHand>().FromInstance(_mainHand);
        Container.Bind<IWeaponVisitor>().FromInstance(_weaponHold);
        Container.Bind<WeaponService>().AsSingle();
        Container.Bind<IMapService>().FromInstance(_terrainMap);
        Container.Bind<Player>().FromInstance(_player);
        Container.Bind<Spawner>().AsSingle();
    }
}
