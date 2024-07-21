using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new weapon container", menuName = "SO/Create weapon container", order = 51)]
public class WeaponContainer : ScriptableObject
{
    [SerializeField] private List<Weapon> _weapons;

    public IEnumerable<Weapon> Weapons => _weapons;
}
