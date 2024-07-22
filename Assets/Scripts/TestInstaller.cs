using UnityEngine;
using Zenject;

public class TestInstaller : MonoInstaller
{
    [SerializeField] private Player _player;
    [SerializeField] private MainHand _mainHand;
    [SerializeField] private WeaponHold _weaponHold;
    [SerializeField] private TerrainMap _terrainMap;
    [SerializeField] private CameraMap _cameraMap;
    [SerializeField] private SoundContainer _soundContainer;
    [SerializeField] private WeaponSound _weaponSound;

    public override void InstallBindings()
    {
        Container.Bind<MainHand>().FromInstance(_mainHand);
        Container.Bind<IWeaponVisitor>().FromInstance(_weaponHold);
        Container.Bind<WeaponService>().AsSingle();
        Container.Bind<IMapService>().FromInstance(_terrainMap);
        Container.Bind<Player>().FromInstance(_player);
        Container.Bind<Spawner>().AsSingle();
        Container.Bind<IEnemyVisitor>().FromInstance(new PlayerScore()).AsSingle();
        Container.Bind<EnemyFactory>().AsSingle();
        Container.Bind<CameraMap>().FromInstance(_cameraMap);
        Container.Bind<INewWeaponVisitor>().FromInstance(new NewWeaponVisitor()).AsSingle();
        Container.Bind<IBonusVisitor>().FromInstance(new BonusFactory()).AsSingle();
        Container.Bind<EndGame>().AsSingle();
        Container.Bind<ScoreStorage>().AsSingle();
        Container.Bind<SoundContainer>().FromInstance(_soundContainer);
        Container.Bind<WeaponSound>().FromInstance(_weaponSound);
        Container.Bind<TerrorZoneService>().AsSingle();
    }
}
