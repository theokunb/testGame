using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WeaponService
{
    private readonly MainHand _mainHand;
    private readonly IWeaponVisitor _weaponVisitor;

    private Weapon _currentWeapon;
    private Dictionary<string, Weapon> _weapons;
    private WeaponSound _weaponSound;

    public Weapon CurrentWeapon => _currentWeapon;

    [Inject]
    public WeaponService(MainHand mainHand, IWeaponVisitor weaponVisitor, WeaponSound weaponSound)
    {
        _weapons = new Dictionary<string, Weapon>();
        _mainHand = mainHand;
        _weaponVisitor = weaponVisitor;
        _weaponSound = weaponSound;
    }

    public void Shoot(Vector3 shootDirection)
    {
        if (_currentWeapon.Shoot(shootDirection))
        {
            _currentWeapon?.Accept(_weaponSound);
        }
    }

    public void Equip(Weapon weapon)
    {
        if (_weapons.ContainsKey(weapon.Id))
        {
            AcvivateWeapon(weapon.Id);
            _currentWeapon = _weapons[weapon.Id];
        }
        else
        {
            _currentWeapon = Object.Instantiate(weapon, _mainHand.transform);
            _weapons.Add(_currentWeapon.Id, _currentWeapon);
            AcvivateWeapon(_currentWeapon.Id);
        }

        _currentWeapon.Accept(_weaponVisitor);
    }

    private void AcvivateWeapon(string weaponId)
    {
        foreach (var element in _weapons.Keys)
        {
            _weapons[element].gameObject.SetActive(element == weaponId);
        }
    }
}