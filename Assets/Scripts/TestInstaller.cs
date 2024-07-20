using UnityEngine;
using Zenject;

public class TestInstaller : MonoInstaller
{
    [SerializeField] private MainHand _mainHand;
    [SerializeField] private WeaponHold _weaponHold;

    public override void InstallBindings()
    {
        Container.Bind<MainHand>().FromInstance(_mainHand);
        Container.Bind<IWeaponVisitor>().FromInstance(_weaponHold);
        Container.Bind<WeaponService>().AsSingle();
    }
}
