using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "WeaponData")]
public class WeaponData : ScriptableObject
{
    public int _maxAmmo;
    public string _weaponName;
    public float _shootRate;
    public bool _isAutomatic;
}
