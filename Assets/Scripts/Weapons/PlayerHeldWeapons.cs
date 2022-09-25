using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeldWeapons : MonoBehaviour
{
    private List<PickupWeapon> _heldWeapons;
    private PickupWeapon _selectedWeapon;
    private bool _holdingFire;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _fireRate;

    [SerializeField] private PickupWeapon _defaultWeapon;

    private void Awake()
    {
        _selectedWeapon = _defaultWeapon;
    }
    private void Update()
    {
        //Debug.Log(transform.name + " holding fire: " + _holdingFire);
        //Debug.Log(_holdingFire);
        if(_selectedWeapon.WeaponIsAutomatic() && _holdingFire)
        {
            //_selectedWeapon.IsHoldingFire(_holdingFire);
            //_selectedWeapon.Shoot();
            Debug.Log(transform.name + " is shooting");
        }
    }
    public void HoldingFire(bool holdingFire)
    {
        _holdingFire = holdingFire;
    }
    public void Shoot()
    {
        _selectedWeapon.Shoot();
    }
}
