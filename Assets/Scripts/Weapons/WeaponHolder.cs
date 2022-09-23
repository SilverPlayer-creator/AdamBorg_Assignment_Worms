using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    private List<Weapon> _heldWeapons;
    [SerializeField] private Weapon _defaultWeapon;
    [SerializeField] private int _index;
    private Weapon _selectedWeapon;
    private int _weaponIndex = 0;
    private bool _holdingFire;
    [SerializeField] private Transform _barrel;
    public int Index { get { return _index; } private set { } }
    void Awake()
    {
        
    }
    private void Start()
    {
        _heldWeapons = new List<Weapon>();
        _heldWeapons.Add(_defaultWeapon);
        foreach (Weapon weapon in _heldWeapons)
        {
            if (weapon != null)
            {
                weapon.Initialize(_index, _barrel);
                //Debug.Log("Initialize");
            }
        }
        _selectedWeapon = _heldWeapons[0];
    }
    private void Update()
    {
        if (_selectedWeapon.IsAutomatic)
        {
            if (_holdingFire)
            {
                if (Time.time >= _selectedWeapon.NextShootTime)
                {
                    Debug.Log("Shoot");
                    _selectedWeapon.Shoot(_barrel);
                    _selectedWeapon.NextShootTime = Time.time + 1 / _selectedWeapon.ShootRate;
                }
            }
        }
    }
    public void FireHeldWeapon(bool _inputPressed)
    {
        if (_selectedWeapon.IsAutomatic)
        {
            //_selectedWeapon.HoldingFire(_inputPressed);
            _holdingFire = _inputPressed;
        }
        else
        {
            _selectedWeapon.Shoot(_barrel);
        }
    }
    public void ReloadHeldWeapon(int index)
    {
        _selectedWeapon.Reload(index);
    }
    public void AddWeapon(Weapon newWeapon)
    {
        _heldWeapons.Add(newWeapon);
        newWeapon.Initialize(_index, _barrel);
    }
    void SwitchWeapon()
    {
        
    }
}
